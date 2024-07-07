using DG.Tweening;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class MagicPocket : MonoBehaviour
{
    public Transform pocketWorld;
    private HashSet<XRGrabInteractable> itemsInPocket = new HashSet<XRGrabInteractable>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Herb") || other.gameObject.CompareTag("HerbPowder") || other.gameObject.CompareTag("Potion"))
        {
            XRGrabInteractable item = other.gameObject.GetComponent<XRGrabInteractable>();
            Rigidbody rigidbody = other.gameObject.GetComponent<Rigidbody>();   

            if (item != null && item.isSelected && !itemsInPocket.Contains(item))
            {
                item.gameObject.transform.SetParent(pocketWorld);
                item.gameObject.layer = LayerMask.NameToLayer("Stencil");

                // HerbGrabInteractable 또는 PotionGrabInteractable 인지 확인
                var pocketHerb = item as HerbsGrabInteractable;
                var pocketPotion = item as PotionGrabInteractable;         

                if (pocketHerb != null)
                {
                    pocketHerb.isInPocket = true;
                    item.gameObject.transform.DOScale(pocketHerb.originScale / 5, 0.5f);
                }
                else if (pocketPotion != null)
                {
                    pocketPotion.isInPocket = true;
                    item.gameObject.transform.DOScale(pocketPotion.originScale / 5, 0.5f);
                }

                itemsInPocket.Add(item);

                AudioManager.Instance.PlaySfx(AudioManager.Sfx.MagicPocket);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Herb") || other.gameObject.CompareTag("HerbPowder") || other.gameObject.CompareTag("Potion"))
        {
            XRGrabInteractable item = other.gameObject.GetComponent<XRGrabInteractable>();

            if (item != null && item.isSelected && itemsInPocket.Contains(item))
            {

                item.gameObject.transform.SetParent(null);
                item.gameObject.layer = LayerMask.NameToLayer("Default");

                // HerbGrabInteractable 또는 PotionGrabInteractable 인지 확인
                var pocketHerb = item as HerbsGrabInteractable;
                var pocketPotion = item as PotionGrabInteractable;

                if (pocketHerb != null)
                {
                    pocketHerb.isInPocket = false;
                    item.gameObject.transform.DOScale(pocketHerb.originScale, 0.5f);
                }
                else if (pocketPotion != null)
                {
                    pocketPotion.isInPocket = false;
                    item.gameObject.transform.DOScale(pocketPotion.originScale, 0.5f);
                }

                itemsInPocket.Remove(item);

                AudioManager.Instance.PlaySfx(AudioManager.Sfx.MagicPocket);
            }
        }
    }
}
