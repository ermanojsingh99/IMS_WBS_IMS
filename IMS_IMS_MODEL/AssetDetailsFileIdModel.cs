using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace IMS_IMS_MODEL
{
   public class AssetDetailsFileIdModel
    {
        

        [Required(ErrorMessage = "Employee id is required")]
        [Display(Name = "Employee Id")]
        public string _EmpId { get; set; }
      
        //public string FileName { get; set; }
        //public string FullPath { get; set; }

        //[DataType(DataType.Upload)]
        //[Display(Name = "Upload File")]
        //[Required(ErrorMessage = "Please choose file to upload.")]
        //[NotMapped]
        //public HttpPostedFileBase UploadAgreement { get; set; }

       
        //Above properties are for AssetID Table
    }
}
