using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodingTracker.Model
{
    public class CodeItem
    {
        public long Id { get; set; }
        public int Duration { get; set; }
        public String StartTime { get; set; }
        public String EndTime { get; set; }

        public CodeItem(int duration, string startTime, string endTime)
        {
            Duration = duration;
            StartTime = startTime;
            EndTime = endTime;
        }

    }
}
