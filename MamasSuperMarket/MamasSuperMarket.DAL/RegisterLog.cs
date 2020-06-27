using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MamasSuperMarket.DAL
{
    public class RegisterLog
    {
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public Employee UsingEmployee { get;}

        public RegisterLog(DateTime start, Employee emp)
        {
            this.StartTime = start;
            this.UsingEmployee = emp;
        }

        public RegisterLog(DateTime start, DateTime end, Employee emp)
        {
            this.StartTime = start;
            this.UsingEmployee = emp;
            this.EndTime = end;
        }

        public RegisterLog()
        {
            this.UsingEmployee = null;
        }
    }
}
