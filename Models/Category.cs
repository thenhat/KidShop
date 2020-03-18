using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KidShop.Models
{
    public class Category
    {
        [Key]
        [Required(ErrorMessage = "Please enter CategoryId.")]
        public string CategoryId { get; set; }
        [Required(ErrorMessage = "Please enter CategoryName.")]
        public String CategoryName { get; set; }
        public List<Product> Products { get; set; }
    }
}