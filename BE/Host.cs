using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Host
    {
        public long Password { get; set; }
        public long HostKey { get; set; }
        public string PrivateName { get; set; }
        public string FamilyName { get; set; }
        public long FhoneNumber { get; set; }
        public string MailAddress { get; set; }
        public long BankAccountNumber { get; set; }
        public BankBranch BankBranchDetails { get; set; }
        public bool CollectionClearance { get; set; }
        public int Payment { get; set; }

        //for debugging
        public override string ToString()
        {
            return HostKey.ToString()+ PrivateName.ToString()+ FamilyName.ToString()+
                FhoneNumber.ToString() + MailAddress.ToString() + BankAccountNumber.ToString()+
                  CollectionClearance.ToString() ;
        }
    }
}
