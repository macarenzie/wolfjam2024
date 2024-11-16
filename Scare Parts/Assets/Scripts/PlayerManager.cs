using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    // fields
    public int Health = 100;
    public int Gas = 100;

    private Vector3 objectPosition;         // Initialized in Start() via transform
    [SerializeField] private float speed = 5f;      // Set in the inspector
    private new Camera camera;
    private float camHeight;
    private float camWidth;

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
        camera = GetComponent<Camera>();
        camHeight = camera.orthographicSize;
        camWidth = camHeight * camera.aspect;
    }

    // Update is called once per frame
    void Update()
    {
        // Velocity is direction * speed * deltaTime 
        // (we don’t really even need the tmp variable)
        Vector3 velocity = direction * speed * Time.deltaTime;

        // New position is current position + velocity
        objectPosition += velocity;



        // Draw the vehicle at that new position
        transform.position = objectPosition;

        // Extra logic to adjust for edge interaction
        if (objectPosition.x > camWidth)
        {
            objectPosition.x = camWidth;
        }
        if (objectPosition.x < -camWidth)
        {
            objectPosition.x = -camWidth;
        }
        if (objectPosition.y > camHeight)
        {
            objectPosition.y = camHeight;
        }
        if (objectPosition.y < -camHeight)
        {
            objectPosition.y = -camHeight;
        }
    }
}
