using System;
using MainPlayer.PlayerSettings;
using UnityEngine;

namespace MainPlayer
{
    public sealed class PlayerShooter : ILaserStatistics
    {
        public event Action<int> LaserAmmo;
        public event Action<float> TimeToReloadLaser;

        private readonly PlayerSettingsConfig _playerSettingsConfig;
        private readonly Transform _playerTransform;
        private readonly Transform _gunHolder;

        private float _defaultTilesReload;
        private float _laserTilesReload;

        private float _laserCachedTime;
        private float _defaultCachedTime;

        private int _laserAmmo;

        public PlayerShooter(PlayerSettingsConfig playerSettingsConfig, Transform playerTransform, Transform gunHolder)
        {
            _playerSettingsConfig = playerSettingsConfig;
            _playerTransform = playerTransform;
            _gunHolder = gunHolder;
            
            _defaultCachedTime = Time.time;
            _laserCachedTime = Time.time;

            _laserTilesReload = _playerSettingsConfig.PlayerStats.LaserTilesReload.GetRandomValue();
        }

        public void Shoot()
        {
            if (_defaultCachedTime + _defaultTilesReload >= Time.time)
                return;

            _defaultTilesReload = _playerSettingsConfig.PlayerStats.DefaultTilesReload.GetRandomValue();
            var tile = Instantiater.Spawn
                (_playerSettingsConfig.DefaultTilePrefab, _gunHolder.transform.position, _playerTransform.rotation);

            _defaultCachedTime = Time.time;
        }

        public void LaserShoot()
        {
            if (_laserAmmo <= 0)
                return;

            _laserTilesReload = _playerSettingsConfig.PlayerStats.LaserTilesReload.GetRandomValue();
            var tile = Instantiater.Spawn
                (_playerSettingsConfig.LaserTilePrefab, _gunHolder.transform.position, _playerTransform.rotation);
            
            _laserAmmo -= 1;
            LaserAmmo?.Invoke(_laserAmmo);
            
            _laserCachedTime = Time.time;
        }

        public void GetLaserAmmo()
        {
            var cachedTime = Time.time;
            
            if (!(_laserCachedTime + _laserTilesReload <= cachedTime))
            {
                TimeToReloadLaser?.Invoke(_laserCachedTime + _laserTilesReload - cachedTime);
                return;
            }

            if (_laserAmmo < _playerSettingsConfig.MaxLaserAmmo)
            {
                _laserCachedTime = cachedTime;
                _laserAmmo += 1;
                LaserAmmo?.Invoke(_laserAmmo);
            }
        }
    }
}