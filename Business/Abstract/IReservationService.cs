﻿using Core.Helper.Result.Abstract;
using Entities.Concrete;
using Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IReservationService
    {
        IResult Add(Reservation reservation);
        IDataResult<List<GetReservationDto>> GetAllReservationByCarId(int id);
    }
}
