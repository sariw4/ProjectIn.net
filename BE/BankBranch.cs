﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{


    public class BankBranch
    {
        public int BankNumber { get; set; }
        public string BankName { get; set; }
        public int BranchNumber { get; set; }
        public string BranchAddress { get; set; }
        public string BranchCity { get; set; }
       // public long BankAccountNumber { get; set; }
        public override string ToString()
        {
            return BankNumber.ToString() + BankName.ToString() + BranchNumber.ToString() + BranchAddress.ToString() +
                BranchCity.ToString() .ToString();
        }
    }
}

