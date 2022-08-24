using System;
using Asteroids.AsteroidsGenerator.Builder;
using Asteroids.AsteroidsGenerator.Builder.Configs;
using Common;
using UnityEngine;
using UpdatesSystem;

namespace Asteroids.AsteroidsGenerator
{
    public sealed class AsteroidGenerator : IClean
    {
        public event Action<Asteroid> Spawned;

        private readonly IAsteroidsPlacer _asteroidsPlacer;
        private readonly IAsteroidsPlacer _smallAsteroidPlacer;
        private readonly IAsteroidsProvider _asteroidsProvider;
        
        private readonly SmallAsteroidsBuilder _smallAsteroidsBuilder;
        private readonly SmallAsteroidsConfig _smallAsteroidsConfig;
        private readonly RandomLoopTimer _timer;

        public AsteroidGenerator(IAsteroidsPlacer asteroidsPlacer,IAsteroidsPlacer smallAsteroidPlacer, 
            IAsteroidsProvider asteroidsProvider, RandomLoopTimer timer, SmallAsteroidsBuilder smallAsteroidsBuilder
            , SmallAsteroidsConfig smallAsteroidsConfig)
        {
            _asteroidsPlacer = asteroidsPlacer;
            _smallAsteroidPlacer = smallAsteroidPlacer;
            _asteroidsProvider = asteroidsProvider;
            _smallAsteroidsBuilder = smallAsteroidsBuilder;
            _smallAsteroidsConfig = smallAsteroidsConfig;
            _timer = timer; 
            
            _timer.TimIsUp += SpawnAsteroid;
            Asteroid.Died += SpawnSmallAsteroid;
            _timer.Resume();
        }

        private void SpawnSmallAsteroid(Vector2 position)
        {
            for (var i = 0; i < _smallAsteroidsConfig.DestroyedParts; i++)
            {
                var asteroid = _smallAsteroidsBuilder.BuildAsteroid();
                _smallAsteroidPlacer.PlaceAsteroid(asteroid, position);
            
                Spawned?.Invoke(asteroid);
            }
        }

        private void SpawnAsteroid()
        {
            var asteroid = _asteroidsProvider.GetAsteroid();
            _asteroidsPlacer.PlaceAsteroid(asteroid);
            
            Spawned?.Invoke(asteroid);
        }

        public void Clean()
        {
            _timer.TimIsUp -= SpawnAsteroid;
            Asteroid.Died -= SpawnSmallAsteroid;
            _timer.Clean();
        }
    }
}