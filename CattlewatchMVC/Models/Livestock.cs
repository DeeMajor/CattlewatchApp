using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace CattlewatchMVC.Models
{
    public class Livestock
    {
        public int LivestockId { get; set; }
        [DisplayName("Livestock Name")]
        public string type { get; set; }
    }
}