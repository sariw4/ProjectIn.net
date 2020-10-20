using System;
using System.Collections.Generic;
using System.Text;
using BE;
using DS;

using System.Linq;

namespace DAL
{
    //***Dal_imp impliment Idal interface***
    //***I added public to the function's signs

    public class Dal_imp : IDal
    {
      
        //Add and Update for guestRequest
        public void AddClientRequest(GuestRequest gr)
        {
            if (DataSource.guestRequestList.Exists(item => item.GuestRequestKey == gr.GuestRequestKey) == true)
                throw new Exception("the guest request is already exists ");
            else
            {
                DataSource.guestRequestList.Add(gr);
                gr.GuestRequestKey = Configuration.guestRequestKeyS++;
                gr.Status = RequestStatus.Open;
                gr.RegistrationDate = DateTime.Now;
            }
        }
        public void UpdateClientRequestStatus(GuestRequest gr, RequestStatus requestStatus)
        {
            var v = from item in DataSource.guestRequestList
                    where item.GuestRequestKey == gr.GuestRequestKey
                    select item;
            if (v!=null)
            {
                foreach (var item in v)
                {
                    if (requestStatus == item.Status)
                        throw new Exception("the status of guest request is already initialized to what you requested ");
                    else
                        gr.Status = requestStatus;
                }
            }
            else
                throw new Exception("the status of guest request isn't exists");
        }
        public void AddHostingUnit(HostingUnit ho)
        {
            if (DataSource.HostingunitList.Exists(item => item.HostingUnitKey == ho.HostingUnitKey) == true)
                throw new Exception("the hosting unit is already exists ");
            else
            {
                DataSource.HostingunitList.Add(ho);
                ho.HostingUnitKey = Configuration.HostingUnitKeyS++;
                bool[,] d = new bool[32, 13];
                for (int j = 0; j < 32; j++)
                {
                    for (int i = 0; i < 13; i++)
                        d[j, i] = false;
                }
                ho.Diary = d;
            }
        }
        public void RemoveHostingUnit(HostingUnit ho)
        {
            if (DataSource.HostingunitList.Exists(item => item.HostingUnitKey == ho.HostingUnitKey) == false)
                throw new Exception("the hosting unit isn't exists ");
            else
                DataSource.HostingunitList.Remove(ho);
           
        }
        public void UpdateHostingUnit(HostingUnit ho)
        {
            int x = (DataSource.HostingunitList.FindIndex(item => item.HostingUnitKey == ho.HostingUnitKey));
            if (x != -1)
            {
                DataSource.HostingunitList[x] = ho;
            }
            else { throw new Exception("the hosting unit isn't exists "); }
        }
        public void AddOrder(Order or)
        {
            if (DataSource.orderList.Exists(item => item.OrderKey == or.OrderKey) == true)
                throw new Exception("the order is already exists ");
            else
            {
                DataSource.orderList.Add(or);
                or.OrderKey = Configuration.OrderKeyS++;
            }
        }
        public void UpdateOrder(Order or, OrderStatus ortStatus)
        {
            var v = from item in DataSource.orderList
                    where or.OrderKey == item.OrderKey
                    select item;
            if(v!=null)
            {
                foreach (var item in v)
                {
                    if (ortStatus == item.Status)
                        throw new Exception("the status of order is already initialized to what you requested ");

                    else
                     or.Status = ortStatus;
                }
                
            }
            else { throw new Exception("the order isn't exists "); }
        }
        public List<HostingUnit> GetHostingUnits()
        {
            HostingUnit[] HArr = new HostingUnit[DataSource.HostingunitList.Count];
            DataSource.HostingunitList.CopyTo(HArr);
            return HArr.ToList();
        }
        public List<GuestRequest> GetGuestRequests()
        {
            GuestRequest[] GArr = new GuestRequest[DataSource.guestRequestList.Count];
            DataSource.guestRequestList.CopyTo(GArr);
            return GArr.ToList();
        }
        public List<Order> GetOrders()
        {
            Order[] OArr = new Order[DataSource.orderList.Count];
            DataSource.orderList.CopyTo(OArr);
            return OArr.ToList();
        }

        // public List<BankBranch> ReturnBankBranch()
        //{
        //     List<BankBranch> b = new List<BankBranch>();
        //     BankBranch b1 = new BankBranch
        //     {
        //         BankAccountNumber = 12345678,
        //         BankName = "Haphoalim",
        //         BankNumber = 176,
        //         BranchAddress = "Nisim Gaon 12",
        //         BranchCity = "Elad",
        //         BranchNumber = 12,
        //     };
        //     b.Add(b1);
        //     BankBranch b2 = new BankBranch
        //     {
        //         BankAccountNumber = 23456789,
        //         BankName = "Pagi",
        //         BankNumber = 546,
        //         BranchAddress = "Rabi Akiva 16",
        //         BranchCity = "Bnei Brak",
        //         BranchNumber = 43,
        //     };
        //     b.Add(b2);
        //     BankBranch b3 = new BankBranch
        //     {
        //         BankAccountNumber = 34567890,
        //         BankName = "Mizrachi",
        //         BankNumber = 435,
        //         BranchAddress = "Hgilgal 2",
        //         BranchCity = "Bnei Brak",
        //         BranchNumber = 32,
        //     };
        //     b.Add(b3);
        //     BankBranch b4 = new BankBranch
        //     {
        //         BankAccountNumber = 45678901,
        //         BankName = "Egud",
        //         BankNumber = 98,
        //         BranchAddress = "Haemek 8",
        //         BranchCity = "Tel Aviv",
        //         BranchNumber = 15,
        //     };
        //     b.Add(b4);
        //     BankBranch b5 = new BankBranch
        //     {
        //         BankAccountNumber = 56789012,
        //         BankName = "Leumi",
        //         BankNumber = 17,
        //         BranchAddress = "Menachem 34",
        //         BranchCity = "Jerusalem",
        //         BranchNumber = 71,
        //     };
        //     b.Add(b5);
        //     return b;
        //}
        public IEnumerable<BankBranch> getAllBankBranches()
        {
            throw new Exception();
        }

    }
}
