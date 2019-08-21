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
        /// <summary>Returns the list of items from the database. See example for json format, using get request</summary>
        /// <returns>List containing items in JSON format</returns>
        /// <example>
        ///   <para>[{"name":"TV", "itemId":"dssdf-sdfsdfsd-sdfsdf", "value": 200, "category" : "Electronics"},</para>
        ///   <para>{"name":"Computer", "itemId":"adfadsf-asdfasd-esdf", "value": 5000, "category" : "Electronics"}]
        /// </para>
        ///   <code></code>
        /// </example>
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
        /// <summary>Create new items, see example for format, using post request</summary>
        /// <param name="itemDTO">The item dto.</param>
        /// <returns>Ok status code</returns>
        /// <example>[{"name":"Microwave", "itemId":"dssdf-sdfsdfsd-sdfsdf", "value": 265, "category" : "Kitchen"}]
        /// <code></code></example>
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
        /// <summary>Update the item, see example for format, using put request</summary>
        /// <param name="item">The item.</param>
        /// <returns>Status message</returns>
        /// <example>{"name":"Chair", "itemId":"dssdf-sdfsdfsd-sdfsdf", "value": 299, "category" : "Kitchen"}
        /// <code></code></example>
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
        /// <summary>Deletes the specified item, using delete request.</summary>
        /// <param name="id">Item ID.</param>
        /// <returns>Status message</returns>
        /// <example>http://localhost:53305/api/item/dssdf-sdfsdfsd-sdfsdf
        /// <code></code></example>
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