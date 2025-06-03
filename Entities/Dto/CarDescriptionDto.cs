using Core.Entity.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dto
{
    public class CarDescriptionDto : IDto
    {
        public int Id { get; set; }
        public int CarId { get; set; }
        public string Detail { get; set; }
        public bool IsDelete { get; set; }
    }
}
