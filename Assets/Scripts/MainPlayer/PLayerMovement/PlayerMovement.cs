using MainPlayer.Input;
using MainPlayer.PlayerSettings;
using UnityEngine;

namespace MainPlayer.PLayerMovement
{
    public sealed class PlayerMovement : ISpeed
    {
        private readonly PlayerInputFromKeyboard _playerInputFromKeyboard;
        private readonly PlayerSettingsConfig _playerSettingsConfig;
        private readonly Transform _playerTransform;
        private readonly PlayerRotate _playerRotate;

        private readonly Player _player;
        
        private Vector2 _switchedDirection;
        private Vector3 _speed;

        private float _angleRotation;

        public PlayerMovement(PlayerInputFromKeyboard playerInputFromKeyboard,
            PlayerSettingsConfig playerSettingsConfig, Transform playerTransform, Player player)
        {
            _playerInputFromKeyboard = playerInputFromKeyboard;
            _playerSettingsConfig = playerSettingsConfig;
            _playerTransform = playerTransform;
            _player = player;

            _playerRotate = new PlayerRotate(_playerSettingsConfig);
        }

        public void Move(float deltaTime, ref Vector2 speed)
        {
            var input = _playerInputFromKeyboard.PlayerInputDirection.normalized;

            _playerRotate.Rotate(_playerTransform, input);

            var floatInput = input.y == 0 ? DecreaseSpeed() : OverclockSpeed();

            speed = _playerTransform.right * ((float)floatInput * deltaTime * _playerSettingsConfig.MovementSpeed);

            _playerTransform.transform.localPosition += (Vector3)speed;
        }

        private double OverclockSpeed()
        {
            if (_player.OverclockingSpeed >= 1)
                return _player.OverclockingSpeed;
            _player.OverclockingSpeed += 0.0166666;
            return _player.OverclockingSpeed;
        }

        private double DecreaseSpeed()
        {
            if (_player.OverclockingSpeed <= 0)
            {
                _player.OverclockingSpeed = 0;
                return _player.OverclockingSpeed;
            }
            
            _player.OverclockingSpeed -= 0.0166666;
            return _player.OverclockingSpeed;
        }

        public Vector3 GetPlayer()
        {
            return _speed;
        }
    }

    public interface ISpeed
    {
        Vector3 GetPlayer();
    }
}