using System.Collections.Generic;
using UnityEngine;

public class Cauldron : MonoBehaviour
{
    private List<string> herbPowders = new List<string>();

    private float spawnDistance = 1.0f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("HerbPowder"))
        {
            HerbPowder powder = other.gameObject.GetComponent<HerbPowder>();

            if (powder != null)
            {
                // 가루를 항아리에 추가
                herbPowders.Add(powder.powderName);

                // 가루 오브젝트 삭제
                Destroy(other.gameObject);
            }
        }
    }

    // 지금 호출 안해주고 있음 호출해주는 부분 구현할것!!!!!!!!!!
    public void Stir()
    {
        // 조합식을 확인하여 포션 생성
        string potion = CheckCombination();

        if (potion != null)
        {
            Debug.Log("포션 생성됨: " + potion);
            SpawnPotion(potion);
        }
        else
        {
            Debug.Log("유효한 조합식이 없습니다.");
        }

        // 항아리 초기화
        herbPowders.Clear();
    }

    // 구현할것!!!! 조합식 구현해줄것
    private string CheckCombination()
    {
        // 예시 조합식
        if (herbPowders.Contains("Mosquito Mushroom") && herbPowders.Contains("Watery plant"))
        {
            return "Acceleration potion";
        }
        else if (herbPowders.Contains("Daffodil of fire") && herbPowders.Contains("Mosquito Mushroom"))
        {
            return "Addiction potion";
        }
        else if (herbPowders.Contains("Watery plant") && herbPowders.Contains("fluffy crystal"))
        {
            return "Coolness potion";
        }
        else if (herbPowders.Contains("fluffy crystal") && herbPowders.Contains("Daffodil of fire"))
        {
            return "Deceleration potion";
        }
        else if (herbPowders.Contains("Blood Garnet") && herbPowders.Contains("Daffodil of fire"))
        {
            return "Explosion potion";
        }
        else if (herbPowders.Contains("Blood Garnet") && herbPowders.Contains("Watery plant"))
        {
            return "Healing potion";
        }
        else if (herbPowders.Contains("Mosquito Mushroom") && herbPowders.Contains("Blood Garnet") && herbPowders.Contains("fluffy crystal"))
        {
            return "Necromancy potion";
        }


        return null;
    }

    public void SpawnPotion(string potionName)
    {
        // Resources 폴더에서 프리팹 로드
        GameObject potionPrefab = Resources.Load<GameObject>($"Prefabs/Potions/{potionName}");

        if (potionPrefab != null)
        {
            // 병 앞쪽 위치 계산
            Vector3 spawnPosition = transform.position + transform.up * spawnDistance;

            // 프리팹을 병 앞쪽 위치에 인스턴스화
            Instantiate(potionPrefab, spawnPosition, Quaternion.identity);
        }
        else
        {
            Debug.LogError($"potionPrefab with name {potionName} not found in Resources/Prefabs.");
        }
    }
}
