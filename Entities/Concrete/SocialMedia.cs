﻿using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
	public class SocialMedia : BaseEntity
	{
        public string Name { get; set; }
        public string Url { get; set; }
        public string IconUrl { get; set; }
    }
}
