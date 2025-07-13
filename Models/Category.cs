﻿using SQLite;

namespace w12.Models
{
    public class Category
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; } 
        public string Name { get; set; } = string.Empty;
    }
}
