using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Animal;

public class Giraffe : Animal
{
    void Start()
    {
        base.Start();
        animalType = AnimalType.Giraffe;
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.E) && hungerMeter <= 80)
        {
            Eat(FoodType.Leaf);
        }

        base.Update();
    }

}