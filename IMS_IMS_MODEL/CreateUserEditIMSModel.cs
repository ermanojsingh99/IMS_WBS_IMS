using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace IMS_IMS_MODEL
{
  public  class CreateUserEditIMSModel
    {

        [Required(ErrorMessage = "User ID is required")]
        [Display(Name = "User Id")]
        public int UserId { get; set; }
        [Required(ErrorMessage = "User name is required")]
        [Display(Name = "User Name")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [RegularExpression("^[a-zA-Z0-9_.-]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$", ErrorMessage = "Must be a valid email")]
        [Display(Name = "Email Address")]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is required")]
        [StringLength(255, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 5)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Confirm Password is required")]
        [StringLength(255, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 5)]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password does not match")]
        [Display(Name = "Confirm Password")]
        public string RetypePassword { get; set; }

        //[Required(ErrorMessage = "Location is required")]
        //[Display(Name = "Location")]
        //public string Location { get; set; }


        //[Required(ErrorMessage = "Role is required")]
        //[Display(Name = "User Role")]
        //public string Role { get; set; }

    }
}
