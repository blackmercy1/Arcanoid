using Asteroids.Movement.Direction;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

namespace Ufos.Movement
{
    public sealed class UfoMovement
    {
        private readonly Transform _ufoTransform;
        private readonly IDirectionProvider _directionProvider;
        private readonly float _speed;

        private Vector2 _direction; 
        
        public UfoMovement(Transform ufoTransform, float speed, IDirectionProvider directionProvider)
        {
            _ufoTransform = ufoTransform;
            _speed = speed;
            _directionProvider = directionProvider;
        }

        public void Move(float deltaTime, ref float elapsedTime)
        {
            _direction = _directionProvider.GetDirection();
            elapsedTime += deltaTime;
            var percentageComplete = elapsedTime / _speed;
            _ufoTransform.position = Vector2.Lerp(_ufoTransform.position, _direction, percentageComplete);
        }
    }
}