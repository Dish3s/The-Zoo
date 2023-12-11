using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject menuUI;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleMenu();
        }
    }

    public void ToggleMenu()
    {
        menuUI.SetActive(!menuUI.activeSelf);
        Time.timeScale = (menuUI.activeSelf) ? 0 : 1;
    }

    public void SaveGame()
    {
        GameData gameData = new GameData
        {
            hungerMeter = FindObjectOfType<Animal>().hungerMeter,
            thirstMeter = FindObjectOfType<Animal>().thirstMeter,
            tiredMeter = FindObjectOfType<Animal>().tiredMeter
        };

        string json = JsonUtility.ToJson(gameData);
        PlayerPrefs.SetString("GameData", json);
        PlayerPrefs.Save();

        Debug.Log("Game saved!");
    }

    public void LoadGame()
    {
        string json = PlayerPrefs.GetString("GameData", string.Empty);

        if (!string.IsNullOrEmpty(json))
        {
            GameData gameData = JsonUtility.FromJson<GameData>(json);

            FindObjectOfType<Animal>().hungerMeter = gameData.hungerMeter;
            FindObjectOfType<Animal>().thirstMeter = gameData.thirstMeter;
            FindObjectOfType<Animal>().tiredMeter = gameData.tiredMeter;
        }
    }

    public void QuitGame()
    {
        SaveGame();
        Application.Quit();
    }

    public void RestartGame()
    {
        LoadGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
