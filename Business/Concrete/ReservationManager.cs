using Business.Abstract;
using Core.Helper.Result.Abstract;
using Core.Helper.Result.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EF;
using Entities.Concrete;
using Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ReservationManager(IReservationDal reservationDal) : IReservationService
    {
        private readonly IReservationDal _reservationDal = reservationDal;
        public IResult Add(Reservation reservation)
        {
            _reservationDal.Add(reservation);
            return new SuccessResult("Successfully added!");
        }

        public IDataResult<List<GetReservationDto>> GetAllReservationByCarId(int id)
        {
            var result = _reservationDal.GetAllReservationByCarId(id);
            if (result.Count > 0)
            {
                return new SuccessDataResult<List<GetReservationDto>>(result, "Got Successfully!");
            }
            else
            {
                return new ErrorDataResult<List<GetReservationDto>>(result, "Not found!");
            }
        }
    }
}
