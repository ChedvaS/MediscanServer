﻿using System;
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
        public Nullable<System.DateTime>  NextTakingTime { get; set; }
    }
}