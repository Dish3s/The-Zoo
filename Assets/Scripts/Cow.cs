using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Cow : Animal
{
    protected void Start()
    {
        base.Start();
        animalType = AnimalType.Cow;
        favoriteFood = FoodType.Vegetables;
    }

    protected void Update()
    {
        if (Input.GetKeyUp(KeyCode.V))
        {
            Eat(FoodType.Vegetables);
        }

        base.Update();
        GetTired();
    }
    protected void GetTired()
    {
        for (int i = 0; i < sleepiness; i++)
        {
            sleepiness -= 0.5f;
        }
    }
}
