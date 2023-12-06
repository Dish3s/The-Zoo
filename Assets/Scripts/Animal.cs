using UnityEngine;

public class Animal : MonoBehaviour
{
    protected enum AnimalType
    {
        Lion,
        Tiger,
        Pig,
        Cow,
        Monkey
    }

    protected enum MovementState
    {
        Idle,
        Moving,
        Stopping
    }

    [Header("Animal Type")]
    [SerializeField] protected AnimalType animalType;

    [Header("Meters")]
    [SerializeField] protected float[] meters = new float[3] { 100f, 100f, 100f };

    [Header("Movement")]
    [SerializeField] protected float minSpeed = 3f;
    [SerializeField] protected float maxSpeed = 8f;
    protected float moveSpeed;
    [SerializeField] protected float minStopTime = 2f;
    [SerializeField] protected float maxStopTime = 8f;

    protected Vector2 randomDirection;
    protected MovementState movementState = MovementState.Idle;
    protected float stopTime;


    protected Rigidbody2D rb2D;

    protected virtual void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        InvokeRepeating(nameof(UpdateMeters), 1f, 1f);
        InvokeRepeating(nameof(RandomMovement), 0f, Random.Range(minStopTime, maxStopTime));
    }

    protected virtual void Update()
    {
        if (movementState == MovementState.Moving)
        {
            Move();
        }
    }

    protected virtual void UpdateMeters()
    {
        for (int i = 0; i < meters.Length; i++)
        {
            meters[i] -= 0.1f;
        }
    }

    protected virtual void RandomMovement()
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



    protected virtual void StopMoving()
    {
        movementState = MovementState.Stopping;
    }

    protected virtual void RandomizeDirection()
    {
        randomDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }

    protected virtual void SetRandomSpeed()
    {
        float randomSpeed = Random.Range(minSpeed, maxSpeed);
        moveSpeed = randomSpeed;
    }

    protected virtual void SetRandomStopTime()
    {
        stopTime = Random.Range(minStopTime, maxStopTime);
    }

    protected virtual void Move()
    {
        Vector2 newPosition = rb2D.position + randomDirection * moveSpeed * Time.deltaTime;
        rb2D.MovePosition(newPosition);
    }


}
