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
        [HttpGet]
        public IHttpActionResult Get()
        {
            var dbContext = new NeuraBackendEntities();
            var items = dbContext.Items.ToList();

            //Used to convert c# casing to json. Cannot directly edit the .cs class using the database first approach
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Item, ItemDTO>();
            });
            IMapper iMapper = config.CreateMapper();
            var mappedResult = iMapper.Map<List<Item>, List<ItemDTO>>(items);


            var result = JsonConvert.SerializeObject(mappedResult);
            return Ok(result);
        }


        // POST api/<controller>
        [HttpPost]
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
        [HttpPut]
        public IHttpActionResult Put(ItemDTO item)
        {

            var dbContext = new NeuraBackendEntities();

            var foundItem = dbContext.Items.FirstOrDefault(i => i.ItemId == item.ItemId);

            if (foundItem == null)
            {
                return BadRequest("Item not found");
            }

            foundItem.Name = item.Name;
            foundItem.Category = item.Category;
            foundItem.Value = item.Value;

            try
            {
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return BadRequest("Unable to update item");
            }



            return Ok("Item sucessfully updated");
        }

        // DELETE api/<controller>/5
        [HttpDelete]
        public IHttpActionResult Delete(string id)
        {

            var dbContext = new NeuraBackendEntities();

            var foundItem = dbContext.Items.FirstOrDefault(i => i.ItemId == id);

            if (foundItem == null)
            {
                return BadRequest("Item not found");
            }

            dbContext.Items.Remove(foundItem);

            try
            {
                dbContext.SaveChanges();
            }
            catch (Exception e)
            {
                return BadRequest("Unable to remove item");
            }


            return Ok("Item removed sucessfully");
        }
    }
}