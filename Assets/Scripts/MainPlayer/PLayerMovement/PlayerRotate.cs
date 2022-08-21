using MainPlayer.PlayerSettings;
using UnityEngine;

namespace MainPlayer.PLayerMovement
{
    public sealed class PlayerRotate
    {
        private readonly PlayerSettingsConfig _playerSettingsConfig;
        
        public PlayerRotate(PlayerSettingsConfig playerSettingsConfig)
        {
            _playerSettingsConfig = playerSettingsConfig;
        }

        public void Rotate(Transform playerTransform, Vector2 input)
        {
            if (input.x == 0)
                return;
            
            var rotationInput = new Vector3(0f, 0f, -input.x);
            
            playerTransform.Rotate(rotationInput, _playerSettingsConfig.RotationSpeed);
        }
    }
}