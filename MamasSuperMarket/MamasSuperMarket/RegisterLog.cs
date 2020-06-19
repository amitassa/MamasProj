using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMSuperMarket
{
    public class RegisterLog
    {
        public Nullable<DateTime> StartTime { get; set; }
        public Nullable<DateTime> EndTime { get; set; }
        public Employee UsingEmployee { get;}

        public RegisterLog(DateTime start, Employee emp)
        {
            this.StartTime = start;
            this.UsingEmployee = emp;
            this.EndTime = null;
        }

        public RegisterLog(DateTime start, DateTime end, Employee emp)
        {
            this.StartTime = start;
            this.UsingEmployee = emp;
            this.EndTime = end;
        }

        public RegisterLog()
        {
            this.EndTime = null;
            this.StartTime = null;
            this.UsingEmployee = null;
        }
    }
}
