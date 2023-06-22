using System;
using System.ComponentModel.DataAnnotations;

namespace StoryAPI.Models
{
	public class Category
	{
		[Key]
		public int CategoryId { get; set; }
		public string CategoryName { get; set; }
	}
}

