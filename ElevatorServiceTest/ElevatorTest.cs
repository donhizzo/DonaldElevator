using ElevatorService;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ElevatorServiceTest
{
    [TestFixture]

    public class ElevatorTest
    {
        private Elevator elevator;

        [SetUp]
        public void Setup()
        {
            elevator = new Elevator();
        }

        [Test]
        public void Elevator_ShouldStartAtFirstFloor()
        {
            Assert.Equals(1, elevator.CurrentFloor);
        }

        [Test]
        public void RequestFloor_ShouldAddRequest()
        {
            elevator.RequestFloor(3);
            elevator.Operate();
            Assert.Equals(3, elevator.CurrentFloor);
        }

        [Test]
        public void CallElevator_ShouldAddExternalRequest()
        {
            elevator.CallElevator(5, MovementState.Down);
            elevator.Operate();
            Assert.Equals(5, elevator.CurrentFloor);
        }

        [Test]
        public void Elevator_ShouldStopAtAllRequestedFloors()
        {
            elevator.CallElevator(3, MovementState.Up);
            elevator.RequestFloor(5);
            elevator.Operate();

            Assert.Equals(5, elevator.CurrentFloor);
        }

        [Test]
        public void Elevator_ShouldMoveUpToNextRequestedFloor()
        {
            elevator.RequestFloor(4);
            elevator.Operate();
            Assert.Equals(4, elevator.CurrentFloor);
        }

        [Test]
        public void Elevator_ShouldMoveDownToNextRequestedFloor()
        {
            elevator.RequestFloor(3);
            elevator.CallElevator(5, MovementState.Down);

            // Move to 5th floor first, then request down to 3rd floor
            elevator.Operate();
            Assert.Equals(5, elevator.CurrentFloor);

            // Now it should move down to 3rd floor
            elevator.Operate();
            Assert.Equals(3, elevator.CurrentFloor);
        }

        [Test]
        public void Elevator_ShouldHandleMultipleRequests()
        {
            elevator.CallElevator(2, MovementState.Up);
            elevator.CallElevator(4, MovementState.Down);
            elevator.RequestFloor(3);

            elevator.Operate();

            // The elevator should stop at the 2nd, 3rd, and 4th floors in that order
            Assert.Equals(4, elevator.CurrentFloor);
        }

        [Test]
        public void Elevator_DoorsShouldOpenAndCloseAtRequestedFloors()
        {
            elevator.CallElevator(3, MovementState.Up);

            // Simulate the elevator operating to the 3rd floor
            elevator.Operate();

            // Check if the elevator stopped and opened doors at 3rd floor
            Assert.Equals(3, elevator.CurrentFloor);
            Assert.Equals(DoorState.Closed, elevator.Door);
        }

        [Test]
        public void Elevator_ShouldBecomeStationaryWhenNoRequests()
        {
            elevator.RequestFloor(2);
            elevator.Operate();
            Assert.Equals(2, elevator.CurrentFloor);

            // After serving all requests, the elevator should be stationary
            elevator.Operate();
            Assert.Equals(MovementState.Stationary, elevator.Movement);
        }

    }
}
