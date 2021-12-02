﻿using Microsoft.AspNetCore.Identity;

namespace DAL
{
    public class User : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual UserProfile UserProfile { get; set; }
    }
}
