using System.Collections.Generic;
using UnityEngine;

public class Soup : MonoBehaviour
{
    private List<string> herbPowders = new List<string>();
    private MeshRenderer mdshRenderer;
    private Color originColor;
    private Color currentColor;
    private float spawnDistance = 1.0f;

    private float currentTime;
    private float spawnTime = 10f;

    private void Awake()
    {
        mdshRenderer = GetComponent<MeshRenderer>();
        originColor = mdshRenderer.material.color;
        //Debug.Log(originColor.ToString());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("HerbPowder"))
        {
            HerbPowder powder = other.gameObject.GetComponent<HerbPowder>();

            if (powder != null)
            {
                // 가루를 항아리에 추가
                herbPowders.Add(powder.powderName);
                //Debug.Log($"Added powder: {powder.powderName}"); // 추가된 가루의 이름을 출력

                // 색상 혼합
                MixColor(powder.powderColor);

                // 가루 오브젝트 삭제
                Destroy(other.gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Ladle"))
        {
            currentTime = 0;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Ladle"))
        {
            Ladle ladle = other.gameObject.GetComponent<Ladle>();

            if(ladle.isGrabbed)
            {
                currentTime += Time.deltaTime;

                if (currentTime > spawnTime)
                {
                    Stir();
                }
            }
            else
            {
                currentTime = 0;
            }
        }
    }

    private void MixColor(Color newColor)
    {
        // 현재 색상과 새로운 색상을 혼합
        currentColor = Color.Lerp(currentColor, newColor, 0.5f);

        // 수프의 색상 변경
        mdshRenderer.material.color = currentColor;
    }

    public void Stir()
    {
        string potion = CheckCombination();

        if (potion != null)
        {
            //Debug.LogWarning("포션 생성됨: " + potion);
            SpawnPotion(potion);
            currentTime = 0;
        }
        else
        {
            Debug.Log("유효한 조합식이 없습니다.");
        }

        // 항아리 초기화
        herbPowders.Clear();

        // 색상 초기화
        currentColor = originColor;
        mdshRenderer.material.color = currentColor;
    }

    private string CheckCombination()
    {
        //foreach (string herb in herbPowders)
        //{
        //    Debug.Log($"Herb in list: {herb}");
        //}

        if (herbPowders.Contains("Mosquito Mushroom") && herbPowders.Contains("Watery Plant"))
        {
            return "Acceleration Potion";
        }
        else if (herbPowders.Contains("Daffodil Of Fire") && herbPowders.Contains("Mosquito Mushroom"))
        {
            return "Addiction Potion";
        }
        else if (herbPowders.Contains("Watery Plant") && herbPowders.Contains("Fluffy Crystal"))
        {
            return "Coolness Potion";
        }
        else if (herbPowders.Contains("Fluffy Crystal") && herbPowders.Contains("Daffodil Of Fire"))
        {
            return "Deceleration Potion";
        }
        else if (herbPowders.Contains("Blood Garnet") && herbPowders.Contains("Daffodil Of Fire"))
        {
            return "Explosion Potion";
        }
        else if (herbPowders.Contains("Blood Garnet") && herbPowders.Contains("Watery Plant"))
        {
            return "Healing Potion";
        }
        else if (herbPowders.Contains("Mosquito Mushroom") && herbPowders.Contains("Blood Garnet") && herbPowders.Contains("Fluffy Crystal"))
        {
            return "Necromancy Potion";
        }

        return null;
    }

    public void SpawnPotion(string potionName)
    {
        GameObject potionPrefab = Resources.Load<GameObject>($"Prefabs/Potions/{potionName}");

        if (potionPrefab != null)
        {
            Vector3 spawnPosition = transform.position + transform.up * spawnDistance;
            Instantiate(potionPrefab, spawnPosition, Quaternion.identity);
        }
        else
        {
            Debug.LogError($"potionPrefab with name {potionName} not found in Resources/Prefabs.");
        }
    }
}
