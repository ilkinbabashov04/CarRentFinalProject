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
    public interface IBlogService
    {
        IResult Add(Blog blog);
        IResult Update(Blog blog);
        IResult Delete(int id);
        IDataResult<Blog> GetById(int id);
        IDataResult<List<Blog>> GetAll();
        IDataResult<List<BlogDto>> GetLastThreeBlogs();
        IDataResult<List<BlogDto>> GetAllBlogsWithAuthor();
        IDataResult<List<BlogWithAuthorIdDto>> GetBlogWithoutAuthorId(int id);
    }
}
