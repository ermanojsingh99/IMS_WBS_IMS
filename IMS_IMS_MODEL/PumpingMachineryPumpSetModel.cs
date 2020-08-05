using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace IMS_IMS_MODEL
{
    public class PumpingMachineryPumpSetModel
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
        [Required(ErrorMessage = "Discharge is required")]
        [Display(Name = "Discharge")]
        public int Discharge { get; set; }
        //----------------------------------

        [Required(ErrorMessage = "Head is required")]
        [Display(Name = "Head")]
       
        public int Head { get; set; }
        //----------------------------------
        [Required(ErrorMessage = "Make is required")]
        [Display(Name = "Make")]
        public string Make { get; set; }
        //----------------------------------

        [Required(ErrorMessage = "Quantity is required")]
        [Display(Name = "Quantity")]
        public int Quantity { get; set; }

       
        //----------------------------------
        [Required(ErrorMessage = " Select Status ")]
        [Display(Name = "Item Status")]
        public string Status { get; set; }

        //----------------------------------
        [Required(ErrorMessage = "Description is required")]
        [Display(Name = "Other Details")]
        public string Description { get; set; }

        //----------------------------------

    }

   
}

public class DDLSubLocation
{
    public int Id { get; set; }
    public string SubLocation { get; set; }

}
