using UnityEngine;

namespace Asteroids.AsteroidsGenerator
{
    public interface IAsteroidsPlacer
    {
        void PlaceAsteroid(Asteroid asteroid, Vector2 position = default);
    }
}