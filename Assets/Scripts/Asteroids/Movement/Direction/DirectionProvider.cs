using Asteroids.AsteroidsGenerator;
using UnityEngine;

namespace Asteroids.Movement.Direction
{
    public class DirectionProvider : IDirectionProvider
    {
        private Vector2 _direction;

        public DirectionProvider(IAsteroidEnding asteroidEnding)
        {
            asteroidEnding.GetEndPosition += OnAsteroidEnding;
        }

        private void OnAsteroidEnding(Vector2 endPosition)
        {
            _direction = endPosition;
        }

        public Vector2 GetDirection()
        {
            return _direction;
        }
    }
}