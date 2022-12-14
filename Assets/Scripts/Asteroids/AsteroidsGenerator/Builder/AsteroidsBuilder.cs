using Asteroids.Movement;
using Asteroids.Movement.Direction;
using Asteroids.Stats;
using Asteroids.Stats.Decorators;
using Stats;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Asteroids.AsteroidsGenerator.Builder
{
    public sealed class AsteroidsBuilder
    {
        private readonly IAsteroidsStatsProvider _asteroidsStatsProvider;
        private readonly Asteroid _prefab;
        private readonly IAsteroidEnding _asteroidEnding;
        
        private AsteroidsStats stats => _asteroidsStatsProvider.Stats;

        public AsteroidsBuilder(IAsteroidsStatsProvider asteroidsStatsProvider, Asteroid prefab,
            IAsteroidEnding asteroidEnding)
        {
            _asteroidsStatsProvider = asteroidsStatsProvider;
            _prefab = prefab;
            _asteroidEnding = asteroidEnding;
        }

        public Asteroid BuildAsteroid()
        {
            var instance = Object.Instantiate(_prefab);
            InitializeAsteroid(instance);
            return instance;
        }

        public void InitializeAsteroid(Asteroid asteroid)
        {
            var health = GetHealth();
            var movement = GetMovement(asteroid);
            var damage = stats.Damage.GetRandomValue();
            var killPoints = stats.KillPoints.GetRandomValue();

            asteroid.Initialize(health, movement, killPoints, damage);
        }

        private Health GetHealth()
        {
            return new Health(stats.HitPoints.GetRandomValue());
        }

        private AsteroidsMovement GetMovement(Component asteroid)
        {
            var directionProvider = new AsteroidDirectionProvider(_asteroidEnding);
            return new AsteroidsMovement(asteroid.transform, stats.Speed.GetRandomValue(), directionProvider);
        }
    }
}