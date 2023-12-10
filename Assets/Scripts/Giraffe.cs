using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Animal;

public class Giraffe : Animal
{
    protected void Start()
    {
        base.Start();
        animalType = AnimalType.Giraffe;
        favoriteFood = FoodType.Vegetables;
    }

    protected void Update()
    {
        if (Input.GetKeyUp(KeyCode.V))
        {
            Eat(FoodType.Vegetables);
        }

        GetTired();
        base.Update();
    }

    protected void GetTired()
    {
        for (int i = 0; i < sleepiness; i++)
        {
            sleepiness -= 0.5f;
        }
    }
}