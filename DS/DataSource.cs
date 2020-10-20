using System;
using System.Collections.Generic;
using System.Text;
using BE;

namespace DS
{
    public class DataSource
    {

        public static List<GuestRequest> guestRequestList = new List<GuestRequest>()
        {
            new GuestRequest ()
            {
                GuestRequestKey =Configuration.guestRequestKeyS++,
                FamilyName = "Liberman",
                EntryDate = DateTime.Now.AddDays(2),
                MailAddress = "nam@gmail.com",
                Adults = 1,
                Area = AreaStatus.North,
                Children = 3,
                Pool = Additions.Necessary,
                PrivateName = "Naama",
                RegistrationDate = DateTime.Now,
                ReleaseDate = DateTime.Now.AddDays(7),
                Status = RequestStatus.Open,
                Type = TypeStatus.Hotel,
                Jacuzzi=Additions.Necessary,
                ChildrensAttractions=Additions.Notinterested,
                Garden=Additions.Notinterested
            },
            new GuestRequest ()
            {
                GuestRequestKey =Configuration.guestRequestKeyS++,
                FamilyName = "WE",
                EntryDate = DateTime.Now.AddDays(8),
                MailAddress = "SARIW4@gmail.com",
                Adults = 2,
                Area = AreaStatus.Jerusalem,
                Children = 3,
                Pool = Additions.Necessary,
                PrivateName = "sari",
                RegistrationDate = DateTime.Now,
                ReleaseDate = DateTime.Now.AddDays(9),
                Status = RequestStatus.Open,
                Type = TypeStatus.Hotel,
                Jacuzzi=Additions.Necessary,
                ChildrensAttractions=Additions.Notinterested,
                Garden=Additions.Notinterested
            }
        };

        public static List<Order> orderList = new List<Order>();
    



        public static List<HostingUnit> HostingunitList = new List<HostingUnit>()
        {
            new HostingUnit()
            {
               HostingUnitKey = Configuration.HostingUnitKeyS++,
               HostingUnitName = "WN",
               Diary =new bool [32,13],
               Owner = new Host
               {
                   HostKey = Configuration.HostKeyS++,
                   Password = 12345678,
                   PrivateName = "Sari",

                   FamilyName = "Weiss",
                   FhoneNumber = 0528109198,
                   BankAccountNumber = 58,
                   CollectionClearance = true,
                   MailAddress = "sariw@jct.ac.il"
               },
               Area = AreaStatus.Jerusalem,
               Price = 1500,
               Pool=true,
               Jacuzzi=true,
               NumOfVacationers = 6,
               Type = TypeStatus.Hotel

            },
             new HostingUnit()
            {
               HostingUnitKey = Configuration.HostingUnitKeyS++,
               HostingUnitName = "W",
               Diary =new bool [32,13],
               Owner = new Host
               {
                   HostKey = Configuration.HostKeyS++,
                   Password = 12345678,
                   PrivateName = "Sari",
                   FamilyName = "Weiss",
                   FhoneNumber = 0528109198,
                   BankAccountNumber = 58,
                   CollectionClearance = true,
                   MailAddress = "sariw@jct.ac.il"
               },
               Area = AreaStatus.North ,
               Price = 1260,
               NumOfVacationers = 3,
               Type = TypeStatus.Zimmer
            }

        };


    }

        
}
