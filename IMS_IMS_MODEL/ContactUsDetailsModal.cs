using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace IMS_IMS_MODEL
{
   public class ContactUsDetailsModal
    {

        //----------------------------------------------------

        [Required(ErrorMessage = "First Name is required")]
      
        public string FirstName { get; set; }
        //----------------------------------------------------


        [Required(ErrorMessage = "Last name is required")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        //------------------------------------------
        [Required(ErrorMessage = "Email is required")]
        //[StringLength(16, ErrorMessage = "Must be between 5 and 50 characters", MinimumLength = 5)]
        [RegularExpression("^[a-zA-Z0-9_.-]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$", ErrorMessage = "Must be a valid email")]
        [Display(Name = "Email Address")]
        public string Email { get; set; }


        //------------------------------------------
        [Required(ErrorMessage = "Mobile is required")]
        [Display(Name = "Mobile")]
        public string Mobile { get; set; }
        //----------------------------------

        [Required(ErrorMessage = "Description is required")]
        [Display(Name = "Description")]
        public string Description { get; set; }
        //-----------------------------------------------

    }
}
