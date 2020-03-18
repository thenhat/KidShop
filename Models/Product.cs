using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace KidShop.Models
{
    public class Product
    {
        [Key]
        [Required(ErrorMessage = "Please enter ProductId.")]
        public string ProductId { get; set; }
        [Required(ErrorMessage = "Please enter ProductName.")]
        public string ProductName { get; set; }
        [Required(ErrorMessage = "Please enter Description.")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Please enter Price.")]
        public double Price { get; set; }
        [Required(ErrorMessage = "Please enter Thumbnail.")]
        public string Thumbnail { get; set; }
        [ForeignKey("Category")]
        public string CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}