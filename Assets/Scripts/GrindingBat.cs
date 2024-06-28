using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GrindingBat : MonoBehaviour
{
    [Header("절구 빻는 횟수")]
    public int count = 0;

    [Header("가루가 될 횟수")]
    public int grindCount1 = 3;
    public int grindCount2 = 7;

    private GameObject powder1;

    string herbName;

    [Header("방망이 되돌아가기")]
    public Transform grinder; // 절구통의 Transform
    private Transform batPos; // 절구방망이가 돌아갈 위치 (절구 옆)
    public float maxDistance = 2.0f; // 절구통에서 벗어날 최대 거리
    private bool isGrabbed = false;

    private void Awake()
    {
        batPos = transform;
    }

    void Update()
    {
        if (!isGrabbed)
        {
            float distance = Vector3.Distance(transform.position, grinder.position);
            if (distance > maxDistance)
            {         
                transform.position = batPos.position;
                transform.rotation = batPos.rotation;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Herb"))
        {
            Herb herb = collision.gameObject.GetComponent<Herb>();

            if (herb != null && !herb.isChange)
            {
                count++;

                if (count >= grindCount1 && count < grindCount2)
                {
                    // 첫 번째 단계
                    GameObject powder1Prefab = Resources.Load<GameObject>($"Prefabs/HerbPowders/P_{herb.data.name}_1");

                    if (powder1Prefab != null)
                    {
                        // 가루 프리팹을 허브 위치에 생성
                        powder1 = Instantiate(powder1Prefab, collision.transform.position, collision.transform.rotation);
                     
                        herbName = herb.data.name;

                        Destroy(collision.gameObject);
                    }
                    else
                    {
                        Debug.LogError("PowderPrefab_1을 Resources 폴더에서 찾을 수 없습니다.");
                    }
                }
                else if (count >= grindCount2)
                {
                    // 두 번째 단계 (최종 가루 변환)
                    GameObject Powder2Prefab = Resources.Load<GameObject>($"Prefabs/HerbPowders/P_{herb.data.name}_2");

                    if (Powder2Prefab != null)
                    {
                        // 최종 가루 프리팹을 허브 위치에 생성
                        GameObject powder2 = Instantiate(Powder2Prefab, powder1.transform.position, powder1.transform.rotation);

                        HerbPowder finalPowderScript = powder2.GetComponent<HerbPowder>();
                        finalPowderScript.powderName = herbName;
                    }
                    else
                    {
                        Debug.LogError("PowderPrefab_2을 Resources 폴더에서 찾을 수 없습니다.");
                    }

                    // 첫번째 허브파우더 오브젝트 제거
                    Destroy(powder1);

                    // 카운트를 초기화하고 변환 플래그 설정
                    count = 0;
                    herb.isChange = true;
                }
            }
        }
    }

    public void OnGrab()
    {
        isGrabbed = true;
    }

    public void UnGrab()
    {
        isGrabbed = false;
    }
}
