using Asteroids.AsteroidsGenerator.Pool;
using Asteroids.Movement.Direction;
using Asteroids.Stats;
using MainPlayer;
using Stats;
using UnityEngine;

namespace Asteroids.AsteroidsGenerator.Builder
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

    public sealed class UfoDirectionProvider : IDirectionProvider
    {
        private Vector2 _direction;

        public UfoDirectionProvider(Player player)
        {
            player.PlayerChangedPosition += ChangeDirection;
        }

        private void ChangeDirection(Vector2 position)
        {
            _direction = position;
        }

        public Vector2 GetDirection()
        {
            return _direction;
        }
    }
}