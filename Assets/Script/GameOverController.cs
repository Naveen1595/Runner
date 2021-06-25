using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverController : MonoBehaviour
{
    [SerializeField] Button Restart;
    [SerializeField] Button GameOver;


    private void Update()
    {
        Restart.onClick.AddListener(RestartFun);
        GameOver.onClick.AddListener(GameOverFun);
    }

    void RestartFun()
    {
        SceneManager.LoadScene(1);
    }

    void GameOverFun()
    {
        Application.Quit();
    }


}
