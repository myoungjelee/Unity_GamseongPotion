using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class YButtonHandler : MonoBehaviour
{
    public InputActionProperty yButtonAction;

    void OnEnable()
    {
        yButtonAction.action.performed += OnYButtonPressed;
    }

    void OnDisable()
    {
        yButtonAction.action.performed -= OnYButtonPressed;
    }

    private void OnYButtonPressed(InputAction.CallbackContext context)
    {
        GameManager.Instance.customerCount = 10;
        GameManager.Instance.currentState = GameManager.State.CanSleep;
       // Debug.Log($"손님 숫자 : {GameManager.Instance.customerCount}, 잘수있나? : {GameManager.Instance.currentState}");
    }

}
