using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        instance = this;
    }


    public void ChangeLevelTo(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }
}
