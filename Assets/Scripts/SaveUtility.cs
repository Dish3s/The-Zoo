using System.Collections.Generic;
using UnityEngine;
using static SaveSystem;

public class SaveUtility : MonoBehaviour
{
    [SerializeField] private SaveSystem saveSystem;
    [SerializeField] private List<Animal> animals = new List<Animal>();

    public void SaveGame()
    {
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
        List<SaveSystem.AnimalData> loadedAnimalDataList = saveSystem.GetAnimalDataList();

        for (int i = 0; i < Mathf.Min(animals.Count, loadedAnimalDataList.Count); i++)
        {
            AnimalData animalData = loadedAnimalDataList[i];

            animals[i].hungerMeter = animalData.hungerMeter;
            animals[i].thirstMeter = animalData.thirstMeter;
            animals[i].tiredMeter = animalData.tiredMeter;
        }
    }
}
