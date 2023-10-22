using UI;
using Units.Abstract;

namespace Units.Player
{
    public class PlayerHealth : Health
    {
        public override void Initialize()
        {
            base.Initialize();
            UIEvents.onHealthChanged?.Invoke(CurrentHealth);
        }
        
        public void ApplyHeal(int healPoint)
        {
            if (CurrentHealth + healPoint != MaxHealth) CurrentHealth += healPoint;

            if (CurrentHealth + healPoint >= MaxHealth)
            {
                healPoint = MaxHealth - CurrentHealth;
                CurrentHealth += healPoint;
            }

            UIEvents.onHealthChanged?.Invoke(CurrentHealth);
        }

       


        public override void ApplyDamage(int damage)
        {
            psBlood.Play();
            if (CurrentHealth - damage > 0)
                CurrentHealth -= damage;
            else
                Death();
            UIEvents.onHealthChanged?.Invoke(CurrentHealth);
        }

        public override void Death()
        {
            onDeath?.Invoke();
        }
    }
}