using Microsoft.AspNetCore.Identity;
using System;
namespace URLShortener.Models
{
	public class User : IdentityUser
	{
		public String? FullName {  get; set; }
		public List<URL> Urls { get; set; }	
		public User()
		{
			Urls = new List<URL>();
		}
	}
}

