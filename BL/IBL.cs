using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BE;


namespace BL
{
    public interface IBL
    {
        void AddClientRequest(GuestRequest gr);
        void UpdateClientRequestStatus(GuestRequest gr, RequestStatus requestStatus);
        void AddHostingUnit(HostingUnit ho);
        void RemoveHostingUnit(HostingUnit ho);
        void UpdateHostingUnit(HostingUnit ho);
        void AddOrder(Order or);
        void UpdateOrder(Order or, OrderStatus ortStatus);
        List<HostingUnit> GetHostingUnits();
        List<GuestRequest> GetGuestRequests();
        List<Order> GetOrders();
        IEnumerable<BankBranch> getAllBankBranches();
        //returns the list of all available hosting units on this date.
        List<HostingUnit> AvailableHostingUnit(DateTime entry, int NumOfDays);
        //returns the days batween two dates
        int NumOfDays(params DateTime[] list);
        //returns all orders that have elapsed since they were created / Since sending an email to a customer greater than or equal to the number of days the function received.
        List<Order> CreatedOrSentOrders(int numDays);
        //return all customer requirements that fit a particular condition
        List<GuestRequest> ConditionGuestRequests(Func<GuestRequest, bool> myRequirments);//delegate
        //returns the number of orders sent to it
        int NumOfOrders(GuestRequest gr);
        //returns the number of orders sent / the number of orders Successfully closed for this unit through the site
        int ClosedOrSentOrders(HostingUnit hu);
        //returns List of customer requirements according to the required area.
        IEnumerable<IGrouping<AreaStatus, GuestRequest>> GroupByArea();
        //returns List of customer requirements according to the number of vacationers.
        IEnumerable<IGrouping<int, GuestRequest>> GroupByVacationers();
        //returns List of hosts by the number of accommodation units they hold
        List<Host> GroupByNumOfUnits(int num);
        //returns List of accommodation units according to the required area
        IEnumerable<IGrouping<AreaStatus, HostingUnit>> GroupHostingUnitsByArea();
        //returns num of all busy days
        int GetAnnualBusyDays(HostingUnit hu);
        //returns percentage of all busy days
        float GetAnnualBusyPercentage(HostingUnit hu);
        //returns sorted list of hosting units by prices
        List<HostingUnit> OrderByPrice();
        //returns list of hosting units between prices
        List<HostingUnit> BetweenPrices( int low, int high);
        //returns list of hosting units that included specific num of vacationers
        IEnumerable<IGrouping<int, HostingUnit>> NumOfVacationers();
        //returns list of specific type of hosting units 
        IEnumerable<IGrouping<TypeStatus, HostingUnit>> TypeOfHostingUnits();
        bool matchingGrAndHu(GuestRequest gr, HostingUnit hu);
        void SentMail(Order or, GuestRequest gr);
        void UpdateThread();

    }
}
