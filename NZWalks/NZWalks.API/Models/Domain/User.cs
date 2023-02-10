﻿using System.ComponentModel.DataAnnotations.Schema;

namespace NZWalks.API.Models.Domain
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [NotMapped]
        public List<string> Roles { get; set; } //This field is added post migration and is not required in the database, Hence we are marking it as [NotMapped]
        //Navigation Property
        public List<User_Role> UserRoles { get; set; }
    }
}
