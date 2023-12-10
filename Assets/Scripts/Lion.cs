using UnityEngine;

public class Lion : Animal
{
    protected void Start()
    {
        base.Start();
        animalType = AnimalType.Lion;
        favoriteFood = FoodType.Meat;
    }

    protected void Update()
    {
        if (Input.GetKeyUp(KeyCode.M))
        {
            Eat(FoodType.Meat);
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

