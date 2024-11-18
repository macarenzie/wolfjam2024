using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Slideshow : MonoBehaviour
{
    [SerializeField] Texture[] imageArray;
    private int currentImage;

    private UIDocument document;
    private Button button;

    /// <summary>
    /// setting up the button to switch the image
    /// </summary>
    private void Awake()
    {
        document = GetComponent<UIDocument>();

        button = document.rootVisualElement.Q("NextButton") as Button;
        button.RegisterCallback<ClickEvent>(NextImage);
    }

    // Start is called before the first frame update
    void Start()
    {
        currentImage = 0;
    }

    /// <summary>
    /// putting the image on the screen
    /// </summary>
    void OnGUI()
    {
        int w = Screen.width, h = Screen.height;

        Rect imageRect = new Rect(0, 0, w, h);

        GUI.DrawTexture(imageRect, imageArray[currentImage]);
    }

    /// <summary>
    /// move on to the next image in the sequence
    /// </summary>
    private void NextImage(ClickEvent evt)
    {
        if (currentImage < imageArray.Length - 1)
        {
            currentImage++;
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
