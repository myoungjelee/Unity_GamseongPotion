using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GrindingBat : MonoBehaviour
{
    [Header("절구 빻는 횟수")]
    public int count = 0;

    [Header("가루가 될 횟수")]
    public int grindCount = 5;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Herb"))
        {
            Herb herb = collision.gameObject.GetComponent<Herb>();

            if (herb != null && !herb.isChange)
            {
                count++;

                if (count >= grindCount)
                {
                    // Resources 폴더에서 가루 프리팹 로드
                    GameObject powderPrefab = Resources.Load<GameObject>($"Prefabs/HerbPowders/P_{herb.data.name}"); 

                    if (powderPrefab != null)
                    {
                        // 가루 프리팹을 허브 위치에 생성
                        GameObject powder = Instantiate(powderPrefab, collision.transform.position, collision.transform.rotation);

                        HerbPowder powderScript = powder.GetComponent<HerbPowder>();
                        powderScript.powderName = herb.data.name;
                    }
                    else
                    {
                        Debug.LogError("PowderPrefab을 Resources 폴더에서 찾을 수 없습니다.");
                    }

                    // 현재 허브 오브젝트 제거
                    Destroy(collision.gameObject);

                    // 카운트를 초기화하고 변환 플래그 설정
                    count = 0;
                    herb.isChange = true;
                }
            }
        }
    }
}
