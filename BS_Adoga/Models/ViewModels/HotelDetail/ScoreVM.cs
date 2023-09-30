using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BS_Adoga.Models.ViewModels.HotelDetail
{
    public class ScoreVM
    {
        public decimal ScoreAvg { get; set; }

        public string ScoreLevel { get; set; }

        public int AllMessageCount { get; set; }

        public int HighScoreMessageCount { get; set; }

        public int HighScorePercent { get; set; }
    }
}