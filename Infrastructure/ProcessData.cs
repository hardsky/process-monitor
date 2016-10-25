using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    [Serializable]
    public class ProcessData
    {
        public int Id { get; set; }
        public int Priority { get; set; } //BasePriority
    }
}
