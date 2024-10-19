using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shark.Gameplay.Entities.Player
{
    public class PlayerInitializer : MonoBehaviour
    {
        private void Start()
        {
            TryInitializeWithPropertiesRigidBodyComponent();

            var model = new PlayerModel();
            var view = Utilities.CommonUtils.LoadComponent<PlayerView>(gameObject);
            var ui = new PlayerUIController();

            new PlayerController(model, view, ui);
            Destroy(this);
        }

        private void TryInitializeWithPropertiesRigidBodyComponent()
        {
            if (!gameObject.TryGetComponent<Rigidbody2D>(out var rigidbody))
            {
                rigidbody = gameObject.AddComponent<Rigidbody2D>();
                rigidbody.bodyType = RigidbodyType2D.Dynamic;
                rigidbody.gravityScale = .0f;
                rigidbody.drag = 1.0f;
                rigidbody.angularDrag = .0f;
                rigidbody.freezeRotation = true;
            }
        }
    }
}