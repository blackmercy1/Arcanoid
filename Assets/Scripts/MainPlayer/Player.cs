using System;
using MainPlayer.Input;
using MainPlayer.PLayerMovement;
using MainPlayer.PlayerSettings;
using Stats;
using UnityEngine;
using UpdatesSystem;

namespace MainPlayer
{
    public sealed class Player : IUpdate, IFixedUpdate, IClean, IDamageable
    {
        public event Action<IFixedUpdate> UpdateFixedRemoveRequested;
        public event Action<IUpdate> UpdateRemoveRequested;
        public event Action<Vector2> PlayerChangedPosition;
        
        public event Action Died;
        
        public double OverclockingSpeed;
        
        private readonly PlayerInputFromKeyboard _playerInputFromKeyboard;
        private readonly PlayerSettingsConfig _playerSettingsConfig;
        private readonly Transform _playerTransform;
        private readonly Health _health;

        private readonly PlayerShooter _shooter;
        private readonly int _hitPoints;

        private PlayerMovement _movement;

        public Player(PlayerInputFromKeyboard playerInputFromKeyboard, PlayerSettingsConfig playerSettingsConfig
            , Transform playerTransform, PlayerControls playerControls, PlayerMarker playerMarker, Transform gunHolder)
        {
            _playerInputFromKeyboard = playerInputFromKeyboard;
            _playerSettingsConfig = playerSettingsConfig;
            _playerTransform = playerTransform;

            InitInputControl(playerControls);

            _health = GetHealth();
            _shooter = GetShooter(gunHolder);
            _movement = GetMovement();

            playerMarker.TakingDamage += TakeDamage;
            _health.Died += OnDied;
        }

        private void OnDied()
        {
            Died?.Invoke();
        }

        private void InitInputControl(PlayerControls playerControls)
        {
            playerControls.Enable();
            playerControls.Player.Shoot.performed += _ => { TryToShoot(); };
            playerControls.Player.LaserShoot.performed += _ => { TryToLaserShoot(); };
        }

        private void TryToShoot()
        {
            _shooter.Shoot();
        }

        private void TryToLaserShoot()
        {
            _shooter.LaserShoot();
        }

        public void GameUpdate(float deltaTime)
        {
            _playerInputFromKeyboard.GetInput();
            _shooter.GetLaserAmmo();
        }

        private Health GetHealth()
        {
            return new Health(_playerSettingsConfig.PlayerStats.HitPoints.GetRandomValue());
        }

        public void FixedGameUpdate(float fixedDeltaTime)
        {
            _movement = GetMovement();
            _movement.Move(fixedDeltaTime);
            PlayerChangedPosition?.Invoke(_playerTransform.position);
        }

        private PlayerMovement GetMovement()
        {
            return new PlayerMovement(_playerInputFromKeyboard, _playerSettingsConfig, _playerTransform, this);
        }

        private PlayerShooter GetShooter(Transform gunHolder)
        {
            return new PlayerShooter(_playerSettingsConfig, _playerTransform, gunHolder);
        }

        public Transform GetPlayerTransform()
        {
            return _playerTransform;
        }

        public void Clean()
        {
            UpdateRemoveRequested?.Invoke(this);
            UpdateFixedRemoveRequested?.Invoke(this);
            _health.Died -= OnDied;
        }

        public Vector3 GetSpeed()
        {
            return _movement.GetSpeed();
        }

        public ILaserStatistics GetGunStatistics()
        {
            return _shooter;
        }

        public void TakeDamage(int damage)
        {
            _health.TakeDamage(damage);
        }
    }
}