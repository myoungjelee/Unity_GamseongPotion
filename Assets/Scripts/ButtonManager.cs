using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public void OnClick_GoToBedRoom()
    {
        GameManager.Instance.GoToBedRoom();
    }

    public void OnClick_GoToMainHall()
    {
        GameManager.Instance.GoToMainHall();
    }
}
