using Stats;
using UnityEngine;

namespace MainPlayer.Shooter
{
    public class Projectile : MonoBehaviour
    {
        private int _damage = 100;      
        
        private void Update()
        {
            Move();
        }

        private void Move()
        {
            transform.position += transform.right * (Time.deltaTime * 10f);
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.TryGetComponent<IDamageable>(out var damageable))
            {
                damageable.TakeDamage(_damage);
                DestroySelf();
            }
        }

        private void DestroySelf()
        {
            Destroy(gameObject);
        }
    }
}