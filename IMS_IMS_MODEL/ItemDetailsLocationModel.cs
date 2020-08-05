using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace IMS_IMS_MODEL
{
    public  class ItemDetailsLocationModel
    {

        //----------------------------------------------------

        [Required(ErrorMessage = "Itemcode is required")]
        [Display(Name = "Item Name")]
        public string ItemName { get; set; }
        //----------------------------------------------------

        [Required(ErrorMessage = "Location is required")]
        [Display(Name = "Location")]
        public string Location { get; set; }
        //----------------------------------------------------
    }
}
