using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace NueraBackend.Models.DTO
{
    public class ItemDTO
    {
        [JsonProperty(PropertyName ="itemName")]
        public string ItemName { get; set; }

        [JsonProperty(PropertyName = "itemName")]
        public string ItemId { get; set; }

        [JsonProperty(PropertyName = "value")]
        public int Value { get; set; }

        [JsonProperty(PropertyName = "category")]
        public string Category { get; set; }
    }
}