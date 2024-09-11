﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exwhyzee.Messaging.Web.PayStack.Fees
{
    public static class ChargeHelper
    {
        static PaystackFee paystackFee = new PaystackFee();

        public static int AddCharge(int amountInKobo)
        {            
            return paystackFee.AddCharge(amountInKobo);
        }

        public static decimal CalculatedCharge(int amountInKobo)
        {
            var amountWithCharge = paystackFee.AddCharge(amountInKobo);
            return ((amountWithCharge - amountInKobo) / 100);
        }        
    }

    public  class CustomerCharge : IChargeHelper
    {
         PaystackFee paystackFee = new PaystackFee();

        public int AddCharge(int amountInKobo)
        {
            return paystackFee.AddCharge(amountInKobo);
        }

        public decimal CalculatedCharge(int amountInKobo)
        {
            var amountWithCharge = paystackFee.AddCharge(amountInKobo);
            return ((amountWithCharge - amountInKobo) / 100);
        }
    }
}
