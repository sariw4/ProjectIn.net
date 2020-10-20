using System;
using System.Collections.Generic;
using System.Text;

namespace BE
{
    public class GuestRequest
    {
        public long GuestRequestKey { get; set; }
        public string PrivateName { get; set; }
        public string FamilyName { get; set; }
        public string MailAddress { get; set; }
        public RequestStatus Status { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime EntryDate { get; set; }
        public DateTime ReleaseDate { get; set; }
        public AreaStatus Area { get; set; }
        public string SubArea { get; set; }
        public TypeStatus Type { get; set; }
        public int Adults { get; set; }
        public int Children { get; set; }
        public Additions Pool { get; set; }
        public Additions Jacuzzi { get; set; }
        public Additions Garden { get; set; }
        public Additions ChildrensAttractions { get; set; }
        public override string ToString()
        {
            return PrivateName.ToString() + FamilyName.ToString() + MailAddress.ToString() + Status.ToString() +
                RegistrationDate.ToString() + EntryDate.ToString() + ReleaseDate.ToString() + Area.ToString() +
                 Type.ToString() + Adults.ToString() + Children.ToString() + Pool.ToString() + Jacuzzi.ToString()
                 + Garden.ToString() + ChildrensAttractions.ToString();
        }
    }
}
