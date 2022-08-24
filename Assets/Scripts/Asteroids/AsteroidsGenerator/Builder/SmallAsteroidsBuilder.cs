using Asteroids.Movement;
using Asteroids.Movement.Direction;
using Asteroids.Stats;
using Asteroids.Stats.Decorators;
using Stats;
using UnityEngine;

namespace Asteroids.AsteroidsGenerator.Builder
{
    public class SmallAsteroidsBuilder
    {
        private readonly Asteroid _prefab;
        private readonly IAsteroidsStatsProvider _asteroidsStatsProvider;
        private readonly IAsteroidEnding _asteroidEnding;

        private AsteroidsStats Stats => _asteroidsStatsProvider.Stats;
        
        public SmallAsteroidsBuilder(Asteroid prefab, IAsteroidsStatsProvider asteroidsStatsProvider, 
            IAsteroidEnding asteroidEnding)
        {
            _asteroidsStatsProvider = asteroidsStatsProvider;
            _asteroidEnding = asteroidEnding;
            _prefab = prefab;
            
            Asteroid.Died += BuildAsteroid;
        }

        private void BuildAsteroid()
        {
            var instance = Object.Instantiate(_prefab);
            InitializeAsteroid(instance);
        }
        
        private void InitializeAsteroid(Asteroid asteroid)
        {
            var health = GetHealth();
            var movement = GetMovement(asteroid);
            var damage = Stats.Damage.GetRandomValue();
            var killPoints = Stats.KillPoints.GetRandomValue();

            asteroid.Initialize(health, movement, killPoints, damage);
        }
        
        private Health GetHealth()
        {
            return new Health(Stats.HitPoints.GetRandomValue());
        }

        private AsteroidsMovement GetMovement(Component asteroid)
        {
            var directionProvider = new AsteroidDirectionProvider(_asteroidEnding);
            return new AsteroidsMovement(asteroid.transform, Stats.Speed.GetRandomValue(), directionProvider);
        }
    }
}