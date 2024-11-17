using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameScreenUIScript : MonoBehaviour
{
    // === Fields ===

    // Reference to the player manager
    [SerializeField] public PlayerManager playerManager;

    // Reference to the UIDocument
    private UIDocument document;

    // Reference to the health bar UI element
    private ProgressBar healthBar;

    // Reference to the gas bar UI element
    private ProgressBar gasBar;


    // === Methods ===

    private void Awake()
    {
        // Get the UIDocument
        document = GetComponent<UIDocument>();

        // Get the healthBar
        healthBar = document.rootVisualElement.Q("HealthBar") as ProgressBar;

        // Get the gasBar
        gasBar = document.rootVisualElement.Q("GasBar") as ProgressBar;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Save ref of the inside bar of the healthBar
        VisualElement insideHealthBar = document.rootVisualElement.Q(className: "unity-progress-bar__progress");

        // Change the inside color of the healthBar to red
        insideHealthBar.style.backgroundColor = Color.red;
    }

    private void Update()
    {
        // Update the healthBar
        healthBar.value = playerManager.Health;

        // Update the gasBar
        gasBar.value = playerManager.Gas;
    }
}
