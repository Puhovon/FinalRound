
namespace Units.Player
{
    public class Player : Unit
    {
        public override int _health { get; protected set; }
        public override int _maxHealth { get; protected set; }
        public override void ApplyDamage(int damage)
        {
            if(damage > 0)
                _health -= damage;
        }
    }
}