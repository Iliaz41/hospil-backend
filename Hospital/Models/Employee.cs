﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace Hospital.Models
{
    public class Employee
    {
        [Key]
        public long Id { get; set; }

        public string? Name { get; set; }

        public string? Surname { get; set; }

        public string? Email { get; set; }

        public string? Password { get; set; }

        public string? Passport { get; set; }

        public string? City { get; set; }

        public string? Street { get; set; }

        public string? House { get; set; }

        public string? Flat { get; set; }

        public Role Role { get; set; }

        public long? Phone { get; set; }

        public int? Hours { get; set; }

        public long? Money { get; set; }

        public int? LoginTime { get; set; }
    }
}
