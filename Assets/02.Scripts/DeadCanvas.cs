using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeadCanvas : MonoBehaviour
{
    [SerializeField] private Button restartBtn;
    [SerializeField] private Button exitBtn;

    private void OnEnable()
    {
        restartBtn.onClick.AddListener(() => SceneManager.LoadScene("GameScene"));
        exitBtn.onClick.AddListener(Application.Quit);
    }
}
