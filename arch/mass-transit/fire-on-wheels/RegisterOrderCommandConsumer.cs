using System.Threading.Tasks;
using MassTransit;

namespace fire_on_wheels
{
    public class RegisterOrderCommandConsumer : IConsumer<IRegisterOrderCommand>
    {
        async Task IConsumer<IRegisterOrderCommand>.Consume(ConsumeContext<IRegisterOrderCommand> context)
        {
            var command = context.Message;

            var id = 12;

            await System.Console.Out.WriteLineAsync($"Order with id {id} register");

            var orderRegistredEvent = new OrderRegistredEvent(command, id);

            await context.Publish<IRegisterOrderEvent>(orderRegistredEvent);
        }
    }
}