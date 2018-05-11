using System;
using System.Collections.Generic;

namespace Final.Models{
    public class Order{
        public Order(){}
        public Order(int id, int descriptionId, int clientId, long cost, string region)
        {
            this.Id = id;
            this.ApartmentId = descriptionId;
            this.ClientId = clientId;
            this.Cost = cost;
            this.Region = region;
        }
        public int Id { get; set; }
        public int ApartmentId { get; set; }
        public int ClientId { get; set; }
        public long Cost { get; set; }
        public string Region { get; set; }
    }
}