using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CattlewatchMVC.Models
{
    public class LivestockOrder
    {
        public int LivestockOrderId { get; set; }
        public int number { get; set; }

        [DisplayName("Livestock type")]
        [ForeignKey("LivestockId")]
        public Livestock Livestock { get; set; }
        public int LivestockId { get; set; }

        [DisplayName("Order Owner")]
        [ForeignKey("OrderId")]
        public Order Order { get; set; }
        public int OrderId { get; set; }

        [DisplayName("Location")]
        [ForeignKey("LocationId")]
        public Location Location { get; set; }
        public int? LocationId { get; set; }

    }
}