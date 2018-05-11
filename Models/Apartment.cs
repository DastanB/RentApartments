using System;
using System.Collections.Generic;

namespace Final.Models{
    public class Apartment{
        public Apartment(){}
        public Apartment(int id, int rooms, long cost, string regionId, string description)
        {
            this.Id = id;
            this.Rooms = rooms;
            this.Cost = cost;
            this.RegionId = regionId;
            this.Description = description;
        }
        public int Id { get; set; }
        public int Rooms { get; set; }
        public long Cost { get; set; }
        public string Description { get; set; }
        public string RegionId { get; set; }
    }
}