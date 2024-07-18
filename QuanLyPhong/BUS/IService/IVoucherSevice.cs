using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS.IService
{
    public interface IVoucherSevice 
    {
        string AddVoucher(Voucher Voucher);
        string RemoveVoucher(Guid Id);
        string UpdateVoucher(Voucher Voucher);
        List<Voucher> GetAllVoucherFromDb();
    }
}
