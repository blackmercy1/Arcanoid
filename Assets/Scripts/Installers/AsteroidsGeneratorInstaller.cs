using Asteroids.AsteroidsGenerator;
using Asteroids.AsteroidsGenerator.Builder;
using Asteroids.AsteroidsGenerator.Builder.Configs;
using Asteroids.AsteroidsGenerator.Pool;
using Asteroids.Ranges;
using Asteroids.Stats.Decorators;
using Asteroids.Stats.Decorators.Realization;
using Common;
using GameAreas;
using UnityEngine;
using UpdatesSystem;

namespace Installers
{
    public class AsteroidsGeneratorInstaller : MonoBehaviour
    {
        [SerializeField] private AnimationCurve _animationCurve;
        [SerializeField] private AsteroidsConfig _asteroidsConfig;
        [SerializeField] private FloatRange _spawnRateRange;

        public AsteroidGenerator Install(GameArea gameArea, GameUpdates gameUpdates)
        {
            return CreateAsteroidGenerator(gameArea, gameUpdates);
        }

        private AsteroidGenerator CreateAsteroidGenerator(GameArea gameArea, GameUpdates gameUpdates)
        {
            var asteroidsPlacer = new AsteroidsPlacer(gameArea);
            var asteroidsProvider = CreateAsteroidsProvider(gameUpdates, asteroidsPlacer);
            
            var timer = new RandomLoopTimer(_spawnRateRange);
            var asteroidsGenerator = new AsteroidGenerator(asteroidsPlacer, asteroidsProvider, timer);

            gameUpdates.AddToUpdateList(timer);
            return asteroidsGenerator;
        }

        private AsteroidsPoolProvider CreateAsteroidsProvider(GameUpdates gameUpdates, IAsteroidEnding asteroidsPlacer)
        {
            var statsProvider = CreateStatsProvider(gameUpdates);

            var asteroidBuilder = new AsteroidsBuilder(statsProvider, _asteroidsConfig.Prefab, asteroidsPlacer);
            var asteroidProvider = new AsteroidsPoolProvider(asteroidBuilder);

            return asteroidProvider;
        }

        private IAsteroidsStatsProvider CreateStatsProvider(GameUpdates gameUpdates)
        {
            var stopWatch = new StopWatch();
            var statsProvider = new TimeScalingSpeedAsteroid(_asteroidsConfig, _animationCurve, stopWatch);

            gameUpdates.AddToUpdateList(stopWatch);

            return statsProvider;
        }
    }
}