using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace DAL
{
    public interface IDal
    {
        //add client request to the list
        void AddClientRequest(GuestRequest gr);
        //update client requestStatus
        void UpdateClientRequestStatus(GuestRequest gr, RequestStatus requestStatus);
        //add hosting unit to the list
        void AddHostingUnit(HostingUnit ho);
        //remove hosting unit from the list
        void RemoveHostingUnit(HostingUnit ho);
        //update hosting unit
        void UpdateHostingUnit(HostingUnit ho);
        //add order to the list
        void AddOrder(Order or);
        //update order
        void UpdateOrder(Order or, OrderStatus ortStatus);
        // return list of hosting units
        List<HostingUnit> GetHostingUnits();
        // return list of guest requests
        List<GuestRequest> GetGuestRequests();
        // return list of orders
        List<Order> GetOrders();
        // return list of bank branch
        IEnumerable<BankBranch> getAllBankBranches();
    }
}
