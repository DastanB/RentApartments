using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Final.Models;
using Microsoft.AspNetCore.Mvc;

namespace Final.Controllers
{
    [Route("api/[controller]")]
    public class ClientController : Controller
    {
        private FinalDbContext database;

        public ClientController(FinalDbContext context)
        {
            database = context;
        }

        [HttpGet]
        public IActionResult GetClients()
        {
            var query = database.Clients
                .AsQueryable();
            
            return Ok(query.ToList());
        }

        [HttpGet("{id}")]
        public IActionResult GetClient(int Id)
        {
            var query = database.Clients
                .AsQueryable();

            query = query.Where(x => x.Id == Id);
            
            return Ok(query.ToList());
        }

        [HttpGet]
        [Route("By")]
        public IActionResult GetClientByName([FromQuery]string name, [FromQuery]string surname)
        {
            var query = database.Clients
                .AsQueryable();

            var query1 = query.Where(x => x.Name.Contains(name) && x.SurName.Contains(surname));
            
            return Ok(query1.ToList());
        }

        [HttpPost]
        public IActionResult PostClient([FromBody]Client client)
        {
            var result = Manager.ValidateClient(client);
            if(result == "success")
            {
                database.Clients.Add(client);
                database.SaveChanges();
                
                var query = database.Clients
                    .AsQueryable();

                return Ok(query.ToList());
            }
            else return BadRequest(result);
        }
 
        [HttpDelete("{id}")]
        public IActionResult DeleteClient(int id)
        {
            var item = database.Clients.First(x => x.Id == id);
            database.Clients.Remove(item);
            database.SaveChanges();

            var query = database.Clients
                .AsQueryable();

            return Ok(query.ToList());

        }

        [HttpPut("{id}")]
        public IActionResult Put([FromBody] Client client, int id)
        {
            var result = Manager.ValidateClient(client);
            if(result == "success")
            {
                var item = database.Clients.FirstOrDefault(x => x.Id == id);
                item.Name = client.Name;
                item.SurName = client.SurName;
                item.TelNumber = client.TelNumber;
                

                database.SaveChanges();
                
                var query = database.Clients
                    .AsQueryable();

                return Ok(query.ToList());
            }
            else return BadRequest(result);
        }
    }
}