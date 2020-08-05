using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace IMS_IMS_MODEL
{
   public class AddItemWBS
    {

        [Required]
        public string ItemName { get; set; }
    }
}
