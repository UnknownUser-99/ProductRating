﻿namespace ProductRating.Data.Database
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string? Email { get; set; }
        public string Password { get; set; }
        public int Role { get; set; }
        public string? Image { get; set; }
    }
}