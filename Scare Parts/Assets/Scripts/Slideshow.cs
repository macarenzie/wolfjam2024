using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Slideshow : MonoBehaviour
{
    // === Fields ===

    [SerializeField] Texture[] imageArray;
    private int currentImage;

    private UIDocument document;
    private Button button;
    private VisualElement container;


    // === Methods ===

    /// <summary>
    /// setting up the button to switch the image
    /// </summary>
    private void Awake()
    {
        document = GetComponent<UIDocument>();

        button = document.rootVisualElement.Q("NextButton") as Button;
        button.RegisterCallback<ClickEvent>(NextImage);

        container = document.rootVisualElement.Q("Container") as VisualElement;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentImage = 0;
        container.style.backgroundImage = (StyleBackground)imageArray[currentImage];
    }

    /// <summary>
    /// move on to the next image in the sequence
    /// </summary>
    private void NextImage(ClickEvent evt)
    {
        if (currentImage < imageArray.Length - 1)
        {
            currentImage++;
            container.style.backgroundImage = (StyleBackground)imageArray[currentImage];
        }
        else
        {
            //transition to next scene
            SceneManager.LoadScene("GameScene");
        }
    }

    private void OnDisable()
    {
        button.UnregisterCallback<ClickEvent>(NextImage);
    }
}
