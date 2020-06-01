using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CattlewatchMVC.Models
{
    public class Location
    {
        public int LocationId { get; set; }
        
        public string CityName { get; set; }
        
        public string Latitude { get; set; }
      
        public string longitude { get; set; }

        public string Description { get; set; }

        
    }
}