using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CattlewatchMVC.Models
{
    
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        public string ClinetName { get; set; }
        public string CLinetId { get; set; }
        public string SameLocation { get; set; }
        public int TotalAnimals { get; set; }
        public string OrderStatus { get; set; }
        public List<string> SelectedLivestock { get; set; }
        public List<string> Location { get; set; }

    }
}