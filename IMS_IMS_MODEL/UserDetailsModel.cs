using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS_IMS_MODEL
{
   public class UserDetailsModel
    {

        public string EmployeeName { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
      

        public AssetDetailsModel UserAllDetails { get; set; }

    }
}
