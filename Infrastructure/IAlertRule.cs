namespace Infrastructure
{
    public interface IAlertRule
    {
        string Check(ProcessData p);
    }
}
