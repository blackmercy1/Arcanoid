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
        [SerializeField] private SmallAsteroidsConfig _smallAsteroidsConfig;
        [SerializeField] private FloatRange _spawnRateRange;

        public AsteroidGenerator Install(GameArea gameArea, GameUpdates gameUpdates)
        {
            return CreateAsteroidGenerator(gameArea, gameUpdates);
        }

        private AsteroidGenerator CreateAsteroidGenerator(GameArea gameArea, GameUpdates gameUpdates)
        {
            var asteroidsPlacer = new AsteroidsPlacer(gameArea);
            var smallAsteroidsPlacer = new SmallAsteroidsPlacer(gameArea);
            var asteroidsProvider = CreateAsteroidsProvider(gameUpdates, asteroidsPlacer);
            var smallAsteroidsBuilder = CreateSmallAsteroidsBuilder(smallAsteroidsPlacer);
            
            var timer = new RandomLoopTimer(_spawnRateRange);
            var asteroidsGenerator = new AsteroidGenerator(asteroidsPlacer, smallAsteroidsPlacer,
                    asteroidsProvider, timer, smallAsteroidsBuilder, _smallAsteroidsConfig);
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

        private SmallAsteroidsBuilder CreateSmallAsteroidsBuilder(IAsteroidEnding smallAsteroidsPlacer)
        {
            var smallStatsProvider = CreateSmallAsteroidsStatsProvider();
            var smallAsteroidBuilder =
                new SmallAsteroidsBuilder(smallStatsProvider, _smallAsteroidsConfig.Prefab, smallAsteroidsPlacer);

            return smallAsteroidBuilder;
        }

        private IAsteroidsStatsProvider CreateSmallAsteroidsStatsProvider()
        {
            var statsProvider = new EmptyAsteroidDecorator(_smallAsteroidsConfig);
            return statsProvider;
        }

        private IAsteroidsStatsProvider CreateStatsProvider(GameUpdates gameUpdates)
        {
            var stopWatch = new StopWatch();
            var statsProvider = new TimeScalingSpeed(_asteroidsConfig, _animationCurve, stopWatch);

            gameUpdates.AddToUpdateList(stopWatch);

            return statsProvider;
        }
    }
}