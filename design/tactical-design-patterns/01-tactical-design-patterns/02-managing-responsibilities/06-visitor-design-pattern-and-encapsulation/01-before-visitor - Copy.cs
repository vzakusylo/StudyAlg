using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace managing_responsibilities_before_visitor
{
    [TestClass]
    public class Solution
    {
        [TestMethod]
        public void Main()
        {
            Car car = new Car("Renault", "Megane", new Engine(66, 1598),
                Seat.FourSeatConfiguration);
            CarRegistration reg = car.Register();
            Console.WriteLine(reg.ToString());

            CarRegistration reg1 = new CarRegistration(car.Make, car.Model, car.Engine.CylinderVolume, car.Seats.Sum(seat => seat.Capacity));

            Console.WriteLine(reg1.ToString());

            var cars = new CarRepository().GetAll();
            CarsView view = new CarsView(cars);
            view.Render();
        }
    }

    class CarRegistration
    {
        private readonly string make;
        private readonly string model;
        private readonly float capacity;
        private readonly int maxPassengers;

        public CarRegistration(string make, string model, float capacity, int maxPassengers)
        {
            this.make = make;
            this.model = model;
            this.capacity = capacity;
            this.maxPassengers = maxPassengers;
        }

        public override string ToString()
        {
            return $"{make} " +
                   $"{model} " +
                   $"{capacity}cc " +
                   $"{maxPassengers} passengers";
        }
    }

    class Seat
    {
        public string Name { get; private set; }
        public int Capacity { get; private set; }

        public Seat(string name, int capacity)
        {
            Name = name;
            Capacity = capacity;
        }

        public static IEnumerable<Seat> FourSeatConfiguration
        {
            get
            {
                return new Seat[]
                {
                    new Seat("Driver", 1),
                    new Seat("Passenger", 2),
                    new Seat("Rear bench", 2)
                };
            }
        }

        public static IEnumerable<Seat> TwoSeatConfiguration
        {
            get
            {
                return new Seat[]
                {
                    new Seat("Driver", 1),
                    new Seat("Passenger", 2)                   
                };
            }
        }
    }

    class CarsView
    {
        private readonly IEnumerable<Car> cars;

        public CarsView(IEnumerable<Car> cars)
        {
            this.cars = cars;
        }

        public void Render()
        {
            foreach (var car in cars)
            {
                Console.WriteLine($"{car.Make} " +
                    $"{car.Model} " +
                    $"{car.Engine.CylinderVolume}cc " +
                    $"{car.Engine.Power}kW " +
                    $"{car.Seats.Sum(seat=>seat.Capacity)} seat(s)") ;
            }
        }
    }

    class CarRepository
    {
        public IEnumerable<Car> GetAll() => 
            new Car[]
            {
                new Car("Renault", "Megane", new Engine(66, 1598), Seat.FourSeatConfiguration),
                new Car("Ford", "Focus", new Engine(74, 1596), Seat.FourSeatConfiguration),
                new Car("Toyota", "Corolla", new Engine(78, 1587), Seat.FourSeatConfiguration),
                new Car("Mercedes-Benz", "SLK250", new Engine(201, 1800), Seat.TwoSeatConfiguration)
            };
    }

    
    class Engine
    {
        public float Power { get; private set; }
        public float CylinderVolume { get; private set; }

        public Engine(float power, float cylinderVolume)
        {
            Power = power;
            CylinderVolume = cylinderVolume;
        }
    }

    class Car
    {
        public string Make { get; private set; }
        public string Model { get; private set; }
        public Engine Engine { get; private set; }
        public IEnumerable<Seat> Seats { get; private set; }

        public Car(string make, string model, Engine engine, IEnumerable<Seat> seats)
        {
            Make = make;
            Model = model;
            Engine = engine;
            Seats = seats;
        }

        public CarRegistration Register()
        {
            return new CarRegistration(Make.ToUpper(), Model, 
                Engine.CylinderVolume, 
                Seats.Sum(seat => seat.Capacity));
        }
    }
}
