using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using DG.Tweening;
using System.Collections; 

public class SellSocketInteractor : XRSocketInteractor
{
    [Header("손님")]
    public Customer customer; 

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);

        if (customer == null) return;

        XRBaseInteractable interactableObject = args.interactableObject as XRBaseInteractable;
        if (interactableObject != null)
        {
            Potion potion = interactableObject.GetComponent<Potion>();

            if (potion != null)
            {
                if (customer.OnPotionResult(potion.data.name))
                {
                    // 코인 세팅 (코인프리팹 만들고 주석풀기)
                    GameObject coinPrefab = Resources.Load<GameObject>($"Prefabs/Coin");
                    if (coinPrefab != null)
                    {
                        GameObject spawnCoin = Instantiate(coinPrefab, transform.position, Quaternion.identity);
                        Coin coin = spawnCoin.GetComponent<Coin>();
                        if (coin != null)
                        {
                            coin.coin = potion.data.sellPrice;
                        }
                    }
                    else
                    {
                        Debug.LogError("코인 프리팹을 로드할 수 없습니다.");
                    }

                    Destroy(interactableObject.gameObject);

                    // 대사 완료 후 이동
                    customer.dialogueText.text = "";
                    Sequence sequence = DOTween.Sequence();
                    sequence.Append(customer.dialogueText.DOText("고맙습니다!", 1));
                    sequence.Append(customer.transform.DOMoveX(3, 3));
                    sequence.OnComplete(() => StartCoroutine(OnOff()));
                }
                else
                {
                    customer.dialogueText.text = "";
                    customer.dialogueText.DOText("이건 제가 원한 것이 아니에요.", 1);
                }
            }
        }
        else
        {
            Debug.LogError("포션 NULL");
        }
    }

    IEnumerator OnOff()
    {
        customer.gameObject.SetActive(false);

        yield return new WaitForSeconds(0.5f);

        customer.gameObject.SetActive(true);
    }
}
