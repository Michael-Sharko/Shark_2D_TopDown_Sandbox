using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

namespace Shark.Gameplay.Entities.Player
{
    public class PlayerController
    {
        private PlayerModel _model;
        private PlayerView _view;
        private PlayerUIController _ui;

        private PlayerInputActions _inputActions = new();

        public PlayerController(PlayerModel model, PlayerView view, PlayerUIController ui)
        {
            _model = model;
            _view = view;
            _ui = ui;

            _view.OnToggledActiveAndEnabled = HandleToggledActiveAndEnabled;
        }

        #region Unity Active and Enabled state switching
        private void HandleToggledActiveAndEnabled(bool enabled) 
        {
            if (enabled) HandleEnabledState();
            else HandleDisabledState();
        }

        public void HandleEnabledState()
        {
            _inputActions.Player.Enable();
            _inputActions.Player.Movement.performed += OnMovementPerformed;
        }

        public void HandleDisabledState()
        {
            _inputActions.Player.Movement.performed -= OnMovementPerformed;
            _inputActions.Player.Disable();
        }
        #endregion

        #region Input action handlers
        private void OnMovementPerformed(CallbackContext context)
        {
            _view.MovementDirection = context.ReadValue<Vector2>();
        }
        #endregion
    }
}