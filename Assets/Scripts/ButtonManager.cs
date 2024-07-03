using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    private WarningPanel warningPanel;

    private void Awake()
    {
         warningPanel = transform.parent.gameObject.GetComponent<WarningPanel>();
    }

    public void OnClick_GoToBedRoom()
    {
        GameManager.Instance.GoToBedRoom();
    }

    public void OnClick_GoToMainHall()
    {
        GameManager.Instance.GoToMainHall();
    }

    public void OnClick_CancleButton()
    {
        warningPanel.UIFadeOut();
    }
}
