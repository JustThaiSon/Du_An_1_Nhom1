﻿using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.IRepositories
{
    internal interface IVoucherRepository 
    {
        Voucher GetById(Guid Id);

        List<Voucher > GetAllVouchers();
        bool CreateVoucher(Voucher voucher);
        bool UpdadateVoucher(Voucher voucher);
        bool DeleteVoucher(Guid Id);
        string GenerateVoucherCode();
    }
}
