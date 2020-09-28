using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mediator
{
    [TestClass]
    public class Solution
    {
        [TestMethod]
        public void Main()
        {
            MessageMediator mm = new MessageMediator();

            var sc = mm.CreateColleague<StudentColleague>();
            var scc = mm.CreateColleague<SchoolColleague>();

            sc.Send("Hi Roma from StudentColleague");
            scc.Send("Hi Anton from SchoolColleague");
        }
    }
    public abstract class Colleague
    {
        protected Mediator mediator;

        //public Colleague(Mediator mediator)
        //{
        //    this.mediator = mediator;
        //}

        internal void SetMediator(Mediator mediator)
        {
            this.mediator = mediator;
        }

        public virtual void Send(string message)
        {
            this.mediator.Send(message, this);
        }

        public abstract void HandleNotification(string message);
    }

    public class SchoolColleague : Colleague
    {
        //public SchoolColleague(Mediator mediator) : base(mediator)
        //{
        //}

        public override void HandleNotification(string message)
        {
            Console.WriteLine($"School colleague receives notification {message}");
        }
    }

    public class StudentColleague : Colleague
    {
        //public StudentColleague(Mediator mediator) : base(mediator)
        //{
        //}

        public override void HandleNotification(string message)
        {
            Console.WriteLine($"Student colleague receives notification {message}");
        }
    }

    public abstract class Mediator
    {
        public abstract void Send(string message, Colleague colleague);
    }

    public class MessageMediator : Mediator
    {
       private List<Colleague> colleagues = new List<Colleague>();

        public void Register(Colleague colleague)
        {
            colleague.SetMediator(this);
            colleagues.Add(colleague);
        }

        public T CreateColleague<T>() where T : Colleague, new()
        {
            var c = new T();
            c.SetMediator(this);
            colleagues.Add(c);
            return c;
        }
             
        public override void Send(string message, Colleague colleague)
        {            
            colleagues.Where(c => c != colleague).ToList().ForEach(c => c.HandleNotification(message));
        }
    }

   
}
