using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMovement : MonoBehaviour
{
    // fields
    public CameraMovement cameraMovement;
    List<GameObjectMovement> gameObjects = new List<GameObjectMovement>();

    void Start()
    {
        if (cameraMovement == null)
        {
            cameraMovement = Camera.main.GetComponent<CameraMovement>();
        }

        if (cameraMovement != null)
        {
            cameraMovement.onCameraTranslate += Move;
        }

        SetLayers();
    }

    
    void SetLayers()
    {
        gameObjects.Clear();
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObjectMovement obj = 
                transform.GetChild(i).GetComponent<GameObjectMovement>();

            if (obj != null)
            {
                obj.name = "Layer-" + i;
                gameObjects.Add(obj);
            }
        }
    }

    private void Move(float delta)
    {
        foreach(GameObjectMovement obj in gameObjects)
        {
            obj.Move(delta);
        }
    }
}
