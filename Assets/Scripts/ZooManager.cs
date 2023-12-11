using System.Collections.Generic;
using UnityEngine;

public class ZooManager : MonoBehaviour
{
    public static ZooManager instance;

    [SerializeField] public List<Animal> animals = new List<Animal>();

    [SerializeField] private SaveSystem saveSystem;

    public void SaveGame()
    {
        Debug.Log("SaveGame method called.");

        List<SaveSystem.AnimalData> animalDataList = new List<SaveSystem.AnimalData>();

        foreach (Animal animal in animals)
        {
            SaveSystem.AnimalData animalData = new SaveSystem.AnimalData
            {
                hungerMeter = animal.hungerMeter,
                thirstMeter = animal.thirstMeter,
                tiredMeter = animal.tiredMeter
            };

            animalDataList.Add(animalData);
        }

        saveSystem.SaveGame(animalDataList);
    }

    public void LoadGame()
    {
        Debug.Log("LoadGame method called.");

        saveSystem.LoadGame();
    }

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
}
