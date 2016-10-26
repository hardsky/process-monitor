namespace Infrastructure
{
    /// <summary>
    /// Describe rules for alert geterator using this interface
    /// </summary>
    public interface IAlertRule
    {
        /// <summary>
        /// Check process data with this rule
        /// </summary>
        /// <param name="p">process data prepared by ProcessMonitor</param>
        /// <returns>Alert description.</returns>
        string Check(ProcessData p);
    }
}
