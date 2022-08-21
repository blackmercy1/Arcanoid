using System;

namespace Stats
{
    public class Health
    {
        public event Action Died;
        public event Action<int> Changed;

        public int HitPoints;

        public Health(int startHitPoints)
        {
            HitPoints = startHitPoints;
        }

        public void TakeDamage(int damage)
        {
            HitPoints -= damage;
            Changed?.Invoke(HitPoints);

            if (IsDead())
                Died?.Invoke();
        }

        private bool IsDead()
        {
            return HitPoints <= 0;
        }
    }
}