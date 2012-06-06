﻿using System.ComponentModel.DataAnnotations;

namespace Effort.Test.Data.DbContextSchema
{
    public class Person
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
