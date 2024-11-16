using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class Slideshow : MonoBehaviour
{
    [SerializeField] Texture[] imageArray;
    private int currentImage;

    private string key;
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

    // Update is called once per frame
    void Update()
    {
        //pause the cutscene if escape is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseCutScene();
        }
    }

    public void PauseCutScene()
    {
        if (EditorApplication.isPlaying)
        {
            EditorApplication.isPlaying = false;
        }
        else
        {
            EditorApplication.isPlaying = true;
        }
    }

    /// <summary>
    /// move on to the next image in the sequence
    /// </summary>
    public void NextImage()
    {
        currentImage++;
    }

    /// <summary>
    /// when the cutscene is over, transition to the game scene
    /// </summary>
    public void EndCutScene()
    {
        if (currentImage >= imageArray.Length)
        {
            //transition to next scene
            SceneManager.LoadScene("GameScene");
        }
    }
}
