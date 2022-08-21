using UnityEngine;
using UnityEngine.InputSystem;

namespace MainPlayer.Input
{
    public sealed class PlayerInputFromKeyboard 
    {
        public Vector2 PlayerInputDirection => _playerInputDirection;
        
        private readonly InputAction _playerAction;
        private readonly PlayerInput _playerInput;
        
        private Vector2 _playerInputDirection;

        public PlayerInputFromKeyboard(InputAction playerAction)
        {
            _playerAction = playerAction;
        }

        public void GetInput()
        {
            _playerInputDirection = _playerAction.ReadValue<Vector2>();
        }
    }
}