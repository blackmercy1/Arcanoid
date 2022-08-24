using System.Numerics;
using Asteroids.AsteroidsGenerator.Pool;
using Asteroids.Movement.Direction;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

namespace Asteroids.AsteroidsGenerator.Builder
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
            // var translation = _direction * (_speed * deltaTime);c
            // _ufoTransform.Translate(translation);
            elapsedTime += deltaTime;
            var percentageComplete = elapsedTime / _speed;
            _ufoTransform.position = Vector2.Lerp(_ufoTransform.position, _direction, percentageComplete);
        }
    }
}