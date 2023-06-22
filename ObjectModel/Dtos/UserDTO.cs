﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectModel.Dtos
{
    public class UserDTO
    {
		public int UserId { get; set; }

		public string FullName { get; set; }

		public string UserName { get; set; }

		public string ImageUser { get; set; }

		public string Email { get; set; }

		public string Password { get; set; }

		public bool IsMale { get; set; }

		public int RoleId { get; set; }

		public bool IsActive { get; set; }
	}
}
