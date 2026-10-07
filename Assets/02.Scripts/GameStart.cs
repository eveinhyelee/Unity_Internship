using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
    [SerializeField] private Button gameStartBtn;
    [SerializeField] private Button exitBtn;

    private void OnEnable()
    {
        gameStartBtn.onClick.AddListener(() => SceneManager.LoadScene("GameScene"));
        exitBtn.onClick.AddListener(Application.Quit);
    }
}