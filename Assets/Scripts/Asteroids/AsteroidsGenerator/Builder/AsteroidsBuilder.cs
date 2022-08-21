using System;
using Asteroids.Movement;
using Asteroids.Movement.Direction;
using Asteroids.Stats;
using Asteroids.Stats.Decorators;
using GameAreas;
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
        
        private AsteroidsStats Stats => _asteroidsStatsProvider.Stats;

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
            var directionProvider = new DirectionProvider(_asteroidEnding);
            return new AsteroidsMovement(asteroid.transform, Stats.Speed.GetRandomValue(), directionProvider);
        }
    }

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
            var directionProvider = new DirectionProvider(_asteroidEnding);
            return new AsteroidsMovement(asteroid.transform, Stats.Speed.GetRandomValue(), directionProvider);
        }
    }
    
    public sealed class SmallAsteroidsPlacer : IAsteroidsPlacer, IAsteroidEnding
    {
        public event Action<Vector2> GetEndPosition;
        
        private readonly GameArea _gameArea;

        private Vector2 _endPosition;
        
        public SmallAsteroidsPlacer(GameArea gameArea)
        {
            _gameArea = gameArea;
        }

        public void PlaceAsteroid(Asteroid asteroid)
        {
            _endPosition = _gameArea.GetRandomEndPosition();
            GetEndPosition?.Invoke(_endPosition);
        }
    }
}