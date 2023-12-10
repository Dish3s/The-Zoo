using UnityEngine;
using static Animal;

public class Animal : MonoBehaviour
{
    public enum AnimalType
    {
        Lion,
        Tiger,
        Pig,
        Cow,
        Giraffe
    }

    public enum MovementState
    {
        Idle,
        Moving,
        Stopping
    }

    public enum FoodType
    {
        Meat,
        Vegetables
    }

    [Header("Animal Type")]
    [SerializeField] protected AnimalType animalType;
    [SerializeField] protected FoodType favoriteFood;
    [SerializeField] protected FoodType animalDiet;
    [SerializeField] protected float hunger = 100f;
    [SerializeField] protected float thirst = 100f;
    [SerializeField] protected float sleepiness = 100f;


    [Header("Movement")]
    [SerializeField] protected float minSpeed = 3f;
    [SerializeField] protected float maxSpeed = 8f;
    protected float moveSpeed;
    [SerializeField] protected float minStopTime = 2f;
    [SerializeField] protected float maxStopTime = 8f;

    protected int currentHunger;
    protected int currentThirst;
    protected int currentFatigue;


    protected Vector2 randomDirection;
    protected MovementState movementState = MovementState.Idle;
    protected float stopTime;

    protected Rigidbody2D rb2D;


    protected void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    
        InvokeRepeating(nameof(GetHungry), 1f, 1f);
        InvokeRepeating(nameof(GetThirsty), 1f, 1f);
        InvokeRepeating(nameof(RandomMovement), 0f, Random.Range(minStopTime, maxStopTime));
    }

// Update method called every frame
    protected void Update()
    {

        if (Input.GetKeyUp(KeyCode.D))
        {
            Drink();
        }

        if (Input.GetKeyUp(KeyCode.S))
        {
            MakeSleep();
        }

        if (movementState == MovementState.Moving)
        {
            Move();
        }
    }

// Update method to decrement meters
    protected void GetHungry()
    {
        for (int i = 0; i < hunger; i++)
        {
            hunger -= 0.1f;
        }
    }
    protected void GetThirsty()
    {
        for (int i = 0; i < thirst; i++)
        {
            thirst -= 0.1f;
        }
    }

    public void Eat(FoodType foodToEat)
    {
        if (foodToEat == animalDiet && currentHunger <= 80)
        {
            currentHunger += 20;
        }
    }
    public void Drink()
    {
        if (currentThirst <= 70)
        {
            currentThirst += 30;
        }
    }

    public void MakeSleep()
    {
        if (currentFatigue <= 50)
        {
            currentFatigue += 50;
        }
    }

    // Method to initiate random movement
    protected void RandomMovement()
    {
    // Switch between Idle and Stopping states and invoke corresponding methods
        switch (movementState)
        {
            case MovementState.Idle:
                movementState = MovementState.Moving;
                SetRandomSpeed();
                SetRandomStopTime();
                Invoke(nameof(StopMoving), stopTime);
                break;
            case MovementState.Stopping:
                movementState = MovementState.Idle;
                InvokeRepeating(nameof(RandomMovement), 0f, Random.Range(minStopTime, maxStopTime));
                break;
        }

    // If in the Moving state, randomize movement direction
        if (movementState == MovementState.Moving)
        {
            RandomizeDirection();
        }
    }

// Method to transition from Moving to Stopping state
    protected void StopMoving()
    {
        movementState = MovementState.Stopping;
    }

// Method to generate a random movement direction
    protected void RandomizeDirection()
    {
        randomDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }

// Method to set a random movement speed
    protected void SetRandomSpeed()
    {
        float randomSpeed = Random.Range(minSpeed, maxSpeed);
        moveSpeed = randomSpeed;
    }

// Method to set a random stop time
    protected void SetRandomStopTime()
    {
        stopTime = Random.Range(minStopTime, maxStopTime);
    }

// Method to move the animal based on the random direction and speed
    protected void Move()
    {
        Vector2 newPosition = rb2D.position + randomDirection * moveSpeed * Time.deltaTime;
        rb2D.MovePosition(newPosition);
    }
}
