using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

/// <summary>
/// Name: Player Manager
/// Purpose: Handles everything related to the player
/// Author(s): Leah Torregiano, McKenzie Lam
/// </summary>
public class PlayerManager : MonoBehaviour
{
    // === FIELDS ===
    [SerializeField] private float speed = 5f; // set in the inspector
    [SerializeField] private float redCooldown = 1;
    [SerializeField] private CollisionManager collison;

    private Vector3 objectPosition; // initialized in Start() via transform
    private int roadWidth = 4;
    private Vector3 direction; // direction object is facing, must be normalized
    private SpriteRenderer rend;


    


    // === PROPERTIES ===
    public float Health
    {
        get { return _health; }
        set { _health = value; }
    }
    private float _health = 100;

    public float Gas
    {
        get { return _gas; }
        set { _gas = value; }
    }
    private float _gas = 100;

    public bool IsSlipping
    {
        get { return _isSlipping; }
        set { _isSlipping = value; }
    }
    private bool _isSlipping = false;

    public Vector3 Direction
    {
        get { return direction; }
        set { direction = value.normalized; }
    }





    // === METHODS ===
    // Start is called before the first frame update
    void Start()
    {
        rend = gameObject.GetComponent<SpriteRenderer>();

        objectPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (redCooldown > 0)
        {
            redCooldown -= Time.deltaTime;
        }
        if (redCooldown < 0)
        {
            redCooldown = 0;
        }

        if (collison.isHit)
        {
            redCooldown = 1;
            rend.color = Color.red;

        }
        else if (redCooldown <= 0)
        {
            rend.color = Color.white;
        }



        // MOVEMENT
        if (IsSlipping)
        {
            return;
        }

        Vector3 velocity = direction * speed * Time.deltaTime;
        objectPosition += velocity;

        // draw the vehicle at that new position
        transform.position = objectPosition;

        // edge of road logic
        if (objectPosition.x > roadWidth)
        {
            objectPosition.x = roadWidth;
        }
        if (objectPosition.x < -roadWidth)
        {
            objectPosition.x = -roadWidth;
        }


        // RESOURCES
        // decrease gas over time
        Gas -= Time.deltaTime * 1;
        if (Gas < 0)
        {
            SceneManager.LoadScene("LoseScene");
        }
    }

    /// <summary>
    /// method that gets called to handle any player movement input
    /// </summary>
    /// <param name="context">external input manager</param>
    public void OnMove(InputAction.CallbackContext context)
    {
        // get latest value for input from Input System for direction
        Direction = context.ReadValue<Vector2>();
    }

    /// <summary>
    /// change a specific resource by a certain number
    /// </summary>
    /// <param name="isHealth">determines what resource is affected</param>
    /// <param name="num">amount to change resource by</param>
    public void ResourceChange(bool isHealth, float num)
    {
        if (isHealth)
        {
            Health += num;
            if (Health > 100)
            {
                Health = 100;
            }
        }
        else
        {
            Gas += num;
        }

        if (Gas < 0 || Health < 0)
        {
            SceneManager.LoadScene("LoseScene");
        }
    }
}
