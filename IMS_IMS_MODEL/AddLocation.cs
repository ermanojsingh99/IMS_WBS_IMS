using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace IMS_IMS_MODEL
{
   public class AddLocation
    {
        [Required(ErrorMessage = "Location required")]
        public string LocationName { get; set; }

    }
}
