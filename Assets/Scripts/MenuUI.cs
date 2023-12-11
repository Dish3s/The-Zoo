using UnityEngine;

public class MenuUI : MonoBehaviour
{
    public MenuManager menuManager;

    public void OnSaveButtonClick()
    {
        menuManager.SaveGame();
    }

    public void OnQuitButtonClick()
    {
        menuManager.QuitGame();
    }

    public void OnRestartButtonClick()
    {
        menuManager.LoadGame();
        menuManager.RestartGame();
    }
}
