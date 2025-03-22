﻿using Microsoft.AspNetCore.Identity;

namespace TouristAgency.Model
{
    public class User
    {

        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string UserPhoto { get; set; }
        public Role Role { get; set; }
        


    }
}
