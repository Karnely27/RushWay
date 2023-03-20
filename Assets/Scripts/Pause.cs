using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    [SerializeField] private GameObject _panel;
    
    public void PauseOn()
    {
        _panel.SetActive(true);
        Time.timeScale = 0;
    }

    public void Resume()
    {
        _panel.SetActive(false);
        Time.timeScale = 1;
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1f;
    }
}
