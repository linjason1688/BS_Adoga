using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BS_Adoga.Models.ViewModels.HotelLogin
{
    public class RoomViewModel
    {
        [StringLength(50)]
        public string RoomID { get; set; }

        [Required]
        [StringLength(50)]
        public string HotelID { get; set; }

        [Required]
        [StringLength(50)]
        public string RoomName { get; set; }

        public int NumberOfPeople { get; set; }

        public int RoomCount { get; set; }

        [Column(TypeName = "money")]
        public decimal RoomPrice { get; set; }

        public int TypesOfBathroomID { get; set; }

        public bool NoSmoking { get; set; }

        public bool Breakfast { get; set; }

        public bool WiFi { get; set; }

        public bool TV { get; set; }

        [Required]
        public string Logging { get; set; }
    }
}