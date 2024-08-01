using BUS.IService;
using DAL.Entities;
using DAL.Enums;
using DAL.IRepositories;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BUS.Service
{
    public class VoucherSevice : IVoucherSevice
    {
        private IVoucherRepository _voucherRepo;
        private IOrdersRepository _ordersRepository;
        public VoucherSevice()
        {
            _voucherRepo = new VoucherRepository();
            _ordersRepository = new OrdersRepository();
        }

        public List<Voucher> GetAllVoucherFromDb()
        {
            return _voucherRepo.GetAllVouchers();
        }

        public string AddVoucher(Voucher Voucher)
        {
            if (_voucherRepo.CreateVoucher(Voucher))
            {
                return "add success";
            }
            else {
                return "add fail";
            }
        }

        public string UpdateVoucher(Voucher Voucher)
        {
            if (_voucherRepo.UpdadateVoucher(Voucher))
            {
                return "Updadate success";
            }
            else
            {
                return "Updadate fail";
            }
        }
        public string RemoveVoucher(Guid Id)
        {
            if (_voucherRepo.DeleteVoucher(Id))
            {
                return "Delete success";
            }
            else
            {
                return "Delete fail";
            }
        }

    

        public async Task UpdateVoucherStatusAuTo()
        {
            await _voucherRepo.UpdateVoucherStatusAuTo();
        }

        public string ValidateVoucher(string voucherCode, Guid orderId)
        {
            var order = _ordersRepository.GetAllOrder().FirstOrDefault(o => o.Id == orderId);
            if (order == null)
            {
                return "Order not found";
            }

            if (order.VoucherId != null)
            {
                return "Order has already applied a voucher";
            }

            var voucher = _voucherRepo.GetAllVouchers().FirstOrDefault(x => x.VoucherCode == voucherCode);
            if (voucher == null)
            {
                return "Invalid voucher code";
            }

            if (voucher.Status != VoucherStatus.Active)
            {
                return "Voucher is not active";
            }

            if (voucher.MinPrice > order.ToTalPrice)
            {
                return "Order total price does not meet voucher minimum price requirement";
            }

            return "Voucher validation successful";
        }

    }
}
