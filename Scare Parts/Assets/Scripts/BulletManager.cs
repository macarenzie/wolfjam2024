using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BulletManager : MonoBehaviour
{
    // fields
    [SerializeField] private GameObject player;
    public List<GameObject> bullets;
    [SerializeField] private GameObject bullet;
    public float speed = 6.0f;
    private InputActionReference fireAction;
    [SerializeField] private Camera cam;
    private float camWidth;


    // Start is called before the first frame update
    void Start()
    {
        bullets = new List<GameObject>();
        camWidth = cam.orthographicSize * cam.aspect;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            bullets.Add(Instantiate(bullet, player.GetComponent<SpriteRenderer>().bounds.center, Quaternion.identity));
        }
    }
}
