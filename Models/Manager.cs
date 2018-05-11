using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Final.Models
{
    public class Manager {
        public static string ValidateApartment(Apartment apartment)
        {
            if(apartment.Rooms < 100)
                return "success";
            else return "number of rooms must be less than 100";
        }
        public static string ValidateClient(Client client)
        {
            if(client.Name.Contains('.') == false && client.Name.Contains(',') == false && client.Name.Contains('?') == false && client.Name.Contains('!') == false)
                if(client.SurName.Contains('.') == false  && client.SurName.Contains(',') == false && client.SurName.Contains('?') == false && client.SurName.Contains('!') == false)
                    if(client.TelNumber.Length == 10)
                        return "success";
                    else return "not correct number";
                else return "invalid surname";
            else return "invalid name";
        }
        public static string ValidateOrder(Order order, List<Client> clients, List<Apartment> apartments)
        {
            if(clients.Contains(clients.FirstOrDefault(x => x.Id == order.ClientId)))
                if(apartments.Contains(apartments.FirstOrDefault(x => x.Id == order.ApartmentId)))
                    return "success";
                else return "apartment does not exist";
            else return "client does not exist";
        }
        public static string UpdateOrder(int apartmentId, int orderId, List<Apartment> apartments, List<Order> orders)
        {
            var apartment = apartments.FirstOrDefault(x => x.Id == apartmentId);
            var order = orders.FirstOrDefault(x => x.Id == orderId);
            if(apartments.Contains(apartment))
                if(apartment.RegionId == order.Region)
                    if(orders.Contains(order))
                        return "success";
                    else return "order does not exist";
                else return "apartment not from previous order's region";
            else return "apartment does not exist";
        }
    }
}