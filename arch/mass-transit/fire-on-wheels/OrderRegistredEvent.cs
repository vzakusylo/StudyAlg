namespace fire_on_wheels
{
    public class OrderRegistredEvent : IRegisterOrderEvent
    {
        private IRegisterOrderCommand command;
        private int id;

        public OrderRegistredEvent(IRegisterOrderCommand command, int id)
        {
            this.command = command;
            this.id = id;
        }
    }
}