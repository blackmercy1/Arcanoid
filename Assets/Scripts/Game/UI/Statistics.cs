using System;
using MainPlayer;
using MainPlayer.Shooter;
using UnityEngine;
using UpdatesSystem;

namespace Game.UI
{
    public sealed class Statistics : IUpdate
    {
        public event Action<int> LaserAmmoChanged;
        public event Action<float> TimeToReloadLaserChanged;
        public event Action<Quaternion> RotationChanged;
        public event Action<Vector2> PositionChanged;
        public event Action<Vector2> SpeedChanged;
        public event Action<IUpdate> UpdateRemoveRequested;
        
        private readonly Transform _playerTransform;
        private readonly Player _player;
        private readonly ILaserStatistics _laserStatistics;
        
        private Vector2 _playerSpeed;

        private int _laserAmmo;

        public Statistics(ILaserStatistics laserStatistics, Transform playerTransform, Player player)
        {
            _laserStatistics = laserStatistics;
            _playerTransform = playerTransform;
            _player = player;

            _laserStatistics.LaserAmmo += OnLaserAmmoChanged;
            _laserStatistics.TimeToReloadLaser += OnTimeToReloadLaserChanged;
        }

        private void OnTimeToReloadLaserChanged(float reloadTime)
        {
            TimeToReloadLaserChanged?.Invoke(reloadTime);
        }

        private void OnLaserAmmoChanged(int laserAmmo)
        {
            LaserAmmoChanged?.Invoke(laserAmmo);
        }

        public void GameUpdate(float deltaTime)
        {
            _playerSpeed = _player.PlayerSpeed;
            
            PositionChanged?.Invoke(_playerTransform.position);
            RotationChanged?.Invoke(_playerTransform.rotation);
            SpeedChanged?.Invoke(_playerSpeed);
        }
    }
}