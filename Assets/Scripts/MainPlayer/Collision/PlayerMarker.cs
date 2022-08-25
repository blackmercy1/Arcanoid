using System;
using Portals;
using Stats;
using UnityEngine;

//Marker for collision
namespace MainPlayer.Collision
{
    public class PlayerMarker : MonoBehaviour, ICollision, IDamageable
    {
        public event Action<int> TakingDamage;
        
        private Vector2 _playerPosition;
    
        public Vector2 GetPosition()
        {
            _playerPosition = transform.position;
            return _playerPosition;
        }

        public void SetPosition(Vector2 position)
        {
            transform.position = position;
        }

        public void TakeDamage(int damage)
        {
            TakingDamage?.Invoke(damage);
        }
    }
}