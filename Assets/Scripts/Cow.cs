using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Cow : Animal
{
    protected void Start()
    {
        base.Start();
        animalType = AnimalType.Cow;
    }

    protected void Update()
    {
        if (Input.GetKeyUp(KeyCode.E) && hungerMeter <= 80)
        {
            Eat(FoodType.Leaf);
        }

        base.Update();

    }

}
