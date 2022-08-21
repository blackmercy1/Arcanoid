using Asteroids.Movement.Direction;
using UnityEngine;

namespace Asteroids.Movement
{
    public class AsteroidsMovement
    {
        private readonly Transform _transform;
        private readonly IDirectionProvider _directionProvider;
        private readonly float _speed;

        private Vector2 _direction; 
        
        public AsteroidsMovement(Transform transform, float speed, IDirectionProvider directionProvider)
        {
            _transform = transform;
            _speed = speed;
            _directionProvider = directionProvider;
        }

        public void Move(float deltaTime)
        {
            if (_direction == Vector2.zero)
                _direction = _directionProvider.GetDirection();
            
            var translation = _direction * (_speed * deltaTime);
            _transform.Translate(translation);
        }
    }
}