using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Pig : Animal
{
    protected void Start()
    {
        base.Start();
        animalType = AnimalType.Pig;
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

