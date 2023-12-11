using UnityEngine;

public class InputManager : MonoBehaviour
{
    public MenuManager menuManager;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            menuManager.ToggleMenu();
        }
    }
}
