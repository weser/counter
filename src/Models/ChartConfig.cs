using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace booka.counter.Models
{
    public class ChartConfig
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public TimeSpan Step { get; set; }
        public string XAxisFormat { get; set; }
        public int AvgBoxSize { get; set; }

        public ChartConfig(ChartSpan span, DateTime? from = null)
        {
            AvgBoxSize = 7;
            switch (span)
            {
                case ChartSpan.OneDay:
                    From = DateTime.Today;
                    To = DateTime.Now.AddHours(1);
                    Step = TimeSpan.FromHours(1);
                    XAxisFormat = "HH";
                    break;
                case ChartSpan.TwoDays:
                    From = DateTime.Today.AddDays(-1);
                    To = DateTime.Now.AddHours(1);
                    Step = TimeSpan.FromHours(2);
                    XAxisFormat = "HH";
                    break;
                case ChartSpan.Week:
                    From = DateTime.Today.AddDays(-7);
                    To = DateTime.Today.AddDays(1).AddMinutes(-1);
                    Step = TimeSpan.FromHours(8);
                    XAxisFormat = "dd";
                    break;
                case ChartSpan.Month:
                    From = DateTime.Today.AddDays(-30);
                    To = DateTime.Today.AddDays(1).AddMinutes(-1);
                    Step = TimeSpan.FromDays(1);
                    XAxisFormat = "dd";
                    break;
                case ChartSpan.All:
                    From = from.Value;
                    To = DateTime.Today.AddDays(1).AddMinutes(-1);
                    Step = TimeSpan.FromDays(1);
                    XAxisFormat = "dd";
                    break;
            }
        }
    }


    public enum ChartSpan
    {
        OneDay,
        TwoDays,
        Week,
        Month,
        All
    }

    public enum ChartMode
    {
        SevenDayAverage,
        Total,
        Added
    }
}
