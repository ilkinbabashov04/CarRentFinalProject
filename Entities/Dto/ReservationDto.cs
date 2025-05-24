using Core.Entity.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dto
{
    public class ReservationDto : IDto
    {
        public int CarId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Mail { get; set; }
        public string Phone { get; set; }
        public int Age { get; set; }
        public int DriverLicenceYear { get; set; }
        public string? Description { get; set; }
        public int? PickUpLocationId { get; set; }
        public int? DropOffLocationId { get; set; }
        public DateTime PickUpDate { get; set; }
        public string PickUpTime { get; set; }
        public DateTime DropOffDate { get; set; }
        public string DropOffTime { get; set; }

        public string Status { get; set; } = "Reservation accepted";

    }
}
