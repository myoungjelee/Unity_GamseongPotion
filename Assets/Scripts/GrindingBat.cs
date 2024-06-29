using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GrindingBat : MonoBehaviour
{
    [Header("절구 빻는 횟수")]
    public int count = 0;

    [Header("가루가 될 횟수")]
    public int grindCount1 = 5;
    public int grindCount2 = 12;

    private GameObject powder1;

    private string herbName;
    private Color herbColor;

    [Header("방망이 되돌아가기")]
    public Transform grinder; // 절구통의 Transform
    private Vector3 originPos; // 방망이 처음 위치
    private Quaternion originRot; // 방망이 처음 회전값
    private float maxDistance = 1.8f; // 절구통에서 벗어날 최대 거리
    private bool isGrabbed = false;

    private bool isProcessing = false; // 가공 중인지 확인하는 플래그


    private void Awake()
    {
        originPos = transform.position;
        originRot = transform.rotation;
    }

    void Update()
    {
        if (!isGrabbed)
        {
            float distance = Vector3.Distance(transform.position, grinder.position);
            if (distance > maxDistance)
            {
                ReturnToOrigin();
            }
        }
    }

    private void ReturnToOrigin()
    {
        transform.position = originPos;
        transform.rotation = originRot;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!isGrabbed || isProcessing) return; // 가공 중이면 바로 반환

        if (collision.gameObject.CompareTag("Herb") || collision.gameObject.CompareTag("HerbPowder"))
        {
            Herb herb = collision.gameObject.GetComponent<Herb>();

            if (herb != null && !herb.isChange)
            {
                StartCoroutine(ProcessHerb(collision, herb));
            }
        }
    }

    private IEnumerator ProcessHerb(Collision collision, Herb herb)
    {
        isProcessing = true; // 가공 시작

        count++;

        if (count >= grindCount2)
        {
            CreatePowderStage2(herb);
        }
        else if (count >= grindCount1)
        {
            CreatePowderStage1(collision, herb);
        }

        yield return new WaitForSeconds(0.1f); // 0.1초 대기

        isProcessing = false; // 가공 완료
    }

    private void CreatePowderStage1(Collision collision, Herb herb)
    {
        // 첫 번째 단계
        GameObject powder1Prefab = Resources.Load<GameObject>($"Prefabs/HerbPowders/P_{herb.data.name}_1");

        if (powder1Prefab != null)
        {
            // 가루 프리팹을 허브 위치에 생성
            powder1 = Instantiate(powder1Prefab, collision.transform.position, collision.transform.rotation);

            herbName = herb.data.name;
            herbColor = herb.data.color;

            Destroy(collision.gameObject);
        }
        else
        {
            Debug.LogError($"Prefabs/HerbPowders/P_{herb.data.name}_1을 Resources 폴더에서 찾을 수 없습니다.");
        }
    }

    private void CreatePowderStage2(Herb herb)
    {
        // 두 번째 단계 (최종 가루 변환)
        GameObject powder2Prefab = Resources.Load<GameObject>($"Prefabs/HerbPowders/P_{herb.data.name}_2");

        if (powder2Prefab != null)
        {
            // 최종 가루 프리팹을 허브 위치에 생성
            GameObject powder2 = Instantiate(powder2Prefab, powder1.transform.position, powder1.transform.rotation);

            Debug.Log(powder2.gameObject.name);

            HerbPowder finalPowderScript = powder2.GetComponent<HerbPowder>();
            finalPowderScript.powderName = herbName;
            finalPowderScript.powderColor = herbColor;

            // 첫 번째 허브파우더 오브젝트 제거
            Destroy(powder1);

            // 카운트를 초기화하고 변환 플래그 설정
            count = 0;
            herb.isChange = true;
        }
        else
        {
            Debug.LogError($"Prefabs/HerbPowders/P_{herb.data.name}_2을 Resources 폴더에서 찾을 수 없습니다.");
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
