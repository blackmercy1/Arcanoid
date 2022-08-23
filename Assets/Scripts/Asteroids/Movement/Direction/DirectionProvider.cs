using Asteroids.AsteroidsGenerator;
using UnityEngine;
using UpdatesSystem;

namespace Asteroids.Movement.Direction
{
    public class DirectionProvider : IDirectionProvider, IClean
    {
        private readonly IAsteroidEnding _asteroidEnding;
        private Vector2 _direction;

        public DirectionProvider(IAsteroidEnding asteroidEnding)
        {
            _asteroidEnding = asteroidEnding;
            _asteroidEnding.GetEndPosition += OnAsteroidEnding;
        }

        private void OnAsteroidEnding(Vector2 endPosition)
        {
            _direction = endPosition;
        }

        public Vector2 GetDirection()
        {
            return _direction;
        }

        public void Clean()
        {
            _asteroidEnding.GetEndPosition -= OnAsteroidEnding;
        }
    }
}