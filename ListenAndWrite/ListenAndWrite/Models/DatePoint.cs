using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListenAndWrite.ModelIdentify
{
    public class DatePoint
    {
        public DateTime date { get; set; }
        public double point { get; set; }
        public int level { get; set; }

        public DatePoint(DateTime date, double point, int level)
        {
            this.date = date;
            this.point = point;
            this.level = level;
        }
    }
}