using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Animal;

public class Tiger : Animal
{
    protected void Start()
    {
        base.Start();
        animalType = AnimalType.Tiger;
    }

    protected void Update()
    {
        if (Input.GetKeyUp(KeyCode.E) && hungerMeter <= 80)
        {
            Eat(FoodType.Meat);
        }

        base.Update();
    }
    
}
