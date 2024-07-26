using DAL.Data;
using DAL.Entities;
using DAL.Enums;
using DAL.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class VoucherRepository : IVoucherRepository
    {
        private MyDbContext _context;
        public VoucherRepository()
        {
            _context = new MyDbContext();
        }

        public bool CreateVoucher(Voucher voucher)
        {
            try
            {
                if (voucher != null)
                {
                    voucher.Id = Guid.NewGuid();
                    voucher.VoucherCode = GenerateVoucherCode();
                    voucher.Status = AddVoucherStatus(voucher);
                    _context.Vouchers.Add(voucher);
                    _context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex) { return false; }
        }

        public bool DeleteVoucher(Guid Id)
        {
            var delete = _context.Vouchers.Find(Id);
            if (delete != null)
            {
                _context.Vouchers.Remove(delete);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public string GenerateVoucherCode()
        {
            long timestamp = DateTime.UtcNow.Ticks;
            int random = new Random().Next(1000, 9999); // Random số 4 chữ số

            // Lấy phần cuối của timestamp để đảm bảo độ dài mã không quá dài
            long truncatedTimestamp = timestamp % 1000000;

            string base36 = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var buffer = new StringBuilder();
            while (truncatedTimestamp > 0)
            {
                buffer.Insert(0, base36[(int)(truncatedTimestamp % 36)]);
                truncatedTimestamp /= 36;
            }
            // Kết hợp với số ngẫu nhiên, đảm bảo chuỗi cuối cùng có độ dài 8 ký tự
            string result = buffer.ToString() + random.ToString();
            if (result.Length > 8)
            {
                result = result.Substring(0, 8);
            }
            else if (result.Length < 8)
            {
                result = result.PadRight(8, '0');
            }

            return result;
        }

        public List<Voucher> GetAllVouchers()
        {
            return _context.Vouchers.ToList();
        }

        public Voucher GetById(Guid Id)
        {
            return _context.Vouchers.FirstOrDefault(x => x.Id == Id);
        }

        public bool UpdadateVoucher(Voucher voucher)
        {
            var update = _context.Vouchers.FirstOrDefault(y => y.Id == voucher.Id);
            if (update == null) return false;
           
            update.Id = voucher.Id;
            update.VoucherName = voucher.VoucherName;
            update.DiscountRate = voucher.DiscountRate;
            update.MinPrice = voucher.MinPrice;
            update.StartDate = voucher.StartDate;
            update.EndDate = voucher.EndDate;
            if (voucher.Status == VoucherStatus.Cancelled)
            {
                update.Status = VoucherStatus.Cancelled;
            }
            else
            {
                voucher.Status = update.Status;
            }
            return true;
        }

        public VoucherStatus AddVoucherStatus(Voucher voucher)
        {
            DateTime now = DateTime.UtcNow;

            if (voucher.Status == VoucherStatus.Cancelled)
            {
                return VoucherStatus.Cancelled;
            }
            else if (now < voucher.StartDate)
            {
                return VoucherStatus.NotDueYet; // Chưa đến ngày bắt đầu
            }
            else if (now > voucher.EndDate)
            {
                return VoucherStatus.Expired; // Đã qua ngày kết thúc
            }
            else if (now >= voucher.StartDate && now <= voucher.EndDate)
            {
                return VoucherStatus.Active; // Đang trong thời gian sử dụng
            }
            else
            {
                return VoucherStatus.NotDueYet; // Trường hợp khác (mặc định chưa đến ngày bắt đầu)
            }
        }


        public async Task UpdateVoucherStatusAuTo()
        {
            var vouchers = await _context.Vouchers.ToListAsync();

            foreach (var voucher in vouchers)
            {
                DateTime now = DateTime.UtcNow;


                if (voucher.Status == VoucherStatus.Cancelled)
                {
                    voucher.Status = VoucherStatus.Cancelled;
                }
                else if (now < voucher.StartDate)
                {
                    voucher.Status = VoucherStatus.NotDueYet; // Chưa đến ngày bắt đầu
                }
                else if (now > voucher.EndDate)
                {
                    voucher.Status = VoucherStatus.Expired; // Đã qua ngày kết thúc
                }
                else if (now >= voucher.StartDate && now <= voucher.EndDate)
                {
                    voucher.Status = VoucherStatus.Active; // Đang trong thời gian sử dụng
                }
                else
                {
                    voucher.Status = VoucherStatus.NotDueYet; // Trường hợp khác (mặc định chưa đến ngày bắt đầu)
                }
            }
           await _context.SaveChangesAsync();
        }

    }
}

