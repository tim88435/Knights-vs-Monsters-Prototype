using UnityEngine;

public static class ArcadeInput
{
    //First and second player
    public static PlayerArcadeInput P1 = new PlayerArcadeInput(1);
    public static PlayerArcadeInput P2 = new PlayerArcadeInput(1);
    public class PlayerArcadeInput
    {
        //Constructor needs to know which player it's keeping track of
        public PlayerArcadeInput(int number)
        {
            _playerNumber = number;
        }
        private readonly int _playerNumber;
        //Buttons
        public bool R1
        {
            get
            {
                return Input.GetButton($"P{_playerNumber} R1");
            }
        }
        public bool R2
        {
            get
            {
                return Input.GetButton($"P{_playerNumber} R2");
            }
        }
        public bool R3
        {
            get
            {
                return Input.GetButton($"P{_playerNumber} R3");
            }
        }
        public bool B1
        {
            get
            {
                return Input.GetButton($"P{_playerNumber} B1");
            }
        }
        public bool B2
        {
            get
            {
                return Input.GetButton($"P{_playerNumber} B2");
            }
        }
        public bool B3
        {
            get
            {
                return Input.GetButton($"P{_playerNumber} B3");
            }
        }
        public bool Start
        {
            get
            {
                return Input.GetButton($"P{_playerNumber} start");
            }
        }
        //Axies
        public float Horizontal
        {
            get
            {
                return Input.GetAxis($"P{_playerNumber} hori");
            }
        }
        public float Vertical
        {
            get
            {
                return Input.GetAxis($"P{_playerNumber} verti");
            }
        }
        //Raw Axies
        public float HorizontalRaw
        {
            get
            {
                return Input.GetAxisRaw($"P{_playerNumber} hori");
            }
        }
        public float VerticalRaw
        {
            get
            {
                return Input.GetAxisRaw($"P{_playerNumber} verti");
            }
        }
    }
}