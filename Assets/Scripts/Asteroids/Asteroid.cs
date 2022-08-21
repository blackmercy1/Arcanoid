using System;
using Asteroids.Movement;
using Pools;
using Portals;
using Stats;
using UnityEngine;
using UpdatesSystem;

namespace Asteroids
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class Asteroid : PooledObject, IDamageable, IFixedUpdate, ICollision
    {
        public event Action<IFixedUpdate> UpdateFixedRemoveRequested;
        public static event Action Died;
        
        public event Action<int> Scored;
        
        private AsteroidsMovement _movement;
        private Health _health;

        private Vector2 _asteroidPosition;
        
        private int _killPoints;
        private int _damage;
        
        public void Initialize(Health health, AsteroidsMovement movement, int killPoints, int damage)
        {
            _damage = damage;
            _movement = movement;
            _killPoints = killPoints;
            
            _health = health;
            _health.Died += OnDied;
        }

        private void OnDied()
        {
            Scored?.Invoke(_killPoints);
            Scored = null;
            
            Died?.Invoke();
            DisableSelf();
        }

        private void BuildSmallAsteroid()
        {
            
        }

        private void DisableSelf()
        {
            UpdateFixedRemoveRequested?.Invoke(this);
            ReturnToPool();
        }

        public void FixedGameUpdate(float fixedDeltaTime)
        {
            _movement.Move(fixedDeltaTime);
        }

        public void TakeDamage(int damage)
        {
            _health.TakeDamage(damage);
        }
        
        public Vector2 GetPosition()
        {
            _asteroidPosition = transform.position;
            return _asteroidPosition;
        }

        public void SetPosition(Vector2 position)
        {
            transform.position = position;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent<IDamageable>(out var damageable))
            {
                damageable.TakeDamage(_damage);
            }
        }
    }
}