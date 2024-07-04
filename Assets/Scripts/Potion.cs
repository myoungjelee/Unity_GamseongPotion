using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PotionData
{
    public string name;
    public int sellPrice;
}

public class Potion : MonoBehaviour
{
    [Header("포션 데이터")]
    public PotionData data;

    [Header("이름표")]
    public GameObject nameTag;
    private Transform player;

    private void Start()
    {
        // "Player" 태그를 가진 게임 오브젝트의 Transform을 찾습니다.
        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            player = Camera.main.transform;
        }
    }

    private void Update()
    {
        if (player != null)
        {
            // 플레이어를 바라보도록 회전
            Vector3 direction = player.position - nameTag.transform.position;
            direction.y = 0; // y 축 회전을 방지하여 네임태그가 수평을 유지하도록 함
            nameTag.transform.rotation = Quaternion.LookRotation(direction);
            nameTag.transform.forward *= -1;
        }
    }

    // 호버 시작 시 호출되는 메서드
    public void OnHoverEnter()
    {
        nameTag.SetActive(true);
    }

    // 호버 종료 시 호출되는 메서드
    public void OnHoverExit()
    {
        nameTag.SetActive(false);
    }
}
