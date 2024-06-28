namespace ElevatorService
{
    // Enum for the elevator's movement state
    public enum MovementState { Up, Down, Stationary }

    // Enum for door state
    public enum DoorState { Open, Closed }

    public class Elevator
    {
        public int CurrentFloor { get; private set; } = 1; // To Start at the first floor
        public MovementState Movement { get; private set; } = MovementState.Stationary;
        public DoorState Door { get; private set; } = DoorState.Closed; // To default the door state to closed 

        private HashSet<int> internalRequests = new HashSet<int>();
        private HashSet<int> externalRequests = new HashSet<int>();

        // Method to request the elevator from inside
        public void RequestFloor(int floor)
        {
            if (floor >= 1 && floor <= 5)
            {
                internalRequests.Add(floor);
                Console.WriteLine($"Floor {floor} requested from inside the elevator.");
            }
        }

        // Method to call the elevator from outside
        public void CallElevator(int floor, MovementState direction)
        {
            if (floor >= 1 && floor <= 5)
            {
                externalRequests.Add(floor);
                Console.WriteLine($"Elevator called to floor {floor} for direction {direction}.");
            }
        }

        // Method to simulate the elevator's operation
        public void Operate()
        {
            while (internalRequests.Any() || externalRequests.Any())
            {
                if (internalRequests.Contains(CurrentFloor) || externalRequests.Contains(CurrentFloor))
                {
                    StopAtFloor();
                }

                if (internalRequests.Any() || externalRequests.Any())
                {
                    MoveToNextFloor();
                }
            }

            if (Movement != MovementState.Stationary)
            {
                Movement = MovementState.Stationary;
                Console.WriteLine("Elevator is now stationary.");
            }
        }

        // Method to stop at the current floor
        private void StopAtFloor()
        {
            Console.WriteLine($"Elevator stopped at floor {CurrentFloor}. Doors opening.");
            Door = DoorState.Open;


            internalRequests.Remove(CurrentFloor);
            externalRequests.Remove(CurrentFloor);

            // Simulate door staying open for a bit
            Thread.Sleep(2000);
            Console.WriteLine("Doors closing.");
            Door = DoorState.Closed;

        }

        // Method to determine the next floor and move
        private void MoveToNextFloor()
        {
            int nextFloor = DetermineNextFloor();
            if (nextFloor > CurrentFloor)
            {
                Movement = MovementState.Up;
                CurrentFloor++;
            }
            else if (nextFloor < CurrentFloor)
            {
                Movement = MovementState.Down;
                CurrentFloor--;
            }

            Console.WriteLine($"Elevator moving {Movement}. Current floor: {CurrentFloor}");

            // Simulate travel time between floors
            Thread.Sleep(1000);
        }

        // Method to determine the next floor to go to
        private int DetermineNextFloor()
        {
            if (Movement == MovementState.Stationary || Movement == MovementState.Up)
            {
                return internalRequests.Concat(externalRequests).Where(f => f >= CurrentFloor).DefaultIfEmpty(internalRequests.Concat(externalRequests).Min()).Min();
            }
            else
            {
                return internalRequests.Concat(externalRequests).Where(f => f <= CurrentFloor).DefaultIfEmpty(internalRequests.Concat(externalRequests).Max()).Max();
            }
        }

    }
}