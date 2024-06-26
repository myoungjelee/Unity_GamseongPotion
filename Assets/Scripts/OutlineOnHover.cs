using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineOnHover : MonoBehaviour
{
    public string outlineMaterialPath = "Materials/M_Outline"; // Resources 폴더 내의 머티리얼 경로

    private Material outlineMaterial;
    private MeshRenderer meshRenderer;
    private Material[] originalMaterials; // 원래의 머티리얼 배열

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        if (meshRenderer != null)
        {
            // 원래의 머티리얼 배열을 저장합니다.
            originalMaterials = meshRenderer.materials;
        }

        // Resources 폴더에서 아웃라인 머티리얼을 로드합니다.
        outlineMaterial = Resources.Load<Material>(outlineMaterialPath);

        if (outlineMaterial == null)
        {
            Debug.LogError("Outline Material을 로드할 수 없습니다. 경로를 확인하세요: " + outlineMaterialPath);
        }
    }

    // 호버 시작 시 호출되는 메서드
    public void OnHoverEnter()
    {
        if (meshRenderer != null && outlineMaterial != null)
        {
            // 기존 머티리얼 배열에 아웃라인 머티리얼을 추가합니다.
            Material[] newMaterials = new Material[originalMaterials.Length + 1];
            originalMaterials.CopyTo(newMaterials, 0);
            newMaterials[newMaterials.Length - 1] = outlineMaterial;

            // 새로운 머티리얼 배열을 설정합니다.
            meshRenderer.materials = newMaterials;
        }
    }

    // 호버 종료 시 호출되는 메서드
    public void OnHoverExit()
    {
        if (meshRenderer != null)
        {
            // 원래의 머티리얼 배열로 되돌립니다.
            meshRenderer.materials = originalMaterials;
        }
    }
}
