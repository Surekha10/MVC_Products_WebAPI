using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MVC_Product_Assignment.Models
{
    public class ProductModel
    {
        [Display(Name = "Product ID")]//Label Text
        public int ProductID { get; set; }
        [Display(Name = ("Product Name"))]
        [Required(ErrorMessage = "*")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Too Short Name")]
        public string ProductName { get; set; }
        [Display(Name = ("Product Price"))]
        [Required(ErrorMessage = "*")]
        public int ProductPrice { get; set; }
        [Display(Name = ("Product Category"))]
        [Required(ErrorMessage = "*")]
        public string ProductCategory { get; set; }
        public string ProductImageAddress { get; set; }
        [Display(Name = "Product Image")]
        [Required(ErrorMessage = "*")]
        //[Range(10,int.MaxValue,ErrorMessage ="")]
        public HttpPostedFileBase ProductImageFile { get; set; }
    }
}