using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class NextButton : MonoBehaviour
{
    private UIDocument document;
    private Button button;

    /// <summary>
    /// setting up the button to switch the image
    /// </summary>
    private void Awake()
    {
        document = GetComponent<UIDocument>();

        button = document.rootVisualElement.Q("NextButton") as Button;
       // button.RegisterCallback<ClickEvent>(NextImage);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDisable()
    {
       // button.UnregisterCallback<ClickEvent>(NextImage);
    }
}
