﻿//using Business.Abstract;
//using Core.Entity.Concrete;
//using DataAccess.Abstract;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Business.Concrete
//{
//    public class UserManager : IUserService
//    {
//        private readonly IUserDal _userDal;

//        public UserManager(IUserDal userDal)
//        {
//            _userDal = userDal;
//        }

//        public List<OperationClaims> GetClaims(User user)
//        {
//            return _userDal.GetClaims(user);
//        }

//        public void Add(User user)
//        {
//            _userDal.Add(user);
//        }

//        public User GetByMail(string email)
//        {
//            return _userDal.Get(u => u.Email == email);
//        }

//    }
//}
