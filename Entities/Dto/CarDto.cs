using Core.Entity.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dto
{
	public class CarDto : IDto
	{
		//[JsonProperty("id")]  // This ensures correct mapping from API response
		public int id { get; set; }
        public int BrandId { get; set; }
        public string BrandName { get; set; }
        public string Model { get; set; }
		public string CoverImageUrl { get; set; }
		public int Km { get; set; }
		public string Transmission { get; set; }
		public int Seat { get; set; }
		public int Luggage { get; set; }
		public string Fuel { get; set; }
		public string BigImageUrl { get; set; }
        public decimal Amount { get; set; }
    }
}
