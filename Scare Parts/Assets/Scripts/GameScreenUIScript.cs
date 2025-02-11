using UnityEngine;
using UnityEngine.UIElements;

public class GameScreenUIScript : MonoBehaviour
{
    // === FIELDS ===

    // Reference to the player manager
    [SerializeField] public PlayerManager playerManager;

    // Reference to the UIDocument
    private UIDocument document;

    // Reference to the health bar UI element
    private ProgressBar healthBar;

    // Reference to the gas bar UI element
    private ProgressBar gasBar;


    // === METHODS ===

    private void Awake()
    {
        // Initialize variables
        document = GetComponent<UIDocument>();
        healthBar = document.rootVisualElement.Q("HealthBar") as ProgressBar;
        gasBar = document.rootVisualElement.Q("GasBar") as ProgressBar;
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
        gasBar.value = playerManager.Gas;
    }
}
