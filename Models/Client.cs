using System;
using System.Collections.Generic;

namespace Final.Models{
    public class Client{
        public Client(){}
        public Client(int id, string name, string surName, string telNumber)
        {
            this.Id = id;
            this.Name = name;
            this.SurName = surName;
            this.TelNumber = telNumber;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string TelNumber { get; set; }
    }
}