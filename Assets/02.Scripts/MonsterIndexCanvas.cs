using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class MonsterIndexCanvas : MonoBehaviour
{
    [SerializeField] private Image MonsterImg;
    [SerializeField] private TextMeshProUGUI MonsterNameTxt1;
    [SerializeField] private TextMeshProUGUI AttackSpeedNumTxt;
    [SerializeField] private TextMeshProUGUI ExpNumTxt;
    [SerializeField] private TextMeshProUGUI MoveSpeedNumTxt;
    [SerializeField] private TextMeshProUGUI DiscriptionTxt1;
    [SerializeField] private Button monsterIndexBtn;
    [SerializeField] private GameObject MonsterIndex;
    
    [SerializeField] private GameObject slotPrefab; // 슬롯 프리팹 (UI)
    [SerializeField] private Transform contentParent; // Viewport 하위의 Content

    void Start()
    {
        monsterIndexBtn.onClick.AddListener(OnButtonClick);
        MonsterIndex.SetActive(false);
        
        Data[] monsterDataList = LoadMonsterData();

        foreach (Data data in monsterDataList)
        {
            GameObject slot = Instantiate(slotPrefab, contentParent);
            MonsterSlot monsterSlot = slot.GetComponent<MonsterSlot>();
            monsterSlot.Init(data, this);
        }
    }

    public void ShowDetail(Data data)
    {
        // 캔버스에 정보 표시
        MonsterImg.sprite = data.sprite;
        MonsterNameTxt1.text = data.Name;
        AttackSpeedNumTxt.text = data.AttackSpeed.ToString("F2");
        ExpNumTxt.text = data.MaxExp.ToString();
        MoveSpeedNumTxt.text = data.MoveSpeed.ToString("F2");
        DiscriptionTxt1.text = data.Description;

        MonsterIndex.SetActive(true);
    }

    public void OnButtonClick()
    {
        MonsterIndex.SetActive(!MonsterIndex.activeSelf);
    }

    private Data[] LoadMonsterData()
    {
        GameObject[] monsterPrefabs = Resources.LoadAll<GameObject>("Enemy");
    
        List<Data> dataList = new List<Data>();

        foreach (GameObject prefab in monsterPrefabs)
        {
            EnemyController monsterComponent = prefab.GetComponent<EnemyController>();
            if (monsterComponent != null && monsterComponent.data != null)
            {
                dataList.Add(monsterComponent.data);
            }
            else
            {
                Debug.LogWarning($"Monster 또는 monsterData 누락됨: {prefab.name}");
            }
        }
        return dataList.ToArray();
    }
}
