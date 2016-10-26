using System.Collections.Generic;
using Infrastructure;

namespace AlertsLib
{
    /// <summary>
    /// User can set alert rules, that will be checked with ProcessMonitor.
    /// </summary>
    public class AlertMonitor
    {
        private readonly List<IAlertRule> _rules = new List<IAlertRule>();

        /// <summary>
        /// Alerts that passed with IClient.Alert
        /// </summary>
        public List<string> Alerts { get; set; } = new List<string>();

        public bool IsAlert { get { return Alerts.Count != 0; } }

        /// <summary>
        /// Configure AlertMonitor with rules.
        /// User can extend alert library and add custom rules. 
        /// </summary>
        /// <param name="rule">Alert rule</param>
        public void AddAlertRule(IAlertRule rule)
        {
            _rules.Add(rule);
        }

        /// <summary>
        /// Check process data with alert rules.
        /// </summary>
        /// <param name="p">Process data prepared by ProcessMonitor</param>
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
