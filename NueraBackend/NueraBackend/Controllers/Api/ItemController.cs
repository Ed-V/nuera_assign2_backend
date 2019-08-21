using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using Newtonsoft.Json;
using NueraBackend.Models;
using NueraBackend.Models.DTO;


namespace NueraBackend.Controllers
{
    public class ItemController : ApiController
    {
        // GET api/<controller>
        public IHttpActionResult Get()
        {
            return Ok();
        }


        // POST api/<controller>
        public IHttpActionResult Post(List<ItemDTO> itemDTO)
        {
            NeuraBackendEntities dbContext = new NeuraBackendEntities();
            foreach (var element in itemDTO)
            {
                Item newItem = new Item
                {
                    ItemId = element.ItemId,
                    Name = element.Name,
                    Category = element.Category,
                    Value = element.Value
                };

                dbContext.Items.Add(newItem);
            }

            try
            {
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return BadRequest("Unable to save items");
            }
            


            return Ok("Items sucessfully saved");
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}