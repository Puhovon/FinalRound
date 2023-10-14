using UnityEngine.Events;

namespace UI
{
    public static class UIEvents
    {
        public static UnityEvent<int> onHealthChanged = new UnityEvent<int>();
        public static UnityEvent onScoreChanged = new UnityEvent();
        public static UnityEvent onAmmoChanged = new UnityEvent();
    }
}