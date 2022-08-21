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

        private Vector2 _switchedDirection;
        private float _angleRotation;

        private Vector3 _speed;

        public PlayerMovement(PlayerInputFromKeyboard playerInputFromKeyboard,
            PlayerSettingsConfig playerSettingsConfig,
            Transform playerTransform)
        {
            _playerInputFromKeyboard = playerInputFromKeyboard;
            _playerSettingsConfig = playerSettingsConfig;
            _playerTransform = playerTransform;

            _playerRotate = new PlayerRotate(_playerSettingsConfig);
        }

        public void Move(float deltaTime)
        {
            var input = _playerInputFromKeyboard.PlayerInputDirection.normalized;

            _playerRotate.Rotate(_playerTransform, input);

            var floatInput = input.y == 0 ? 0 : 1;

            _speed = _playerTransform.right * (floatInput * deltaTime * _playerSettingsConfig.MovementSpeed);
            
            _playerTransform.transform.localPosition += _speed;
            
        }

        public Vector3 GetSpeed()
        {
            return _speed;
        }
    }

    public interface ISpeed
    {
        Vector3 GetSpeed();
    }
}