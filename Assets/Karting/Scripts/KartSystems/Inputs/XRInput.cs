using UnityEngine;
using UnityEngine.InputSystem;

namespace KartGame.KartSystems {
    public class XRInput : BaseInput
    {
        [Header("VR Input Actions")]
        public InputActionProperty accelerateAction;
        public InputActionProperty brakeAction;
        public InputActionProperty steerAction;

        [Header("Settings")]
        public float turnSensitivity = 1.5f;
        public float inputThreshold = 0.1f;

        private void OnEnable()
        {
            accelerateAction.action?.Enable();
            brakeAction.action?.Enable();
            steerAction.action?.Enable();
        }

        private void OnDisable()
        {
            accelerateAction.action?.Disable();
            brakeAction.action?.Disable();
            steerAction.action?.Disable();
        }

        public override InputData GenerateInput()
        {
            float accelerate = accelerateAction.action?.ReadValue<float>() ?? 0f;
            float brake = brakeAction.action?.ReadValue<float>() ?? 0f;
            var steer = steerAction.action?.ReadValue<Vector2>() ?? Vector2.zero;

            return new InputData
            {
                Accelerate = accelerate > inputThreshold,
                Brake = brake > inputThreshold,
                TurnInput = Mathf.Clamp(-steer.x * turnSensitivity, -1f, 1f)
            };
        }
    }
}
