using System;
using GameAreas;
using UnityEngine;

namespace Asteroids.AsteroidsGenerator.Builder
{
    public sealed class SmallAsteroidsPlacer : IAsteroidsPlacer, IAsteroidEnding
    {
        public event Action<Vector2> GetEndPosition;
        
        private readonly GameArea _gameArea;

        private Vector2 _endPosition;
        
        public SmallAsteroidsPlacer(GameArea gameArea)
        {
            _gameArea = gameArea;
        }

        public void PlaceAsteroid(Asteroid asteroid, Vector2 position)
        {
            asteroid.transform.position = position;
            _endPosition = _gameArea.GetRandomEndPosition();
            GetEndPosition?.Invoke(_endPosition);
        }
    }
}