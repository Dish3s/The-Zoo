using UnityEngine;

public class Animal : MonoBehaviour
{
    // Enum defining types of animals
    protected enum AnimalType
    {
        Lion,
        Tiger,
        Pig,
        Cow,
        Monkey
    }

    // Enum defining movement states
    protected enum MovementState
    {
        Idle,
        Moving,
        Stopping
    }

    // Serialized field for the type of animal
    [Header("Animal Type")]
    [SerializeField] protected AnimalType animalType;

    // Serialized field for meters representing some aspect of the animal
    [Header("Meters")]
    [SerializeField] protected float[] meters = new float[3] { 100f, 100f, 100f };

    // Serialized fields for movement parameters
    [Header("Movement")]
    [SerializeField] protected float minSpeed = 3f;
    [SerializeField] protected float maxSpeed = 8f;
    protected float moveSpeed;
    [SerializeField] protected float minStopTime = 2f;
    [SerializeField] protected float maxStopTime = 8f;

    // Variables for random movement
    protected Vector2 randomDirection;
    protected MovementState movementState = MovementState.Idle;
    protected float stopTime;

    // Reference to the Rigidbody2D component
    protected Rigidbody2D rb2D;

    // Initialization, invoked at the start of the script
    protected virtual void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        // Repeatedly invoke methods to update meters and perform random movement
        InvokeRepeating(nameof(UpdateMeters), 1f, 1f);
        InvokeRepeating(nameof(RandomMovement), 0f, Random.Range(minStopTime, maxStopTime));
    }

    // Update method called every frame
    protected virtual void Update()
    {
        // Check if the animal is in the Moving state and call the Move method
        if (movementState == MovementState.Moving)
        {
            Move();
        }
    }

    // Update method to decrement meters
    protected virtual void UpdateMeters()
    {
        for (int i = 0; i < meters.Length; i++)
        {
            meters[i] -= 0.1f;
        }
    }

    // Method to initiate random movement
    protected virtual void RandomMovement()
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
    protected virtual void StopMoving()
    {
        movementState = MovementState.Stopping;
    }

    // Method to generate a random movement direction
    protected virtual void RandomizeDirection()
    {
        randomDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }

    // Method to set a random movement speed
    protected virtual void SetRandomSpeed()
    {
        float randomSpeed = Random.Range(minSpeed, maxSpeed);
        moveSpeed = randomSpeed;
    }

    // Method to set a random stop time
    protected virtual void SetRandomStopTime()
    {
        stopTime = Random.Range(minStopTime, maxStopTime);
    }

    // Method to move the animal based on the random direction and speed
    protected virtual void Move()
    {
        Vector2 newPosition = rb2D.position + randomDirection * moveSpeed * Time.deltaTime;
        rb2D.MovePosition(newPosition);
    }
}
