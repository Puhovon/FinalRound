using System;

namespace Units.Player
{
    public class PlayerEvents
    {
        public static Action OnJumped;
        public static Action OnTurned;
        public static Action<bool> onChangedState;
        public static Action onFired;

    }
}