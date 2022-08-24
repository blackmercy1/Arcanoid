using Asteroids.AsteroidsGenerator;
using Asteroids.AsteroidsGenerator.Builder;
using Asteroids.AsteroidsGenerator.Builder.Configs;
using Asteroids.AsteroidsGenerator.Pool;
using Asteroids.Ranges;
using Asteroids.Stats.Decorators.Realization;
using Common;
using GameAreas;
using MainPlayer;
using UnityEngine;
using UpdatesSystem;

namespace Installers
{
    public sealed class UfoGeneratorInstaller : MonoBehaviour
    {
        [SerializeField] private UfoConfig _ufoConfig;
        [SerializeField] private FloatRange _spawnRateRange;
        [SerializeField] private AnimationCurve _animationCurve;
        
        public UfoGenerator Install(GameArea gameArea, GameUpdates gameUpdates, Player player)
        {
            return CreateAsteroidGenerator(gameArea, gameUpdates, player);
        }

        private UfoGenerator CreateAsteroidGenerator(GameArea gameArea, GameUpdates gameUpdates, Player player)
        {
            var ufoPlacer = new UfoPlacer(gameArea);
            var ufoProvider = CreateUfoProvider(gameUpdates, player);
            
            var timer = new RandomLoopTimer(_spawnRateRange);
            var ufoGenerator = new UfoGenerator(ufoPlacer, ufoProvider, timer);

            gameUpdates.AddToUpdateList(timer);
            return ufoGenerator;
        }
        
        private UfoPoolProvider CreateUfoProvider(GameUpdates gameUpdates, Player player)
        {
            var statsProvider = CreateStatsProvider(gameUpdates);

            var ufoBuilder = new UfoBuilder(statsProvider, _ufoConfig.Ufo, player);
            var ufoProvider = new UfoPoolProvider(ufoBuilder);

            return ufoProvider;
        }

        private IUfoStatsProvider CreateStatsProvider(GameUpdates gameUpdates)
        {
            var stopWatch = new StopWatch();
            var statsProvider = new TimeScalingSpeedUfo(_ufoConfig, _animationCurve, stopWatch);

            gameUpdates.AddToUpdateList(stopWatch);

            return statsProvider;
        }
    }
}