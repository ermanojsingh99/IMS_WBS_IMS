using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace IMS_IMS_MODEL
{
    public class PipeModel
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
        [Required(ErrorMessage = "Type of Pipe is required")]
        [Display(Name = "type of Pipe")]
        public string TypeOfPipe { get; set; }
        //------------------------------------------
        [Required(ErrorMessage = "Diameter is required")]
        [Display(Name = "Diameter")]
        public float Diameter { get; set; }
        //----------------------------------

        [Required(ErrorMessage = "Length is required")]
        [Display(Name = "Length")]
        public float Length { get; set; }
        //----------------------------------
        [Required(ErrorMessage = "Make is required")]
        [Display(Name = "Make")]
        public string Make { get; set; }
        //----------------------------------

        [Required(ErrorMessage = "Quantity is required")]
        [Display(Name = "Quantity")]
        public int Quantity { get; set; }

      
        //----------------------------------
        [Required(ErrorMessage = "Status is required")]
        [Display(Name = "Status")]
        public string Status { get; set; }

        //----------------------------------
        [Required(ErrorMessage = "Description is required")]
        [Display(Name = "Description")]
        public string Description { get; set; }

        //----------------------------------

    }

   
}
public class DDLPipeType
{
    public int Id { get; set; }
    public string PumpType { get; set; }

}