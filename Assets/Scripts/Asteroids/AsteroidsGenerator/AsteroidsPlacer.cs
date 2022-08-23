using System;
using GameAreas;
using UnityEngine;

namespace Asteroids.AsteroidsGenerator
{
    public sealed class AsteroidsPlacer : IAsteroidsPlacer, IAsteroidEnding
    {
        public event Action<Vector2> GetEndPosition;
        
        private readonly GameArea _gameArea;

        private Vector2 _endPosition;
        
        public AsteroidsPlacer(GameArea gameArea)
        {
            _gameArea = gameArea;
        }

        public void PlaceAsteroid(Asteroid asteroid, Vector2 position)
        {
            var transform = asteroid.transform;

            var startPosition = _gameArea.GetRandomStartPosition();

            _endPosition = _gameArea.GetRandomEndPosition();
            GetEndPosition?.Invoke(_endPosition);
            
            _gameArea.PlaceObject(transform, startPosition);
        }
    }
}