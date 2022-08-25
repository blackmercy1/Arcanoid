using Asteroids.AsteroidsGenerator.Builder;
using Asteroids.Stats;
using MainPlayer;
using Stats;
using Ufos.Movement;
using Ufos.Movement.Direction;
using Ufos.UfoGenerator.Stats;
using Ufos.UfoGenerator.Stats.Decorators;
using UnityEngine;

namespace Ufos.UfoGenerator.Builder
{
    public sealed class UfoBuilder
    {
        private readonly IUfoStatsProvider _ufoStatsProvider;
        private readonly Ufo _ufoPrefab;
        private readonly Player _player;

        private UfoStats stats => _ufoStatsProvider.Stats;

        public UfoBuilder(IUfoStatsProvider ufoStatsProvider, Ufo ufo, Player player)
        {
            _ufoStatsProvider = ufoStatsProvider;
            _ufoPrefab = ufo;
            _player = player;
        }
        
        public Ufo BuildUfo()
        {
            var instance = Object.Instantiate(_ufoPrefab);
            InitializeUfo(instance);
            return instance;
        }

        public void InitializeUfo(Ufo ufo)
        {
            var health = GetHealth();
            var movement = GetMovement(ufo);
            var damage = stats.Damage.GetRandomValue();
            var killPoints = stats.KillPoints.GetRandomValue();

            ufo.Initialize(health, movement, killPoints, damage);
        }
        
        private Health GetHealth()
        {
            return new Health(stats.HitPoints.GetRandomValue());
        }

        private UfoMovement GetMovement(Component asteroid)
        {
            var ufoDirectionProvider = new UfoDirectionProvider(_player);
            return new UfoMovement(asteroid.transform, stats.Speed.GetRandomValue(), ufoDirectionProvider);
        }
    }
}