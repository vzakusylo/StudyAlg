using System;
using System.Collections.Generic;
using System.Globalization;

namespace Mediator
{
    class Program
    {
        static void Main(string[] args)
        {
            Chatroom chatroom = new Chatroom();

            Participant Eddie = new Actor("Eddie");
            Participant Jen = new Actor("Jen");
            Participant Bruce = new Actor("Bruce");
            Participant Tom = new Actor("Tom");
            Participant Tony = new Actor("Tony");

            chatroom.Register(Eddie);
            chatroom.Register(Jen);
            chatroom.Register(Bruce);
            chatroom.Register(Tom);
            chatroom.Register(Tony);


            Tony.Send("Tom", "Hey Tom! I got a mission for you.");
            Jen.Send("Bruce", "Teach me to act and I'll teach you dance.");
            Bruce.Send("Eddie", "How come you don't do stanup anymore.");
            Jen.Send("Eddie", "Do you like math.");
            Tom.Send("Tony", "Teach me to sing");

            Console.ReadKey();
        }

    
    }

    // the 'Mediator' 
    public abstract class AbstractChatroom
    {
        public abstract void Register(Participant participant);
        public abstract void Send(string from, string to, string message);
    }

    class Chatroom : AbstractChatroom
    {
        private Dictionary<string, Participant> _participants = new Dictionary<string, Participant>();

        public override void Register(Participant participant)
        {
            if (!_participants.ContainsValue(participant))
            {
                _participants[participant.Name] = participant;
            }

            participant.Chatroom = this;
        }

        public override void Send(string from, string to, string message)
        {
            Participant participant = _participants[to];
            participant?.Receive(from, message);
        }
    }

    public class Participant
    {
        public Participant(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }
        public AbstractChatroom Chatroom { get; set; }

        public virtual void Receive(string from, string message)
        {
            Console.WriteLine($"{from} {Name} {message}" );
        }

        public void Send(string to, string message)
        {
            Chatroom.Send(Name, to, message);
        }
    }

    public class Actor : Participant
    {
        public Actor(string name) : base(name)
        {
        }

        public override void Receive(string from, string message)
        {
            Console.Write("To a Actor: ");
            base.Receive(from, message);
        }
    }
}
