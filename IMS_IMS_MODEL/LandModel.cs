using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace IMS_IMS_MODEL
{
    public class LandModel
    {

       
        //----------------------------------------------------

        [Required(ErrorMessage = "Location is required")]
        [Display(Name = "Location")]
        public string Location { get; set; }
        //----------------------------------------------------


        [Required(ErrorMessage = "Item name is required")]
        [Display(Name = "Item Name")]
        public string ItemName { get; set; }
        //------------------------------------------

        [Required(ErrorMessage = "Sub Location is required")]
        [Display(Name = "Sub Location")]
        public string SubLocation { get; set; }
        //------------------------------------------
        [Required(ErrorMessage = "Length is required")]
        [Display(Name = "Length")]
        public float Length { get; set; }
        //----------------------------------

        [Required(ErrorMessage = "Breadth is required")]
        [Display(Name = "Breadth")]
        public float Breadth { get; set; }
        //----------------------------------

        [Required(ErrorMessage = "Area is required")]
        [Display(Name = "Area")]
        public float Area { get; set; }
       
        //----------------------------------

        [Required(ErrorMessage = "Quantity is required")]
        [Display(Name = "Quantity")]
        public int Quantity { get; set; }

        //----------------------------------
        [Required(ErrorMessage = "Description is required")]
        [Display(Name = "Description")]
        public string Description { get; set; }

        //----------------------------------

    }

   
}