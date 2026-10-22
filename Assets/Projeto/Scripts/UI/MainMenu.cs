using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Play() { Time.timeScale = 1f; SceneManager.LoadScene("Gameplay"); }
    public void Quit()
    {
#if UNITY_EDITOR
        Debug.Log("Sair: Application.Quit() fecha o jogo na versão compilada.");
#else
        Application.Quit();
#endif
    }
}
