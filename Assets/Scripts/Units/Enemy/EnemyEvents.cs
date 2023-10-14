using UnityEngine.Events;

namespace Units.Enemy
{
    public static class EnemyEvents
    {
        public static UnityEvent onPlayerDetect = new UnityEvent();
        public static UnityEvent onPlayerUndetect = new UnityEvent();
        public static UnityEvent<bool> onWalked = new UnityEvent<bool>();
        public static UnityEvent onDeath = new UnityEvent();
        public static UnityEvent onTuned;
        public static UnityEvent onFired = new UnityEvent();
    }
}