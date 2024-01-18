﻿using System.ComponentModel.DataAnnotations;

namespace HopeConnect.Customer.Api.Infrastructure.Model
{
	public class User
	{
		[Key]
		public int Id { get; set; }
		public string? FirebaseUserId { get; set; }
		public string? Email { get; set; }
		public string? FullName { get; set; }		
	}
}
