using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Final.Models;
using Microsoft.AspNetCore.Mvc;

namespace Final.Controllers
{
    [Route("api/[controller]")]
    public class OrderController : Controller
    {
        private FinalDbContext database;

        public OrderController(FinalDbContext context)
        {
            database = context;
        }

        [HttpGet]
        public IActionResult GetOrders()
        {
            var query = database.Orders
                .AsQueryable();
            
            return Ok(query.ToList());
        }

        [HttpGet]
        [Route("Total")]
        public IActionResult GetTotal()
        {
            var query = database.Orders
                .AsQueryable();
            
            long total = 0;

            foreach(var order in query)
            {
                total += order.Cost;
            }
            return Ok(total);
        }

        [HttpGet]
        [Route("Report")]
        public IActionResult GetReports()
        {
            var query = database.Orders
                .AsQueryable();
            
            var reports = new List<Report>();

            var reportM = new Report();
            reportM.Region = "Медеуский";
            var reportA = new Report();
            reportA.Region = "Алмалинский";
            var reportB = new Report();
            reportB.Region = "Бостандыкский";
            var reportN = new Report();
            reportN.Region = "Наурызбайский";
            var reportAl = new Report();
            reportAl.Region = "Алатауский";
            var reportAu = new Report();
            reportAu.Region = "Ауэзовский";
            var reportZh = new Report();
            reportZh.Region = "Жетысуский";
            var reportT = new Report();
            reportT.Region = "Турксибский";

            double total = 0;
            
            foreach(var order in query)
            {
                if(order.Region == "Медеуский") reportM.AllCosts += order.Cost;
                else if(order.Region == "Алмалинский") reportA.AllCosts += order.Cost;
                else if(order.Region == "Бостандыкский") reportB.AllCosts += order.Cost;
                else if(order.Region == "Наурызбайский") reportN.AllCosts += order.Cost;
                else if(order.Region == "Алатауский") reportAl.AllCosts += order.Cost;
                else if(order.Region == "Ауэзовский") reportAu.AllCosts += order.Cost;
                else if(order.Region == "Жетысуский") reportZh.AllCosts += order.Cost;
                else reportT.AllCosts += order.Cost;
                total += order.Cost;
            }
            
            reportM.Percent = 100*Convert.ToDouble(reportM.AllCosts)/total;
            reportA.Percent = 100*Convert.ToDouble(reportA.AllCosts)/total;
            reportB.Percent = 100*Convert.ToDouble(reportB.AllCosts)/total;
            reportN.Percent = 100*Convert.ToDouble(reportN.AllCosts)/total;
            reportAl.Percent = 100*Convert.ToDouble(reportAl.AllCosts)/total;
            reportAu.Percent = 100*Convert.ToDouble(reportAu.AllCosts)/total;
            reportZh.Percent = 100*Convert.ToDouble(reportZh.AllCosts)/total;
            reportT.Percent = 100*Convert.ToDouble(reportT.AllCosts)/total;

            reports.Add(reportM);
            reports.Add(reportA);
            reports.Add(reportN);
            reports.Add(reportB);
            reports.Add(reportAl);
            reports.Add(reportAu);
            reports.Add(reportZh);
            reports.Add(reportT);

            return Ok(reports);
        }

        [HttpGet("{id}")]
        public IActionResult GetOrder(int Id)
        {
            var query = database.Orders
                .AsQueryable();

            query = query.Where(x => x.Id == Id);
            
            return Ok(query.ToList());
        }

        [HttpPost]
        public IActionResult PostOrder([FromQuery]string ids, [FromQuery]int clientId, [FromBody]Order o)
        {
            var orders = new List<Order>();
            foreach(var i in ids.Split(","))
            {
                var id = Convert.ToInt32(i);
                var order = new Order();
                var apartment = database.Apartments.FirstOrDefault(x => x.Id == id);;
                var client = database.Clients.FirstOrDefault(x => x.Id == clientId);
                order.ApartmentId = id;
                order.ClientId = clientId;
                var result = Manager.ValidateOrder(order, database.Clients.ToList(), database.Apartments.ToList());
                if(result == "success")
                {
                    
                    order.Region = apartment.RegionId;

                    if(apartment.RegionId == "Медеуский") order.Cost = Convert.ToInt64(apartment.Cost * 1.12);
                    else if(apartment.RegionId == "Алмалинский") order.Cost = Convert.ToInt64(apartment.Cost * 1.11);
                    else if(apartment.RegionId == "Бостандыкский") order.Cost = Convert.ToInt64(apartment.Cost * 1.1);
                    else if(apartment.RegionId == "Наурызбайский") order.Cost = Convert.ToInt64(apartment.Cost * 1.09);
                    else if(apartment.RegionId == "Алатауский") order.Cost = Convert.ToInt64(apartment.Cost * 1.08);
                    else if(apartment.RegionId == "Ауэзовский") order.Cost = Convert.ToInt64(apartment.Cost * 1.07);
                    else if(apartment.RegionId == "Жетысуский") order.Cost = Convert.ToInt64(apartment.Cost * 1.06);
                    else order.Cost = Convert.ToInt64(apartment.Cost * 1.05);
                    orders.Add(order);
                }
                else return BadRequest(result);
            
            }

            string reg = orders[0].Region;
            int cnt = 0;
            foreach(var order in orders)
            {
                if(order.Region == reg) cnt += 0;
                else cnt++;
            }

            if(cnt == 0)
            {
                foreach(var order in orders)
                {
                    database.Orders.Add(order);
                }
                database.SaveChanges();
                var query = database.Orders
                .AsQueryable();
                return Ok(query);
            }

            else return BadRequest("Apartments not from one region");
            
            
        
        }
 
        [HttpDelete("{id}")]
        public IActionResult DeleteOrder(int id)
        {
            var item = database.Orders.First(x => x.Id == id);
            database.Orders.Remove(item);
            database.SaveChanges();

            var query = database.Orders
                .AsQueryable();

            return Ok(query.ToList());

        }

        [HttpPut]
        public IActionResult Put([FromQuery]int orderId, [FromQuery]int apartmentId, [FromBody]Order o)
        {
                var result = Manager.UpdateOrder(apartmentId, orderId, database.Apartments.ToList(), database.Orders.ToList());
                if(result == "success")
                {
                    var order = database.Orders.FirstOrDefault(x => x.Id == orderId);
                    order.ApartmentId = apartmentId;
                    var apartment = database.Apartments.FirstOrDefault(x => x.Id == apartmentId);
                    order.Region = apartment.RegionId;
                    if(apartment.RegionId == "Медеуский") order.Cost = Convert.ToInt64(apartment.Cost * 1.12);
                    else if(apartment.RegionId == "Алмалинский") order.Cost = Convert.ToInt64(apartment.Cost * 1.11);
                    else if(apartment.RegionId == "Бостандыкский") order.Cost = Convert.ToInt64(apartment.Cost * 1.1);
                    else if(apartment.RegionId == "Наурызбайский") order.Cost = Convert.ToInt64(apartment.Cost * 1.09);
                    else if(apartment.RegionId == "Алатауский") order.Cost = Convert.ToInt64(apartment.Cost * 1.08);
                    else if(apartment.RegionId == "Ауэзовский") order.Cost = Convert.ToInt64(apartment.Cost * 1.07);
                    else if(apartment.RegionId == "Жетысуский") order.Cost = Convert.ToInt64(apartment.Cost * 1.06);
                    else order.Cost = Convert.ToInt64(apartment.Cost * 1.05);
                    
                    database.SaveChanges();
                    
                    var query = database.Orders
                        .AsQueryable();

                    return Ok("successful");
                }
                else return BadRequest(result);
            
        }
    }
}