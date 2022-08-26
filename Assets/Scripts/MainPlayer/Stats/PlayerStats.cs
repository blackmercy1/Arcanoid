using System;
using Ranges;
using UnityEngine;

namespace MainPlayer.Stats
{
    [Serializable]
    public class PlayerStats
    {
        [SerializeField] private IntRange _killPoints;
        [SerializeField] private IntRange _damage;
        [SerializeField] private IntRange _hitPoints;
        [SerializeField] private FloatRange _speed;
        [SerializeField] private FloatRange _defaultTilesReload;
        [SerializeField] private FloatRange _laserTilesReload;

        public IntRange KillPoints => _killPoints;
        public IntRange Damage => _damage;
        public IntRange HitPoints => _hitPoints;
        public FloatRange Speed => _speed;
        public FloatRange DefaultTilesReload => _defaultTilesReload;
        public FloatRange LaserTilesReload => _laserTilesReload;

        public PlayerStats(IntRange killPoints, IntRange damage, IntRange hitPoints, FloatRange speed, 
             FloatRange defaultTilesReload, FloatRange laserTilesReload)
        {
            _killPoints = killPoints;
            _damage = damage;
            _speed = speed;
            _hitPoints = hitPoints;
            _defaultTilesReload = defaultTilesReload;
            _laserTilesReload = laserTilesReload;
        }

        public PlayerStats(float speed)
        {
            _speed = new FloatRange(speed, speed);
        }

        public static PlayerStats operator +(PlayerStats stats1, PlayerStats stats2)
        {
            var speed = stats1.Speed + stats2.Speed;
            var damage = stats1.Damage + stats2.Damage;
            var hitPoints = stats1.HitPoints + stats2.HitPoints;
            var killPoints = stats1.KillPoints + stats2.KillPoints;
            var defaultTilesReload = stats1.DefaultTilesReload + stats2.DefaultTilesReload;
            var laserTilesReload = stats1.LaserTilesReload + stats2.LaserTilesReload;
            
            return new PlayerStats(killPoints, damage, hitPoints, speed, defaultTilesReload, laserTilesReload);
        }
    }
}