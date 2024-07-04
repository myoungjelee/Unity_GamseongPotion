using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class PotionSocketInteractor : XRSocketInteractor
{
    public Image progressBar;
    public Soup soup;

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        // interactableObject가 PotionGrabInteractable 타입인지 확인합니다.
        if (args.interactableObject is PotionGrabInteractable potion)
        {
            base.OnSelectEntered(args);

            soup.isPotionSpawn = true;

            Debug.Log("포션생성!!");
        }
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);

        // interactableObject가 PotionGrabInteractable 타입인지 확인합니다.
        if (args.interactableObject is PotionGrabInteractable potion)
        {
            base.OnSelectExited(args);

            soup.isPotionSpawn = false;

            Debug.Log("포션 가져감!!");
        }
    }

}
