﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace IMS_IMS_MODEL
{
    public class ValveModel
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
        [Required(ErrorMessage = "Valve Type is required")]
        [Display(Name = "Valve Type")]
        public string ValveType { get; set; }


        //------------------------------------------
        [Required(ErrorMessage = "Size is required")]
        [Display(Name = "Size")]
        public int Size { get; set; }
        //----------------------------------

      
        [Required(ErrorMessage = "Make is required")]
        [Display(Name = "Make")]
        public string Make { get; set; }
        //----------------------------------

        [Required(ErrorMessage = "Quantity is required")]
        [Display(Name = "Quantity")]
        public int Quantity { get; set; }

        //----------------------------------  
     
        //[Required(ErrorMessage = "Status is required")]
        //[Display(Name = "Status")]
        //public string Status { get; set; }

        //----------------------------------
        [Required(ErrorMessage = "Description is required")]
        [Display(Name = "Description")]
        public string Description { get; set; }

        //----------------------------------

    }

   
}

public class DDLValveType
{
    public int Id { get; set; }
    public string ValveType { get; set; }

}