using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProductCRUDCart.Models.Entities
{
    public class Product
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Please give a name!")]
        [StringLength(20, ErrorMessage ="Name should not exceed 10 character")]
        [MinLength(5)]
        public string Name { get; set; }
        [Required]
        public int Qty { get; set; }
        [Required]
        public float Price { get; set; }
        public string Descr { get; set; }
    }
}