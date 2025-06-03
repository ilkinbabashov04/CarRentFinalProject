using Core.DataAccess.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EF
{
    public class EfReservationDal : BaseReporsitory<Reservation, BaseProjectContext>, IReservationDal
    {
        public EfReservationDal(BaseProjectContext context) : base(context)
        {
        }

        public List<GetReservationDto> GetAllReservationByCarId(int id)
        {
            using var context = new BaseProjectContext();

            var result = context.Reservations
                .Where(r => r.CarId == id && r.IsDelete == false)
                .Select(r => new GetReservationDto
                {
                    Id = r.Id,
                    PickUpDate = r.PickUpDate,
                    DropOffDate = r.DropOffDate,
                })
                .ToList();

            return result;
        }

    }
}
