using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIClass : MonoBehaviour
{
    public static UIClass instance;

    [SerializeField] private GameObject gameOverUI;
    [Space]
    [SerializeField] private TextMeshProUGUI timerText;


    private void Awake()
    {
        instance = this;
        Time.timeScale = 1;

    }

    private void Update()
    {
        timerText.text = Time.time.ToString("F1") + "s";
    }


    public void EnableGameOverUI()
    {
        Time.timeScale = .5f;
        gameOverUI.SetActive(true);

    }

    public void RestartLevel1()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(sceneIndex);
    }
}
