using UnityEngine;
using UnityEngine.InputSystem;

public class OculusControllerInput : MonoBehaviour
{
    [SerializeField] public InputAction aButtonAction;

    private void OnEnable()
    {
        aButtonAction.Enable();
    }

    private void OnDisable()
    {
        aButtonAction.Disable();
    }

    private void Update()
    {
        if (aButtonAction.ReadValue<float>() > 0.5f)
        {
            Debug.Log("A button pressed!");
            // Your code here to respond to the A button press
        }
    }
}