using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Task.Models
{
    public class ProductModel
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; } = DateTime.Today;

        [Required]
        public string Name { get; set; }

    }
}
