﻿namespace Ui.Models
{
	public class JwtResponseModel
	{
        public string Token { get; set; }
        public DateTime ExpireDate { get; set; }
        public string Role { get; set; }
    }
}
