using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyController : MonoBehaviour
{
    [SerializeField] Button Play;
    [SerializeField] Button Quite;


    private void Update()
    {
        Play.onClick.AddListener(PlayFun);
        Quite.onClick.AddListener(QuitFun);
    }

    void PlayFun()
    {
        SceneManager.LoadScene(1);
    }

    void QuitFun()
    {
        Application.Quit();
    }


}
