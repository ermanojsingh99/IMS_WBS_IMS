using System.ComponentModel.DataAnnotations;

namespace IMS_IMS_MODEL
{
    public  class WaterBoostingInventory
    {

        [Key]
        public int Id { get; set; }
        //----------------------------------------------------

        [Required(ErrorMessage = "Location is required")]
        [Display(Name = "Location")]
        public string Location { get; set; }
        //----------------------------------------------------
        [Required(ErrorMessage = "Item Type is required")]
        [Display(Name = "Item Type")]
        public string ItemType { get; set; }
        //----------------------------------

        [Required(ErrorMessage = "Category is required")]
        [Display(Name = "Category")]
        public string Category { get; set; }
        //----------------------------------
        [Required(ErrorMessage = "Item name is required")]
        [Display(Name = "Item Name")]
        public string ItemName { get; set; }

        //----------------------------------
        [Required(ErrorMessage = "Pump house name is required")]
        [Display(Name = "Pump House Name")]
        public string PumpHouseName { get; set; }
        //--------------------------------------
        [Required(ErrorMessage = "Pump house name is required")]
        [Display(Name = "Pump House Name")]
        public string PumpHouseWTP { get; set; }

        // [Required(ErrorMessage = "Pump Reference Name is required")]
        [Display(Name = "Pump Reference Name")]
        public string PumpReferenceName { get; set; }
        //----------------------------------
       // [Required(ErrorMessage = "Rated Power is required")]
        [Display(Name = "Rated Power(in HP)")]
        public string RatedPower { get; set; }
        //----------------------------------
      //  [Required(ErrorMessage = "Rated Head is required")]
        [Display(Name = "Rated Head(in Meter)")]
        public string RatedHead { get; set; }
        //----------------------------------
      //  [Required(ErrorMessage = "Rated Flow is required")]
        [Display(Name = "Rated Flow(in M3/Hr.)")]
        public string RatedFlow { get; set; }
        //----------------------------------
        //[Required(ErrorMessage = "Type of Pump is required")]
        [Display(Name = "Pump Type")]
        public string TypeOfPump { get; set; }
        //------------------------------------------
        [Required(ErrorMessage = "Make is required")]
        [Display(Name = "Make")]
        public string Make { get; set; }
        //----------------------------------

        [Required(ErrorMessage = "Quantity is required")]
        [Display(Name = "Quantity")]
        public int Quantity { get; set; }

        //----------------------------------  
        [Required(ErrorMessage = "UOM is required")]
        [Display(Name = "Unit Of Measurement")]
        public string UOM { get; set; }
        //----------------------------------

        [Required(ErrorMessage = "Other Details is required")]
        [Display(Name = "Other Details")]
        public string Description { get; set; }
    }
}

public class location
{
    public int Id { get; set; }
    public string locationName { get; set; }

}


public class DDLCategory
{
    public int Id { get; set; }
    public string Category { get; set; }

}