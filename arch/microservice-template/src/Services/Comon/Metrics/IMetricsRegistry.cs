namespace Usavc.Microservices.Appointment.Metrics
{
    public interface IMetricsRegistry
    {
        void IncrementFindDiscountsQuery();
    }
}