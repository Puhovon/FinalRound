using UnityEngine.Events;

namespace Units.Enemy
{
    public static class EnemyEvents
    {
        public static UnityEvent onPlayerDetect = new UnityEvent();
        public static UnityEvent onPlayerUndetect = new UnityEvent();
        public static UnityEvent<bool> onWalked = new UnityEvent<bool>();
        public static UnityEvent onFired = new UnityEvent();
        
        
        public static UnityEvent onZombieAttack = new UnityEvent();
        public static UnityEvent<bool> onZombieWalk = new UnityEvent<bool>();
        public static UnityEvent onZombieDeath = new UnityEvent();
        public static UnityEvent onZombieDeatectPlayer = new UnityEvent();
        public static UnityEvent onZombieUnDeatectPlayer = new UnityEvent();
    }
}