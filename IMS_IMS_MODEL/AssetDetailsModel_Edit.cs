using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace IMS_IMS_MODEL
{
  public  class AssetDetailsModel_Edit
    {

        [Required(ErrorMessage = "Employee ID is required")]

        public string EmpId { get; set; }

        [Required(ErrorMessage = "Asset unique id is required")]

        public string AssetId_unique { get; set; }
        [Required(ErrorMessage = "Asset Name is required")]
        public string AssetName { get; set; }

        [Required(ErrorMessage = "Serial No. is required")]
        public string SerialNo { get; set; }

        public string IpAddress { get; set; }

        public string MacAddress { get; set; }
        public string HDD { get; set; }
        public string RAM { get; set; }
        public string Processor { get; set; }

        [Required(ErrorMessage = "Warranty Start date is required")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime WarrantyStart { get; set; }
        [Required(ErrorMessage = "Warranty end date is required")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime WarrantyEnd { get; set; }
        [Required(ErrorMessage = "Date of issue is required")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateOfIssue { get; set; }
    }
}
