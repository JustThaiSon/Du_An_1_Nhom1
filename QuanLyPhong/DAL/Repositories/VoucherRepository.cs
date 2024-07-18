using DAL.Data;
using DAL.Entities;
using DAL.IRepositories;
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
            update.VoucherCode = voucher.VoucherCode;
            update.VoucherName = voucher.VoucherName;
            update.DiscountRate = voucher.DiscountRate;
            update.MinPrice = voucher.MinPrice;
            update.StartDate = voucher.StartDate;
            update.EndDate = voucher.EndDate;
            update.Status = voucher.Status; 
            return true;
        }
    }
}
