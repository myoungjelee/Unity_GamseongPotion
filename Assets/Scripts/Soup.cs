using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Soup : MonoBehaviour
{
    private List<string> herbPowders = new List<string>();
    private MeshRenderer mdshRenderer;
    private Color originColor;
    private Color currentColor;
    private float spawnDistance = 1.0f;

    private float currentTime;
    private float spawnTime = 10f;

    [SerializeField] private Image progressBar; 
    [SerializeField] private GameObject progressCanvas;

    public GameObject Player;

    private void Awake()
    {
        mdshRenderer = GetComponent<MeshRenderer>();
        originColor = mdshRenderer.material.color;
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
            UpdateProgressBar(0); // 프로그레스 바 초기화
            progressBar.color = Color.white;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Ladle"))
        {
            Ladle ladle = other.gameObject.GetComponent<Ladle>();

            if (ladle.isGrabbed)
            {
                currentTime += Time.deltaTime;
                UpdateProgressBar(currentTime / spawnTime); // 프로그레스 바 업데이트

                if (currentTime > spawnTime)
                {
                    Stir();
                }
            }
            else
            {
                currentTime = 0;
                UpdateProgressBar(0); // 프로그레스 바 초기화
                progressBar.color = Color.white;
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
            SpawnPotion(potion);
            currentTime = 0;
            progressBar.color = Color.green;
        }
        else
        {
            progressBar.color = Color.red;
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

    // UpdateProgressBar 메서드는 프로그레스 바(progressBar)의 진행 상태를 업데이트하고,
    // 특정 조건에 따라 프로그레스 바의 활성화 여부를 설정합니다.
    private void UpdateProgressBar(float progress)
    {
        // progressBar가 null이 아닐 때만 진행 상태를 업데이트하고 활성화 여부를 설정합니다.
        if (progressBar != null)
        {
            // Mathf.Clamp01 메서드를 사용하여 progress 값을 0과 1 사이의 값으로 제한합니다.
            progressBar.fillAmount = Mathf.Clamp01(progress);

            // progress 값이 0보다 큰 경우 progressBar를 활성화(보이게)하고,
            // 그렇지 않으면 비활성화(안 보이게)합니다.
            progressCanvas.gameObject.SetActive(progress > 0f);
        }
    }
}
