using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void OpenWindow(GameObject panel)
    {
        panel.SetActive(true);
    }

    public void ExitWindow(GameObject panel)
    {
        panel.SetActive(false);
    }
}
