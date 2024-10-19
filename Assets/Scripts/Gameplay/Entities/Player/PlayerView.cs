using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shark.Gameplay.Entities.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerView : MonoBehaviour
    {
        public Action<bool> OnToggledActiveAndEnabled;

        public Vector2 MovementDirection { private get; set; }

        private Rigidbody2D _rb;

        [SerializeField, Min(1.0f)] 
        private float _speed = 5;

        private void Start()
        {
            Utilities.CommonUtils.LoadComponent(gameObject, out _rb);
            OnToggledActiveAndEnabled?.Invoke(isActiveAndEnabled);
        }

        private void OnEnable() => OnToggledActiveAndEnabled?.Invoke(isActiveAndEnabled);
        private void OnDisable() => OnToggledActiveAndEnabled?.Invoke(isActiveAndEnabled);

        private void FixedUpdate()
        {
            if (_rb != null)
            {
                MovementDirection.Normalize();
                _rb.MovePosition(_rb.position + _speed * Time.fixedDeltaTime * MovementDirection);
            }
        }
    }
}