using System;
using Common;
using UpdatesSystem;

namespace Asteroids.AsteroidsGenerator
{
    public sealed class AsteroidGenerator : IClean
    {
        public event Action<Asteroid> Spawned;

        private readonly IAsteroidsPlacer _asteroidsPlacer;
        private readonly IAsteroidsProvider _asteroidsProvider;
        private readonly RandomLoopTimer _timer; 
        
        public AsteroidGenerator(IAsteroidsPlacer asteroidsPlacer, 
            IAsteroidsProvider asteroidsProvider, RandomLoopTimer timer)
        {
            _asteroidsPlacer = asteroidsPlacer;
            _asteroidsProvider = asteroidsProvider;
            
            _timer = timer;
            _timer.TimIsUp += SpawnAsteroid;
            _timer.Resume();
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
            _timer.Clean();
        }
    }
}