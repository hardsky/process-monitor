using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure;

namespace ProcessMonitor
{
    public class AlertMonitor
    {
        public List<String> Alerts { get; set; } = new List<string>();
        public bool IsAlert
        {
            get
            {
                return Alerts.Count != 0;
            }
        }

        private readonly List<IAlertRule> _rules = new List<IAlertRule>();

        public void AddAlertRule(IAlertRule rule)
        {
            _rules.Add(rule);
        }

        public void Check(ProcessData p)
        {
            foreach (var rule in _rules)
            {
                var alert = rule.Check(p);
                if (alert != null)
                    Alerts.Add(alert);
            }
        }
    }
}
