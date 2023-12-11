using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class SaveSystem : MonoBehaviour
{
    public static SaveSystem instance;

    [System.Serializable]
    public class AnimalData
    {
        public float hungerMeter;
        public float thirstMeter;
        public float tiredMeter;
    }

    [SerializeField] private List<AnimalData> animalDataList = new List<AnimalData>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        LoadGame();
    }

    public void SaveGame(List<AnimalData> animalData)
    {
        Debug.Log("SaveGame method called.");

        animalDataList = new List<AnimalData>(animalData);
        SaveToFile();
    }

    public List<AnimalData> GetAnimalDataList()
    {
        return animalDataList;
    }

    private void SaveToFile()
    {
        string json = JsonUtility.ToJson(animalDataList);

        if (!File.Exists(Application.persistentDataPath + "/data.save"))
        {
            File.Create(Application.persistentDataPath + "/data.save").Dispose();
        }

        File.WriteAllText(Application.persistentDataPath + "/data.save", json);
    }

    private void LoadFile()
    {
        if (File.Exists(Application.persistentDataPath + "/data.save"))
        {
            string json = File.ReadAllText(Application.persistentDataPath + "/data.save");
            animalDataList = JsonUtility.FromJson<List<AnimalData>>(json);
        }
        else
        {
            animalDataList = new List<AnimalData>();
        }
    }

    // Modified LoadGame method to be called by ZooManager
    public void LoadGame()
    {
        Debug.Log("LoadGame method called.");

        LoadFile();

        // Assuming that there is a ZooManager instance in the scene
        if (ZooManager.instance != null)
        {
            List<Animal> animals = ZooManager.instance.animals;

            // Ensure the loaded data matches the number of animals
            while (animalDataList.Count < animals.Count)
            {
                animalDataList.Add(new AnimalData()); // Add default data if needed
            }

            for (int i = 0; i < Mathf.Min(animals.Count, animalDataList.Count); i++)
            {
                AnimalData animalData = animalDataList[i];

                animals[i].hungerMeter = animalData.hungerMeter;
                animals[i].thirstMeter = animalData.thirstMeter;
                animals[i].tiredMeter = animalData.tiredMeter;
            }
        }
        else
        {
            Debug.LogError("ZooManager instance not found.");
        }
    }
}
