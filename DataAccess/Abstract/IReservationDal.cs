﻿using Core.DataAccess.Abstract;
using DataAccess.Concrete.EF;
using Entities.Concrete;
using Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IReservationDal : IBaseReporsitory<Reservation>
    {
        List<GetReservationDto> GetAllReservationByCarId(int id);
    }
}
