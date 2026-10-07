using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIMainCanvas : MonoBehaviour
{
    [SerializeField]private TextMeshProUGUI stageText;
    [SerializeField]private TextMeshProUGUI hpText;
    [SerializeField]private TextMeshProUGUI expText;
    [SerializeField]private TextMeshProUGUI levelText;
    [SerializeField] Slider hpSlider;
    [SerializeField] Slider expSlider;
    [SerializeField] GameObject DeadCanvas;
    private int curlevel;
    private int curStage;
    private int curHP;
    private int getExp;
    private int curExp;
    [SerializeField] private PlayerController player;

    public Data playerData;

    void Start() //초기화
    {
        DeadCanvas.SetActive(false);
        player = GameManager.instance.player.GetComponent<PlayerController>();;
        playerData = player.data;
        
        player.OnHPChanged += UpdateHPUI;
        player.OnEXPChanged += UpdateExpUI;
        
        UpdateHPUI(player.curHP); 
        UpdateExpUI(player.curEXP); 
        
        UpdateTxt();
        hpSlider.value = (float)playerData.MaxHP / playerData.MaxHP;
        expSlider.value = (float)curExp / playerData.MaxExp;
    }
    void Update()
    {
        UpdateTxt();
    }

    // void UpdateExpBar()
    // {
    //     expSlider.value += getExp;
    //     if (expSlider.value == 1)
    //     {
    //         GameManager.instance.Level =+ 1;
    //         expSlider.value = 0;
    //     }
    // }
    
    void UpdateTxt()
    {
        curlevel = GameManager.instance.Level;
        curStage = GameManager.instance.Stage;
        stageText.text = $"{curStage}";
        levelText.text = $"Level.{curlevel}";
    }
    void UpdateHPUI(int curHp)
    {
        hpText.text = $"{curHp}/{playerData.MaxHP}";
        hpSlider.value = (float)curHp / playerData.MaxHP;
    }
    void UpdateExpUI(int curExp)
    {
        expText.text = $"{curExp}/{playerData.MaxExp}";
        expSlider.value = (float)curExp / playerData.MaxExp;
    }
}