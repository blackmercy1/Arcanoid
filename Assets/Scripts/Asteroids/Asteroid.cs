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
    public class Asteroid : PooledObject, IDamageable, IFixedUpdate, ICollision, IScoreProvider
    {
        public event Action<IFixedUpdate> UpdateFixedRemoveRequested;
        public static event Action<Vector2> Died;
        
        public event Action<int> Scored;
        
        private AsteroidsMovement _movement;
        private Health _health;

        private Vector2 _asteroidPosition;
        
        private int _killPoints;
        private int _damage;

        private bool _destroyed;
        private bool _multiCollision;
        
        public void Initialize(Health health, AsteroidsMovement movement, int killPoints, int damage, bool destroyed = false)
        {
            _damage = damage;
            _movement = movement;
            _killPoints = killPoints;
            _destroyed = destroyed;
            
            _health = health;
            _health.Died += OnDied;
            _multiCollision = false;
        }

        private void OnDied()
        {
            if (_multiCollision)
                return;
            
            Scored?.Invoke(_killPoints);
            Scored = null;

            if (!_destroyed)
            {
                Died?.Invoke(transform.position);
                DisableSelf();
                _multiCollision = true;
            }
            else
            {
                UpdateFixedRemoveRequested?.Invoke(this);
                _health.Died -= OnDied;
                Destroy(gameObject);
            }
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

    public interface IScoreProvider
    {
        public event Action<int> Scored;
    }
}