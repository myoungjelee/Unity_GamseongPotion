using UnityEngine;
using TMPro;
using System.Collections;
using DG.Tweening;
using Unity.VisualScripting;


public class Customer : MonoBehaviour
{
    private Vector3 initPos;

    [Header("스킨")]
    public Mesh[] skins;
    private SkinnedMeshRenderer skinRenderer;

    [Header("대화")]
    public GameObject textUI;
    public TextMeshProUGUI dialogueText;
    public string selectedDialogue;
    private string currentDialogue;
    private string correctAnswer;
    public Coroutine currentCoroutine;
    public SphereCollider collision;
    private bool isSetText;

    private string[] dialogues = new string[21]
    {
        "치유의 포션 파는것 맞죠? 한병 주시죠. 혹시 몰라서 한병 장만해두려 합니다",
        "*콜록*  *콜록* 포션*콜록* *콜록* 주세요*콜록* ",
        "오늘 밭에서 작물을 수확하며 제 할 일을 하고 있었는데, 갑자기 무언가가 제 발목을 잡는 게 느껴졌습니다! 보니까 커다란 벌레더군요. 녀석이 문 곳이 무척 아픕니다. 치료해주시겠어요?",
        "말하자면, 내가 \"보살펴야 할\" 사람이 목욕탕에 많이 간다고 합시다. 그런 사람들은 증기가 건강에 아주 좋다고 생각하지요. 물통에 추가하여 증기를 덜... 건강하게 만들만한 것이 있을까요",
        "바퀴벌레가 집에 침입했어요! 집이 완전히 그들의 소굴이 되었습니다! 끔찍해요! 전 이렇게 살 수 없다고요! 무언가 저 벌레들을 한 번에 없앨 수 있는 것을 주세요.",
        "사업 경쟁자를 몰래 해치울 방법이 있을까요? 음식에 뭔가 타면 되지 않을까요?",
        "뭔가 폭파해야 하는데, 그런 주문은 하나도 모릅니다... 도와주실 수 있나요?",
        "광산에서 일하는데 폭발 포션이 필요합니다.",
        "바위 골렘이나 강철 골렘을 물리치는 데 도움이 될만한 포션이 있나요?",
        "동굴을 파냈는데 괴물로 가득하더군요. 불을 내뿜는 괴물 말입니다. 녀석들의 불을 막고 그놈들을 꽁꽁 얼릴만한 포션이 필요합니다.",
        "냉동 보관함이 고장났습니다! 고기나 채소 같은 음식을 신선하게 보존시켜주는 포션이 필요합니다.",
        "안녕하세요! 아시다시피 맥주는 차갑게 마셔야 좋습니다. 하지만 여름 동안이나 남반구에서는 그대로 유지하기가 어렵지요. 언제나 청량감이 유지되는 맥주를 만들고 싶어요! 도움이 될 만한 포션이 있을까요?",
        "닭장에 들어갈 때마다 제가 누굴 수프에 넣을지 고르고 있다는 걸 닭들도 아는 것 같습니다. 그래서 미친 듯이 달아나더군요! 닭 모이에 녀석들을 느리게 만들어 주는 걸 넣고 싶습니다.",
        "안녕하세요! 들어봐요. 그 지역에 미친 물고기가 있어요. 낚시꾼들은 모두 안답니다. 미끼를 물자마자 미친 듯이 화를 내며 죽어라 하고 헤엄쳐요. 그 어떤 낚싯줄로도 감당할 수 없죠! 미끼에 물고기의 속도를 늦추는 포션을 바르고 싶어요. 제발 도와주세요!",
        "재빠른 적의 속도를 늦춰주는 포션이 필요합니다.",
        "경비병으로부터 도망치거나 숨는 데 도움이 될만한 포션이 필요합니다.",
        "제 말이 경주에서 이기게 해줄 포션이 필요합니다.",
        "전 거북이만큼 빨라지고 싶습니다! 네, 어떤 거북이는 아주 재빠르기로 유명하거든요!",
        "지금 하고 있는 실험에 주술의 포션이 필요합니다.",
        "지팡이에 망자를 되살리는 마법을 걸고 싶습니다.",
        "위대하신 그분을 소환하고싶습니다! "
    };

    private void Awake()
    {
        skinRenderer = transform.Find("Character").GetComponent<SkinnedMeshRenderer>();
        initPos = new Vector3(-3f, 0, 11.1f);
        transform.position = initPos;
    }

    private void OnEnable()
    {
        StopConversation();
        StartCoroutine(InitCustomer());
    }

    IEnumerator InitCustomer()
    {
        SetRandomCharacter();
        float randomDelay = Random.Range(2f, 6f); // 2초에서 5초 사이의 랜덤 시간
        yield return new WaitForSecondsRealtime(randomDelay);
        AudioManager.Instance.PlaySfx(AudioManager.Sfx.Bell);
        transform.DOMoveX(-0.2f, 2).OnComplete(() => {
            collision.enabled = true;
        });
    }

    private void OnDisable()
    {
        transform.position = initPos;
        textUI.SetActive(false);
        collision.enabled = false;
        StopConversation();
    }

    void SetRandomCharacter()
    {
        int randomDialogue = Random.Range(0, dialogues.Length);
        selectedDialogue = dialogues[randomDialogue];

        int randomMesh = Random.Range(0, skins.Length);
        skinRenderer.sharedMesh = skins[randomMesh];
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SetText();
        }
    }

    public void SetText()
    {
        if (DOTween.IsTweening(dialogueText)) return;

        StopConversation();

        currentCoroutine = StartCoroutine(SetTextRoutine());
    }

    IEnumerator SetTextRoutine()
    {
        textUI.SetActive(true);
        dialogueText.text = "";
        dialogueText.DOKill();

        Tween textTween = dialogueText.DOText(selectedDialogue, 3);
        AudioManager.Instance.PlaySfx(AudioManager.Sfx.Dialogue);
        yield return textTween.WaitForCompletion();
        AudioManager.Instance.StopSfx(AudioManager.Sfx.Dialogue);

        yield return new WaitForSeconds(5);
        textUI.SetActive(false);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StopConversation();
            AudioManager.Instance.StopSfx(AudioManager.Sfx.Dialogue);
            textUI.SetActive(false);
        }
    }

    public bool OnPotionResult(string potionName)
    {
        string correctPotion = GetCorrectPotionForDialogue(selectedDialogue);

        if (potionName == correctPotion)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    string GetCorrectPotionForDialogue(string dialogue)
    {
        if (dialogue.Contains("치유") || dialogue.Contains("콜록") || dialogue.Contains("치료")) return "Healing potion";
        if (dialogue.Contains("건강") || dialogue.Contains("바퀴벌레") || dialogue.Contains("경쟁자")) return "Addiction potion";
        if (dialogue.Contains("폭파") || dialogue.Contains("광산") || dialogue.Contains("골렘")) return "Explosion potion";
        if (dialogue.Contains("꽁꽁") || dialogue.Contains("냉동") || dialogue.Contains("맥주")) return "Coolness potion";
        if (dialogue.Contains("닭장") || dialogue.Contains("물고기") || dialogue.Contains("속도")) return "Deceleration potion";
        if (dialogue.Contains("경비병") || dialogue.Contains("경주") || dialogue.Contains("거북이")) return "Acceleration potion";
        if (dialogue.Contains("주술") || dialogue.Contains("망자") || dialogue.Contains("소환")) return "Necromancy potion";

        return string.Empty;
    }

    public void StopConversation()
    {
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
            currentCoroutine = null;
        }
        dialogueText.DOKill();
        dialogueText.text = "";
    }
}
