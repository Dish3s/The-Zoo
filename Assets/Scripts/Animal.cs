using UnityEngine;
using UnityEngine.UI;

public class Animal : MonoBehaviour
{
    // Enum defining types of animals
    protected enum AnimalType
    {
        Lion,
        Tiger,
        Pig,
        Cow,
        Giraffe
    }

    // Enum defining movement states
    protected enum MovementState
    {
        Idle,
        Moving,
        Stopping
    }

    [Header("Animal Type")]
    [SerializeField] protected AnimalType animalType;
    [SerializeField] protected FoodType diet;

    [Header("Meters")]
    [SerializeField] public float hungerMeter = 100f;
    [SerializeField] public float thirstMeter = 100f;
    [SerializeField] public float tiredMeter = 100f;

    [Header("Movement")]
    [SerializeField] private float minSpeed = 3f;
    [SerializeField] private float maxSpeed = 8f;
    private float moveSpeed;
    [SerializeField] public float minStopTime = 2f;
    [SerializeField] public float maxStopTime = 8f;

    [Header("Timer")]
    [SerializeField] private float timer = 0;

    [Header("UI Sliders")]
    public Slider hungerSlider;
    public Slider thirstSlider;
    public Slider tirednessSlider;

    protected Rigidbody2D rb2D;
    protected MovementState movementState = MovementState.Idle;
    private Vector2 randomDirection;
    private float stopTime;

    // Initialization, invoked at the start of the script
    public void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        InvokeRepeating(nameof(UpdateMeters), 1f, 1f);
        InvokeRepeating(nameof(RandomMovement), 0f, Random.Range(minStopTime, maxStopTime));

        // Set initial slider values
        UpdateUI();
    }

    public void Update()
    {
        HandleUserInput();
        UpdateUI();

        if (hungerMeter <= 0f || thirstMeter <= 0f || tiredMeter <= 0f)
        {
            Die();
        }

        if (movementState == MovementState.Moving)
        {
            Move();
            timer += Time.deltaTime;

            if (timer >= 1)
            {
                timer = 0;
            }
        }
    }

    // Handle user input for feeding and caring for the animal
    public void HandleUserInput()
    {
        if (Input.GetKeyUp(KeyCode.D) && thirstMeter <= 80)
        {
            Drink(20);
        }

        if (Input.GetKeyUp(KeyCode.S) && tiredMeter <= 50)
        {
            Sleep(50);
        }
        if (Input.GetKeyUp(KeyCode.E) && hungerMeter <= 80)
        {
            Eat(FoodType.Leaf);
        }
        if (Input.GetKeyUp(KeyCode.E) && hungerMeter <= 80)
        {
            Eat(FoodType.Meat);
        }
    }

    // Feed the animal on key press
    public void Eat(FoodType foodToEat)
    {
        if (foodToEat == diet)
        {
            hungerMeter += 20;
        }
    }

    // Give animal water on button press
    public void Drink(int drinkAmount)
    {
        thirstMeter += drinkAmount;
    }

    // Increase animal's sleep on button press
    public void Sleep(int sleepTime)
    {
        tiredMeter += sleepTime;
    }

    // Die if neglected
    public void Die()
    {
        Destroy(gameObject);
    }

    // Update method to decrement meters
    public void UpdateMeters()
    {
        hungerMeter -= 5f;
        thirstMeter -= 5f;
        tiredMeter -= 0.5f;
    }

    // Update method to update UI sliders
    public void UpdateUI()
    {
        if (hungerSlider != null)
        {
            hungerSlider.value = hungerMeter / 100f;
        }

        if (thirstSlider != null)
        {
            thirstSlider.value = thirstMeter / 100f;
        }

        if (tirednessSlider != null)
        {
            tirednessSlider.value = tiredMeter / 100f;
        }
    }

    // Method to initiate random movement
    private void RandomMovement()
    {
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

        if (movementState == MovementState.Moving)
        {
            RandomizeDirection();
        }
    }

    // Method to transition from Moving to Stopping state
    private void StopMoving()
    {
        movementState = MovementState.Stopping;
    }

    // Method to generate a random movement direction
    private void RandomizeDirection()
    {
        randomDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }

    // Method to set a random movement speed
    private void SetRandomSpeed()
    {
        float randomSpeed = Random.Range(minSpeed, maxSpeed);
        moveSpeed = randomSpeed;
    }

    // Method to set a random stop time
    private void SetRandomStopTime()
    {
        stopTime = Random.Range(minStopTime, maxStopTime);
    }

    // Method to move the animal based on the random direction and speed
    private void Move()
    {
        Vector2 newPosition = rb2D.position + randomDirection * moveSpeed * Time.deltaTime;
        rb2D.MovePosition(newPosition);
    }
}

public enum FoodType
{
    Meat,
    Leaf,
}
