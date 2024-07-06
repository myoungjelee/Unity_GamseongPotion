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
                // HerbGrabInteractable 또는 PotionGrabInteractable 인지 확인
                var pocketHerb = item as HerbsGrabInteractable;
                var pocketPotion = item as PotionGrabInteractable;

                if (pocketHerb != null)
                {
                    pocketHerb.isInPocket = true;                   
                }
                else if (pocketPotion != null)
                {
                    pocketPotion.isInPocket = true;
                }

                if (item.gameObject.transform.parent != pocketWorld)
                {
                    item.gameObject.transform.SetParent(pocketWorld);
                }

                item.gameObject.layer = LayerMask.NameToLayer("Stencil");

                // NameTag 태그를 가진 자식 객체 비활성화
                Transform nameTag = item.gameObject.transform.Find("NameTag");
                if (nameTag != null)
                {
                    nameTag.gameObject.SetActive(false);
                }

                itemsInPocket.Add(item);

                //AudioManager.Instance.PlaySfx(AudioManager.Sfx.MagicPocket);

                //Debug.Log($"{item.gameObject.name}가 Magic Pocket에 들어갔습니다.");
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
                    //item.gameObject.transform.DOScale(pocketHerb.originScale, 1);
                }
                else if (pocketPotion != null)
                {
                    pocketPotion.isInPocket = false;
                    //item.gameObject.transform.DOScale(pocketPotion.originScale, 1);
                }

                itemsInPocket.Remove(item);

                AudioManager.Instance.PlaySfx(AudioManager.Sfx.MagicPocket);
                //Debug.Log($"{item.gameObject.name}가 Magic Pocket에서 나갔습니다.");
            }
        }
    }
}
