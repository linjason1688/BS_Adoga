using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BS_Adoga.Models.ViewModels.Search
{
    public class FacilityViewModel
    {
        public int FacilitieID { get; set; }
        public string HotelID { get; set; }
        //住宿設施
        public bool SwimmingPool { get; set; }
        public bool AirportTransfer { get; set; }
        public bool FamilyChildFriendly { get; set; }
        public bool Restaurants { get; set; }
        public bool Nightclub { get; set; }
        public bool GolfCourse { get; set; }
        public bool Gym { get; set; }
        public bool NoSmoking { get; set; }
        public bool SmokingArea { get; set; }
        public bool FacilitiesFordisabledGuests { get; set; }
        public bool CarPark { get; set; }
        public bool FrontDesk { get; set; }
        public bool SpaSauna { get; set; }
        public bool BusinessFacilities { get; set; }

        //客房設施
        public bool Internet { get; set; }
        public bool PetsAllowed { get; set; }
    }
}