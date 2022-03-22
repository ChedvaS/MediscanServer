using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    public class activityReminders
    {
        public string MedicineName { get; set; }
        public short LeftDays { get; set; }
        public List<Nullable<System.DateTime>>  TakingTimes { get; set; }
        public  Nullable<short> frequincy { get; set; }

        public string comment { get; set; }
    }
}
