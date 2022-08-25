using System;
using Asteroids;
using Asteroids.AsteroidsGenerator.Builder;
using Pools;
using Stats;
using Ufos.Movement;
using UnityEngine;
using UpdatesSystem;
using Vector2 = System.Numerics.Vector2;

namespace Ufos
{
    public class Ufo : PooledObject, IFixedUpdate, IDamageable, IScoreProvider
    {
        public event Action<IFixedUpdate> UpdateFixedRemoveRequested;

        public event Action<int> Scored;

        public float _elapsedTime;
        
        private UfoMovement _movement;
        private Health _health;

        private Vector2 _ufoPosition;
        
        private int _killPoints;
        private int _damage;

        public void Initialize(Health health, UfoMovement movement, int killPoints, int damage)
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
            DisableSelf();
        }

        private void DisableSelf()
        {
            UpdateFixedRemoveRequested?.Invoke(this);
            ReturnToPool();
        }

        public void FixedGameUpdate(float fixedDeltaTime)
        {
            _movement.Move(fixedDeltaTime, ref _elapsedTime);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent<IDamageable>(out var damageable))
            {
                damageable.TakeDamage(_damage);
            }
        }

        public void TakeDamage(int damage)
        {
            _health.TakeDamage(damage);
        }
    }
}