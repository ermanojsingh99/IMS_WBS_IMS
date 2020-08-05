using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace IMS_IMS_MODEL
{
    public class WTPModel
    {

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
        [Required(ErrorMessage = "Diameter is required")]
        [Display(Name = "Diameter")]
        public int Diameter { get; set; }
        //----------------------------------

        [Required(ErrorMessage = "Capacity is required")]
        [Display(Name = "Capacity")]
        public int Capacity { get; set; }
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