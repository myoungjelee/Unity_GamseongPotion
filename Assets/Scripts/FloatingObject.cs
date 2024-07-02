using UnityEngine;
using DG.Tweening;

public class FloatingObject : MonoBehaviour
{
    public float minFloatDuration = 1f; // 떠오르고 내려가는 최소 시간
    public float maxFloatDuration = 3f; // 떠오르고 내려가는 최대 시간
    public float minFloatStrength = 0.2f; // 최소 이동 거리 (높이)
    public float maxFloatStrength = 1f; // 최대 이동 거리 (높이)

    void Start()
    {
        StartFloating();
    }

    void StartFloating()
    {
        // 초기 위치 저장
        Vector3 originalPosition = transform.localPosition;

        // 랜덤한 떠오르는 높이와 시간 설정
        float floatDuration = Random.Range(minFloatDuration, maxFloatDuration);
        float floatStrength = Random.Range(minFloatStrength, maxFloatStrength);

        // 둥둥 떠다니는 애니메이션
        transform.DOLocalMoveY(originalPosition.y + floatStrength, floatDuration)
            .SetLoops(-1, LoopType.Yoyo) // 무한 반복, Yoyo 방식으로 (원래 위치로 돌아왔다가 다시 이동)
            .SetEase(Ease.Linear); // 완만하게 이동
    }
}
