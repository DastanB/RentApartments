using System;
using System.Collections.Generic;

namespace Final.Models{
    public class Report{
        public Report(){}
        public Report(string region, long cost, int per)
        {
            this.Region = region;
            this.AllCosts = cost;
            this.Percent = per;
        }
        public string Region { get; set; }
        public long AllCosts { get; set; }
        public double Percent { get; set; }
    }
}