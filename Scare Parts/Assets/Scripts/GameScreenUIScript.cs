using UnityEngine;
using UnityEngine.UIElements;

public class GameScreenUIScript : MonoBehaviour
{
    // === FIELDS ===

    // Reference to the player manager
    [SerializeField] private PlayerManager playerManager;

    // Reference to the UIDocument
    private UIDocument document;

    // Reference to the health bar UI element
    private ProgressBar healthBar;


    // WIP moving health bar is commented out as
    // it was determined to be currently out of scope

    // Reference to the current player positon
    // private Vector3 playerPosition;

    // Scalar between UI scale and game scale
    //[SerializeField] private int positionScalar;
    //[SerializeField] private int xOffset;
    //[SerializeField] private int yOffset;


    // === METHODS ===

    private void Awake()
    {
        // Initialize variables
        document = GetComponent<UIDocument>();
        healthBar = document.rootVisualElement.Q("HealthBar") as ProgressBar;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Save reference of the inner health bar
        // This is the part of the bar that moves in response to health
        VisualElement insideHealthBar = document.rootVisualElement.Q(className: "unity-progress-bar__progress");

        // Change the color of the inner health bar
        insideHealthBar.style.backgroundColor = Color.red;
    }
    
    private void Update()
    {
        // Updates bar values
        healthBar.value = playerManager.Health;


        // TODO: Get the health bar to translate accoring to player position

        // Move the healthbar to the player's current position
        // playerPosition = playerManager.ObjectPosition;


        // Attempt moving it with the translate
        // This makes it move, but it's position is off
        // and it doesn't move the right amounts

        //healthBar.style.translate = new Translate(
        //    (playerPosition.x + xOffset) * positionScalar,
        //    (playerPosition.y + yOffset) * positionScalar);


        // Attempt to translate the coordinates from
        // the UI system to the game screen

        //Vector2 playerPositionCorrected = new Vector2(playerPosition.x, Screen.height - playerPosition.y);
        //playerPositionCorrected = RuntimePanelUtils.ScreenToPanel(document.rootVisualElement.panel, playerPositionCorrected);


        // Attempt moving it with .top and .left
        // but this also has the same problems as .translate
        // It doesn't translate to the player's position and 
        // it doesnt move the right amounts

        //healthBar.style.top = playerPositionCorrected.y;
        //healthBar.style.left = playerPositionCorrected.x;
        
    }
}
