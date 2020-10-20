using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using BE;
using DAL;
using System.Net.Mail;
using System.Threading;

namespace BL
{
    public class Ibl_imp : IBL
    {
        IDal dal = DalFactory.getDal();
        #region client func
        public void AddClientRequest(GuestRequest gr)
        {
            try
            {
                if ((gr.ReleaseDate - gr.EntryDate).TotalDays >= 1)
                {
                    dal.AddClientRequest(gr);
                }
                else
                    throw new Exception("The entry date isn't earlier than the release date");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public void UpdateClientRequestStatus(GuestRequest gr, RequestStatus requestStatus)
        {
            try
            {
                if (dal.GetGuestRequests().Exists(item => item.GuestRequestKey == gr.GuestRequestKey) == false)
                    throw new Exception("the guest request isn't exists ");
                else if (requestStatus == RequestStatus.Open && (gr.Status == RequestStatus.SiteClose || gr.Status == RequestStatus.Expired))
                {
                    throw new Exception("can't change the status because the guest request is closed");
                }
                else
                    dal.UpdateClientRequestStatus(gr, requestStatus);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
        #region hosting func
        public void AddHostingUnit(HostingUnit ho)
        {
            try
            {
                if (ho.Price<= 2000)
                {
                    if (ho.Owner.CollectionClearance == true)
                    {
                        dal.AddHostingUnit(ho);
                    }
                    else
                        throw new Exception("can't add hosting unit because the collection clearance isn't approved");
                }
                else
                {
                    throw new Exception("can't add hosting unit because the price is too expensive");

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void RemoveHostingUnit(HostingUnit ho)
        {
            try
            {
                if (dal.GetOrders().Count == 0)
                {
                    var v1 = from item in dal.GetHostingUnits()
                             where item.HostingUnitKey == ho.HostingUnitKey
                             select item;
                    foreach (var item in v1)
                    {
                        dal.RemoveHostingUnit(ho);
                    }
                }
                var v = from item in dal.GetOrders()
                        where item.HostingUnitKey == ho.HostingUnitKey
                        select item;
                foreach (var item in v)
                {
                    if (item.Status != OrderStatus.MailHasBeenSent || item.Status != OrderStatus.NotYetAddressed)
                    {
                        dal.RemoveHostingUnit(ho);
                    }
                    else
                        throw new Exception("can't remove the hosting unit because she has open orders");
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void UpdateHostingUnit(HostingUnit ho)
        {
            try
            {
                HostingUnit ho1 = dal.GetHostingUnits().Find(item => item.HostingUnitKey == ho.HostingUnitKey);
                if (ho.Owner.CollectionClearance == false && ho1.Owner.CollectionClearance == true)
                {
                    foreach (var item in dal.GetOrders())
                    {
                        if (item.HostingUnitKey == ho.HostingUnitKey && (item.Status == OrderStatus.ClosesOutOfResponsiveness || item.Status == OrderStatus.ClosesWithResponse))
                        {
                            dal.UpdateHostingUnit(ho);
                        }
                        else throw new Exception("can't change collection clearance because you have open orders");
                    }
                }
                else
                    dal.UpdateHostingUnit(ho);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
        #region order func
        public void AddOrder(Order or)
        {

            GuestRequest gr = dal.GetGuestRequests().Find(item => item.GuestRequestKey == or.GuestRequestKey);
            HostingUnit ho = dal.GetHostingUnits().Find(item => item.HostingUnitKey == or.HostingUnitKey);
            if (gr != null && ho != null)
            {
                bool degel = true;
                for (DateTime i = gr.EntryDate.AddDays(1); i < gr.ReleaseDate; i = i.AddDays(1))
                {
                    if (ho.Diary[i.Day, i.Month] != false)
                    {
                        degel = false;
                    }
                }
                if (degel == true)
                {
                    try { dal.AddOrder(or); }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                    or.CreateDate = DateTime.Today;
                }
                else
                    throw new Exception("This hosting unit isn't aviable in this dates period!");
            }
            else
                throw new Exception("The guest request or the hosting unit doesn't exist!");


        }
        public void UpdateOrder(Order or, OrderStatus ortStatus)
        {
            try
            {

                GuestRequest gr = dal.GetGuestRequests().Find(item => item.GuestRequestKey == or.GuestRequestKey);
                HostingUnit ho = dal.GetHostingUnits().Find(item => item.HostingUnitKey == or.HostingUnitKey);

                if (ortStatus == OrderStatus.MailHasBeenSent)
                {
                    if (ho.Owner.CollectionClearance == true)
                    {
                        dal.UpdateOrder(or, ortStatus);
                        Console.WriteLine(or.ToString());
                    }
                    else throw new Exception("You have not erranged the collection clearance with the bank - you can not send an email!");
                }

                if (or.Status == OrderStatus.ClosesOutOfResponsiveness || or.Status == OrderStatus.ClosesWithResponse)
                {
                    throw new Exception("can't change the order status because the order is closed ");
                }
                if (ortStatus == OrderStatus.ClosesWithResponse)
                {
                    for (DateTime i = gr.EntryDate.AddDays(1); i < gr.ReleaseDate; i = i.AddDays(1))
                    {
                        ho.Diary[i.Day, i.Month] = true;
                    }
                    ho.Owner.Payment += Configuration.fee * NumOfDays(gr.EntryDate, gr.ReleaseDate);
                    var v = from item in dal.GetOrders()
                            where item.GuestRequestKey == gr.GuestRequestKey && item.OrderKey != or.OrderKey
                            select item;
                    foreach (var item in v)
                    {
                        item.Status = OrderStatus.ClosesOutOfResponsiveness;
                    }
                    gr.Status = RequestStatus.SiteClose;
                    dal.UpdateOrder(or, ortStatus);
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
        public List<HostingUnit> GetHostingUnits()
        {
            return dal.GetHostingUnits();
        }
        public List<GuestRequest> GetGuestRequests()
        {
            return dal.GetGuestRequests();
        }
        public List<Order> GetOrders()
        {
            return dal.GetOrders();
        }

        public IEnumerable<BankBranch> getAllBankBranches()
        {
            return dal.getAllBankBranches();
        }
        public List<HostingUnit> AvailableHostingUnit(DateTime enetry, int NumOfDays)
        {
            List<HostingUnit> Newlist = new List<HostingUnit>();
            foreach (var item in dal.GetHostingUnits())
            {
                bool degel = true;
                for (DateTime i = enetry.AddDays(1); i < enetry.AddDays(NumOfDays); i = i.AddDays(1))
                {
                    if (item.Diary[i.Day, i.Month] == false)
                        degel = false;
                }
                if (degel == false)
                {
                    Newlist.Add(item);
                }
            }
            if (Newlist.Count == 0)
            {
                throw new Exception("there is no hosting unit that meet the requirements");
            }
            else
                return Newlist;

        }
        public int NumOfDays(params DateTime[] list)
        {
            if (list.Count() == 1)
                return (int)(DateTime.Today - list[0]).TotalDays;
            else if (list.Count() == 2)
                return (int)(list[1] - list[0]).TotalDays;
            else
                return 0;
        }
        public List<Order> CreatedOrSentOrders(int NumOfDays)
        {
            //To check if the defualt datetime value is today's date
            try
            {
                List<Order> NewList = new List<Order>();
                foreach (Order item in dal.GetOrders())
                {
                    if (item.Status == OrderStatus.MailHasBeenSent && (DateTime.Today - item.OrderDate).TotalDays < NumOfDays) //Configuration.numDaysExpire
                        NewList.Add(item);
                    if (item.Status == OrderStatus.NotYetAddressed && (DateTime.Today - item.CreateDate).TotalDays < NumOfDays) //Configuration.numDaysExpire
                        NewList.Add(item);
                }
                if (NewList == null)
                {
                    throw new Exception("there is no  orders that meet the requirements");
                }
                return NewList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<GuestRequest> ConditionGuestRequests(Func<GuestRequest, bool> myRequirments) //delegate
        {

            List<GuestRequest> newList = (from GuestRequest item in dal.GetGuestRequests()
                                          where myRequirments(item) == true
                                          select item).ToList();
            return newList.Count == 0 ? throw new Exception("There is no guest request that meet the requirements") : newList;

        }
        public int NumOfOrders(GuestRequest gr)
        {
            int count = 0;
            var v = from item in dal.GetOrders()
                    where item.GuestRequestKey == gr.GuestRequestKey
                    select item;
            foreach (var item in v)
            {
                count++;
            }
            return  count;

        }
        public int ClosedOrSentOrders(HostingUnit hu)
        {
            int count = 0;
            var v = from item in dal.GetOrders()
                    where item.HostingUnitKey == hu.HostingUnitKey
                          && (item.Status == OrderStatus.ClosesWithResponse
                          || item.Status == OrderStatus.MailHasBeenSent)
                    select item;
            foreach (var item in v)
            {
                count++;
            }
            return count;

        }

        public IEnumerable<IGrouping<AreaStatus, GuestRequest>> GroupByArea()
        {
            return from item in dal.GetGuestRequests()
                   group item by item.Area into gr
                   select gr;
        }
        public IEnumerable<IGrouping<int, GuestRequest>> GroupByVacationers()
        {
            return from item in dal.GetGuestRequests()
                   let numVacationers = item.Adults + item.Children
                   group item by numVacationers into gr
                   select gr;
        }
        public List<Host> GroupByNumOfUnits(int num)
        {
            var v = from item in dal.GetHostingUnits()
                    let numHostingUnit = NumOfHostingUnitOfTheHost(item.Owner.HostKey)
                    group item by numHostingUnit into ho
                    select ho;
            List<Host> lst = new List<Host>();
            foreach (IGrouping<int, HostingUnit> ho in v)
            {
                if (ho.Key == num)
                {
                    Console.WriteLine(ho.Key);
                    foreach (var item in ho)
                    {
                        lst.Add(item.Owner);
                    }
                }
            }
            return lst;

        }
        public IEnumerable<IGrouping<AreaStatus, HostingUnit>> GroupHostingUnitsByArea()
        {
            return from item in dal.GetHostingUnits()
                   group item by item.Area into hu
                   select hu;

        }
        private int NumOfHostingUnitOfTheHost(long keyHost)
        {
            var v = from item in dal.GetHostingUnits()
                    where item.Owner.HostKey == keyHost
                    select item;
            return v.ToList().Count;

        }
        public int GetAnnualBusyDays(HostingUnit hu)
        {
            if (dal.GetHostingUnits().Exists(item => item.HostingUnitKey == hu.HostingUnitKey) == true)
            {
                int count1 = 0;
                for (int i = 0; i < 31; i++)
                {
                    for (int j = 1; j < 12; j++)
                    {
                        if (hu.Diary[i, j] == true)
                        {
                            count1++;
                        }

                    }
                }
                return count1;
            }
            else
                throw new Exception("the hosting unit isn't exists");

        }
        public float GetAnnualBusyPercentage(HostingUnit hu)
        {
            if (dal.GetHostingUnits().Exists(item => item.HostingUnitKey == hu.HostingUnitKey) == true)
            {
                int count2 = GetAnnualBusyDays(hu);
                float per = (float)(count2 / 372.0) * 100;
                return per;
            }
            else
                throw new Exception("the hosting unit isn't exists");
        }
        public List<HostingUnit> OrderByPrice()
        {
            List<HostingUnit> h = new List<HostingUnit>();
            var v = from item in dal.GetHostingUnits()
                    orderby item.Price
                    select item;
            foreach (var item in v)
            {
                h.Add(item);
            }
            if (h.Count == 0)
            {
                throw new Exception("There is no hosting unit");
            }
            return h;
        }
        public List<HostingUnit> BetweenPrices( int low, int high)
        {
            List<HostingUnit> H = new List<HostingUnit>();
            H = dal.GetHostingUnits().FindAll(item =>   item.Price > low && item.Price < high);
            return H.Count == 0 ? throw new Exception("There is no hosting unit that meet the requirements") : H;
        }
       
        public IEnumerable<IGrouping<int, HostingUnit>> NumOfVacationers()
        {
            return from item in dal.GetHostingUnits()
                   group item by item.NumOfVacationers into hu
                   select hu;

        }
       
        public IEnumerable<IGrouping<TypeStatus, HostingUnit>> TypeOfHostingUnits()
        {
            return from item in dal.GetHostingUnits()
                   group item by item.Type into hu
                   select hu;

        }
        public void UpdateThread()
        {
            Thread t = new Thread(UpdateWithNotResponsOrders);
            t.IsBackground = true;
            t.Start();
        }

        private void UpdateWithNotResponsOrders()
        {
            while (true)
            {
              foreach (var item in GetOrders())
              {
                    if (item.OrderDate.AddDays(31)>=DateTime.Now&& item.Status==OrderStatus.MailHasBeenSent)
                    {
                        UpdateOrder(item, OrderStatus.ClosesOutOfResponsiveness);
                    }
                    Thread.Sleep(86400000);
              }
            }
        }

        public bool matchingGrAndHu(GuestRequest gr, HostingUnit hu)
        {
            if (gr.Status==RequestStatus.SiteClose)
                return false;
            for (DateTime i = gr.EntryDate; i < gr.ReleaseDate; i = i.AddDays(1))
            {
                //אם היחידה תפוסה
                if (hu.Diary[i.Day, i.Month])
                    return false;
            }
            if (gr.Jacuzzi == Additions.Necessary && hu.Jacuzzi == false)
                return false;
            if (gr.Pool == Additions.Necessary && hu.Pool == false)
                return false;
            if (gr.Garden == Additions.Necessary && hu.Garden == false)
                return false;
            if (gr.Area != hu.Area && gr.Area == AreaStatus.All)
                return false;
            if (gr.Type != hu.Type)
                return false;
            if (gr.Children + gr.Adults > hu.NumOfVacationers)
                return false;
            return true;
        }
        public void SentMail(Order or,GuestRequest gr)
        {
            MailMessage mail = new MailMessage();            mail.To.Add(gr.MailAddress);            mail.From = new MailAddress("NWtravelings@gmail.com");            mail.Subject = "We find the perfect vacation for you";            mail.Body = "Contact us for more details " + "phone number: 0539620601"+ "mail: NWtravelings@gmail.com";            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Credentials = new System.Net.NetworkCredential("NWtravelings@gmail.com",
           "2880bgnv");            smtp.EnableSsl = true;
           smtp.Port = 25;
             smtp.Send(mail);

        }
        public string AllHostingUnitNames()
        {
            var NamesGroups =
                    from item in GetHostingUnits()
                    group item by item.HostingUnitName into gs
                    select new { FirstLetter = gs.Key, Names = gs };
            string str = "";
            foreach (var item in NamesGroups)
            {
                str += item.FirstLetter + "\n";
                foreach (var name in item.Names)
                    str += name + "\n";
            }
            return str;

        }
    } 
    }
