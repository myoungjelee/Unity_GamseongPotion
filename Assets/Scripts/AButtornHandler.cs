using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class AButtonHandler : MonoBehaviour
{
    public InputActionProperty aButtonAction;

    void OnEnable()
    {
        aButtonAction.action.performed += OnXButtonPressed;
    }

    void OnDisable()
    {
        aButtonAction.action.performed -= OnXButtonPressed;
    }

    private void OnXButtonPressed(InputAction.CallbackContext context)
    {
        GameManager.Instance.ClearGameData();
    }
}
