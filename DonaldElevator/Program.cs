using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElevatorService;


namespace DonaldElevator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Elevator elevator = new Elevator();

            // Simulate external calls
            elevator.CallElevator(1, MovementState.Up);
            elevator.CallElevator(3, MovementState.Down);
            elevator.CallElevator(5, MovementState.Down);

            // Simulate internal requests
            elevator.RequestFloor(2);
            elevator.RequestFloor(4);

            // Start the elevator operation
            elevator.Operate();

            Console.WriteLine("Elevator operation complete. Press any key to exit.");
            Console.ReadKey();

        }
    }
}
