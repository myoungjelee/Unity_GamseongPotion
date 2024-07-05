using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;

public class CameraSwitch : MonoBehaviour
{
    public Camera initialCamera; // 초기 카메라
    public Camera vrCamera; // VR 카메라
    public Image startAnim;
    private float switchTime = 2f; // 전환 시간

    void Start()
    {
        if (GameManager.Instance.currentState == GameManager.State.CanSleep)
        {
            initialCamera.enabled = true;
            vrCamera.enabled = false;

            // DOTween 애니메이션 재생 및 완료 후 카메라 전환
            startAnim.GetComponent<DOTweenAnimation>().RecreateTweenAndPlay();

            StartCoroutine(SwitchCameraRoutine());

            GameManager.Instance.currentState = GameManager.State.BeAwake;
        }
    }

    IEnumerator SwitchCameraRoutine()
    {
        yield return new WaitForSeconds(switchTime);

        SwitchToVRCamera();
    }

    void SwitchToVRCamera()
    {
        initialCamera.enabled = false;
        vrCamera.enabled = true;
    }
}