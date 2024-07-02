using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HerbPowder : MonoBehaviour
{
    public string powderName; // 허브의 종류를 저장
    public Color powderColor;

    [Header("허브 빻기")]
    //private bool isInGrinder = false;
    private Grinder grinder;
    private int count;
    private int grindCount = 5;
    private int powderState;

    private void Awake()
    {
        // 하이어라키에서 "Grinder" 태그를 가진 오브젝트를 찾음
        grinder = GameObject.FindWithTag("Grinder").GetComponent<Grinder>();

        if (grinder == null)
        {
            Debug.LogError("Grinder를 찾을 수 없습니다. 'Grinder' 태그를 가진 오브젝트가 존재하는지 확인하세요.");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (this.gameObject.name.Contains("_2")) return;

        if (collision.gameObject.CompareTag("GrindingBat"))
        {
            if (grinder.isInSide)
            {
                count++;

                Debug.Log(count);

                if (count >= grindCount)
                {
                    CreatePowder(2, this);
                }
            }
        }
    }


    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Grinder"))
        {
            //isInGrinder = false;
            Debug.Log("Exited Grinder");
        }
    }

    private void CreatePowder(int id, HerbPowder herbPowder)
    {
        // 첫 번째 단계
        GameObject powderPrefab = Resources.Load<GameObject>($"Prefabs/HerbPowders/P_{herbPowder.powderName}_{id}");

        if (powderPrefab != null)
        {
            // 가루 프리팹을 허브 위치에 생성
            HerbPowder spawnPowder = Instantiate(powderPrefab, transform.position, transform.rotation).GetComponent<HerbPowder>();

            spawnPowder.powderName = powderName;
            spawnPowder.powderColor = powderColor;

            Destroy(gameObject);
        }
        else
        {
            Debug.LogError($"Prefabs/HerbPowders/P_{herbPowder.powderName}_{id}을 Resources 폴더에서 찾을 수 없습니다.");
        }
    }
}
