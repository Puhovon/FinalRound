using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Units.Enemy
{
    public class Timer
    {
        public IEnumerator TimerToAction(UnityEvent @event,int time)
        {
            for (int i = 0; i < time; i++)
            {
                yield return new WaitForSeconds(1);
            }
            
            @event?.Invoke();
            
        }
    }
}