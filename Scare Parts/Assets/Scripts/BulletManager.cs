using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum GunType
{
    Kill,
    Capture,
}
public class BulletManager : MonoBehaviour
{
    // fields
    public GunType Type = GunType.Capture;
    [SerializeField] private GameObject player;
    public List<GameObject> bullets;
    [SerializeField] private GameObject bullet;
    public float speed = 6.0f;
    private InputActionReference fireAction;


    // Start is called before the first frame update
    void Start()
    {
        bullets = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach(GameObject currentBullet in  bullets) 
        { 
            currentBullet.transform.Translate(Vector3.up * speed * Time.deltaTime);
        }
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            bullets.Add(Instantiate(bullet, player.GetComponent<SpriteRenderer>().bounds.center, Quaternion.identity));
        }
    }

    public void OnSwitch(InputAction.CallbackContext context)
    {
        // switch gun types
    }
}
