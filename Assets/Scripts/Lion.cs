using UnityEngine;

public class Lion : Animal
{
    protected void Start()
    {
        base.Start();
        animalType = AnimalType.Lion;
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

