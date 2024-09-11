﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exwhyzee.Messaging.Web.PayStack.Models
{
    public class TransactionInitializationRequestModel
    {
        public string email { get; set; }
        public int amount { get; set; }
        public string subaccount { get; set; }
        public Int32 transaction_charge { get; set; } = 0;
        public string bearer { get; set; } = "account";
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string callbackUrl { get; set; }
        public string reference { get; set; }
        public string plan { get; set; }
        public Int32 invoice_limit { get; set; } = 0;
        public bool makeReferenceUnique { get; set; } = false;
    }
}
