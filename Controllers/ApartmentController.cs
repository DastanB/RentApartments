using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Final.Models;
using Microsoft.AspNetCore.Mvc;

namespace Final.Controllers
{
    [Route("api/[controller]")]
    public class ApartmentController : Controller
    {
        private FinalDbContext database;

        public ApartmentController(FinalDbContext context)
        {
            database = context;
        }

        [HttpGet]
        public IActionResult GetApartments()
        {
            var query = database.Apartments
                .AsQueryable();
            
            return Ok(query.ToList());
        }

        [HttpGet]
        [Route("Only")]
        public IActionResult GetApartment([FromQuery]int id)
        {
            var query = database.Apartments
                .AsQueryable();

            query = query.Where(x => x.Id == id);
            
            return Ok(query.ToList());
        }

        [HttpGet]
        [Route("Almaty")]
        public IActionResult GetAprtByReg([FromQuery]string region)
        {
            var query = database.Apartments
                .AsQueryable();

            query = query.Where(x => x.RegionId.Equals(region));
            
            return Ok(query.ToList());
        }

        [HttpGet]
        [Route("Cost")]
        public IActionResult GetAprtByCost([FromQuery]int from, [FromQuery]int to)
        {
            long i = 999999999999999999;
            if(from < to)
            {
                if(to < i)
                {
                    var query = database.Apartments
                    .AsQueryable();
                    var apartments = query.ToList();
                    var aprs = new List<Apartment>();
                    foreach(var apr in apartments)
                    {
                        if(apr.Cost >= from && apr.Cost <= to)
                        {
                            aprs.Add(apr);
                        }
                    }
                    
                    return Ok(aprs);
                }
                else return BadRequest("too big number for 'to");
            }
            else return BadRequest("'to' must be grater than 'from'");
        }

        [HttpGet]
        [Route("OrderedBy")]
        public IActionResult GetAprtOrdered()
        {
            var query = database.Apartments
                .AsQueryable();

            var query1 = query.GroupBy(x => x.RegionId);
            
            return Ok(query1);
        }

        [HttpPost]
        public IActionResult PostApartment([FromBody]Apartment apartment)
        {
            var result = Manager.ValidateApartment(apartment);
            if(result == "success")
            {
                database.Apartments.Add(apartment);
                database.SaveChanges();
                
                var query = database.Apartments
                    .AsQueryable();
                return Ok(query.ToList());        
            }
            else return BadRequest(result);
            
        }
 
        [HttpDelete("{id}")]
        public IActionResult DeleteApartment(int id)
        {
            var item = database.Apartments.First(x => x.Id == id);
            database.Apartments.Remove(item);
            database.SaveChanges();

            var query = database.Apartments
                .AsQueryable();

            return Ok(query.ToList());

        }

        [HttpPut("{id}")]
        public IActionResult Put([FromBody] Apartment apartment, int id)
        {
            var result = Manager.ValidateApartment(apartment);
            if(result == "success")
            {    
                var item = database.Apartments.FirstOrDefault(x => x.Id == id);
                item.Rooms = apartment.Rooms;
                item.Cost = apartment.Cost;
                item.RegionId = apartment.RegionId;
                item.Description = apartment.Description;

                database.SaveChanges();
                
                var query = database.Apartments
                    .AsQueryable();

                return Ok(query.ToList());
            }
            else return BadRequest(result);
        }
    }
}