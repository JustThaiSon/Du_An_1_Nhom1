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

        public VoucherSevice()
        {
            _voucherRepo = new VoucherRepository();
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
    }
}
