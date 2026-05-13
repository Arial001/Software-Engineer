using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class JoystickInputListener : MonoBehaviour
{
    [Header("Gamepad Settings")]
    public int gamepadIndex = 0; // Index of the gamepad to use
    private Gamepad gamepad;
    private float previousRightStickX = 0f; // Store previous horizontal value
    private float previousRightStickY = 0f; // Store previous vertical value

    private void Start()
    {
        // Get the gamepad based on index
        if (Gamepad.all.Count > gamepadIndex)
        {
            gamepad = Gamepad.all[gamepadIndex];
            Debug.Log($"Gamepad {gamepadIndex} connected: {gamepad.displayName}");
            foreach (var control in gamepad.allControls)
            {
                Debug.Log($"Control Name: {control.name}, Type: {control.GetType()}");
            }
        }
        else
        {
            Debug.LogError("Gamepad index is invalid or not connected!");
        }
    }

    private void Update()
    {
        if (gamepad != null)
        {
            // Check button using GetChildControl with key "buttonSouth" (usually A on Xbox, Cross on PlayStation)
            var buttonA = gamepad.buttonSouth as ButtonControl;

            if (buttonA != null && buttonA.wasPressedThisFrame)
            {
                TriggerAction();
            }
        }

        // Read left stick position
        var leftStick = gamepad.leftStick as StickControl;
        Vector2 leftStickPosition = leftStick.ReadValue();
        if (leftStick != null && leftStickPosition != Vector2.zero)
        {
            Debug.Log($"Left Stick Position: {leftStickPosition}");
        }

        if (gamepad != null)
        {
            // Read right stick values
            var rightStickXControl = gamepad.rightStick.x as AxisControl;
            var rightStickYControl = gamepad.rightStick.y as AxisControl;

            if (rightStickXControl != null && rightStickYControl != null)
            {
                float rightStickX = rightStickXControl.ReadValue();
                float rightStickY = rightStickYControl.ReadValue();

                // Check if stick values have changed compared to previous frame
                if (rightStickX != previousRightStickX || rightStickY != previousRightStickY)
                {
                    Debug.Log($"Right Stick Position Changed - Horizontal: {rightStickX}, Vertical: {rightStickY}");

                    // Update previous values
                    previousRightStickX = rightStickX;
                    previousRightStickY = rightStickY;
                }
            }
        }
    }

    private void TriggerAction()
    {
        // Add action to perform when button A is pressed
        Debug.Log("Action triggered by Button A!");
    }
}
