using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class XButtonHandler : MonoBehaviour
{
    public InputActionProperty xButtonAction;
    public GameObject magicPocket;
    public XRRayInteractor interactor;

    void OnEnable()
    {
        xButtonAction.action.performed += OnXButtonPressed;
        xButtonAction.action.canceled += OnXButtonReleased;
    }

    void OnDisable()
    {
        xButtonAction.action.performed -= OnXButtonPressed;
        xButtonAction.action.canceled -= OnXButtonReleased;
    }

    private void OnXButtonPressed(InputAction.CallbackContext context)
    {
        //Debug.Log("X 버튼이 눌렸습니다.");
        if (magicPocket != null)
        {
            magicPocket.SetActive(true);
            interactor.enabled = false;
            //AudioManager.Instance.PlaySfx(AudioManager.Sfx.MagicPocket);
        }
    }

    private void OnXButtonReleased(InputAction.CallbackContext context)
    {
        //Debug.Log("X 버튼이 떼졌습니다.");
        if (magicPocket != null)
        {
            magicPocket.SetActive(false);
            interactor.enabled = true;
            //AudioManager.Instance.PlaySfx(AudioManager.Sfx.MagicPocket);
        }
    }
}
