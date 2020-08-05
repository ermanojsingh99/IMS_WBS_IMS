using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace IMS_IMS_MODEL
{
    public class PumpingMachineryMotorModel
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
        [Required(ErrorMessage = "BHP is required")]
        [Display(Name = "BHP")]
        public int BHP { get; set; }
       
        //----------------------------------
        [Required(ErrorMessage = "Make is required")]
        [Display(Name = "Make")]
        public string Make { get; set; }

        //----------------------------------
        [Required(ErrorMessage = "Item Type is required")]
        [Display(Name = "Item Type")]
        public string Itemtype { get; set; }
        //----------------------------------

        [Required(ErrorMessage = "Quantity is required")]
        [Display(Name = "Quantity")]
        public int Quantity { get; set; }

       
        //----------------------------------
        [Required(ErrorMessage = "Other Status is required")]
        [Display(Name = "Item Status")]
        public string Status { get; set; }

        //----------------------------------
        [Required(ErrorMessage = "Other Details is required")]
        [Display(Name = "Other Details")]
        public string Description { get; set; }

        //----------------------------------

        //public WBSItemAssetModel WBSItemAssetModel { get; set; }

    }

   
}

public class DDLItemType
{
    public int Id { get; set; }
    public string ItemType { get; set; }

}

public class DDLItemCategory
{
    public int Id { get; set; }
    public string Category { get; set; }

}

public class Status
{
    public int Id { get; set; }
    public string status { get; set; }

}