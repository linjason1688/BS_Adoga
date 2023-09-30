namespace BS_Adoga.Models.ViewModels.HotelLogin
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public class HotelListViewModel
    {
        [Display(Name = "�����s��")]
        public string HotelID { get; set; }

        [Display(Name = "�����W��")]
        public string HotelName { get; set; }

        [Display(Name = "�^��W��")]
        public string HotelEngName { get; set; }

        [Display(Name = "����")]
        public string HotelCity { get; set; }

        [Display(Name = "�ϰ�")]
        public string HotelDistrict { get; set; }

        [Display(Name = "�a�}")]
        public string HotelAddress { get; set; }

    }
}
