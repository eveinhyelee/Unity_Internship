using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class MonsterSlot : MonoBehaviour
{
    [SerializeField] private Image MonsterImg;
    [SerializeField] private Button Monsterbtn;
    private MonsterIndexCanvas monsterIndexCanvas;

    public void Init(Data data, MonsterIndexCanvas canvas)
    {
        monsterIndexCanvas = canvas;

        MonsterImg.sprite = data.sprite;
        
        Monsterbtn.onClick.AddListener(() => monsterIndexCanvas.ShowDetail(data));
    }
}