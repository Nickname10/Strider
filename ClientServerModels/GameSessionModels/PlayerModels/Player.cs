using System;
using System.Runtime.Serialization;

namespace ClientServerModels.GameSessionModels.PlayerModels
{
    [DataContract]
    public class Player : GameObject
    {
        [NonSerialized] private readonly PoolStates _poolStates;
        [DataMember] public double Speed { get; private set; }
        [NonSerialized] private PlayerState _state;
        [NonSerialized] private short _countFood;

        public void Move()
        {
            _state.Move();
        }

        public Player(int id, double globalX, double globalY, double radius, double speed,
            Character character)
        {
            Id = id;
            GlobalX = globalX;
            GlobalY = globalY;
            Radius = radius;
            MainRedColor = character.MainRedColor;
            MainBlueColor = character.MainBlueColor;
            MainGreenColor = character.MainGreenColor;
            StrokeRedColor = character.StrokeRedColor;
            StrokeGreenColor = character.StrokeGreenColor;
            StrokeBlueColor = character.StrokeBlueColor;
            StrokeLength = character.StrokeLength;
            StrokeSpace = character.StrokeSpace;
            Speed = speed;
            _poolStates = new PoolStates(this);
            _state = _poolStates.MovingUpState;
            StrokeThickness = 2;
        }

        public void SetMovingUpState()
        {
            _state = _poolStates.MovingUpState;
        }

        public void SetMovingLeftState()
        {
            _state = _poolStates.MovingLeftState;
        }

        public void SetMovingRightState()
        {
            _state = _poolStates.MovingRightState;
        }

        public void SetMovingDownState()
        {
            _state = _poolStates.MovingDownState;
        }

        public void Grove(GameObject food)
        {
            Radius += food.Radius * 0.25;
            if (++_countFood % 15 == 0) StrokeThickness++;
            Speed -= (Speed / Radius) * 0.1;
        }
    }
}