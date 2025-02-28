using Core.Entity.Abstract;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dto
{
    public class BrandDto: IDto
    {
        [JsonProperty("id")]  // This ensures correct mapping from API response
        public int BrandId { get; set; }

        public string Name { get; set; }
    }

}
