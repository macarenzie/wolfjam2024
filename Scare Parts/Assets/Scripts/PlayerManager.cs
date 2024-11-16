using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    // fields
    public int Health = 100;
    public int Gas = 100;

    private Vector3 objectPosition;         // Initialized in Start() via transform
    [SerializeField] private float speed = 5f;      // Set in the inspector
    private int roadWidth = 4;

    // Direction object is facing, must be normalized
    private Vector3 direction;
    internal Vector3 Direction
    {
        get { return direction; } // Provide it if needed
        set // Only set a normalized copy!
        {
            direction = value.normalized;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        objectPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Velocity is direction * speed * deltaTime 
        Vector3 velocity = direction * speed * Time.deltaTime;

        // New position is current position + velocity
        objectPosition += velocity;

        // Draw the vehicle at that new position
        transform.position = objectPosition;

        // TODO: edge of road logic
        if (objectPosition.x > roadWidth)
        {
            objectPosition.x = roadWidth;
        }
        if (objectPosition.x < -roadWidth)
        {
            objectPosition.x = -roadWidth;
        }
    }

    // The method that gets called to handle any player movement input
    public void OnMove(InputAction.CallbackContext context)
    {
        // Get latest value for input from Input System for direction
        Direction = context.ReadValue<Vector2>();
    }
}
