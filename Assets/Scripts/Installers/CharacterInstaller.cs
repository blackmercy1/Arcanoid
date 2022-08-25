using MainPlayer;
using MainPlayer.Collision;
using MainPlayer.Input;
using MainPlayer.PlayerSettings;
using UnityEngine;
using UnityEngine.InputSystem;
using UpdatesSystem;

namespace Installers
{
    public sealed class CharacterInstaller : MonoBehaviour
    {
        [Header("Player")]
        [SerializeField] private PlayerSettingsConfig _playerSettingsConfig;
        [SerializeField] private InputAction _inputAction;
        [SerializeField] private Transform _playerTransform;
        [SerializeField] private PlayerMarker _playerMarker;
        [SerializeField] private Transform _gunHolder;

        [Header("Updates")]
        private FixedGameUpdates _fixedGameUpdates;
        private GameUpdates _gameUpdates;

        private PlayerControls _playerControls;
        
        public Player Initialize(FixedGameUpdates fixedGameUpdates, GameUpdates gameUpdates)
        {
            _playerControls = new PlayerControls();

            _fixedGameUpdates = fixedGameUpdates;
            _gameUpdates = gameUpdates;
            
            var playerInput = new PlayerInputFromKeyboard(_inputAction);
            _inputAction.Enable();
            var player = CreatePlayer(playerInput);
            
            DestroySelf();

            return player;
        }

        private Player CreatePlayer(PlayerInputFromKeyboard playerInputFromKeyboard)
        {
            var player = new Player(playerInputFromKeyboard, _playerSettingsConfig, _playerTransform, _playerControls,
                _playerMarker, _gunHolder, _inputAction);
            
            _gameUpdates.AddToUpdateList(player);
            _fixedGameUpdates.AddToUpdateList(player);

            return player;
        }

        private void DestroySelf()
        {
            Destroy(gameObject);
        }
    }
}