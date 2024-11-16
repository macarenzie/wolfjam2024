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
    public GameObject Gun;
    [SerializeField] private GameObject player;
    public List<GameObject> bullets;
    [SerializeField] private GameObject bullet;
    public float speed = 6.0f;
    private InputActionReference fireAction;
    [SerializeField] private Sprite spiritGun;
    [SerializeField] private Sprite cryptidGun;
    [SerializeField] private Sprite spiritBullet;
    [SerializeField] private Sprite cryptidBullet;
    [SerializeField] private CmdCooldown cooldown;


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
        if (context.performed && cooldown.CanShoot())
        {
            bullets.Add(Instantiate(bullet, player.GetComponent<SpriteRenderer>().bounds.center, Quaternion.identity));
        }
    }

    public void OnSwitch(InputAction.CallbackContext context)
    {
        if (cooldown.CanSwitch())
        {
            // switch gun types
            switch (Type)
            {
                case GunType.Kill:
                    Gun.GetComponent<SpriteRenderer>().sprite = spiritGun;
                    bullet.GetComponent<SpriteRenderer>().sprite = spiritBullet;
                    Type = GunType.Capture;
                    break;
                default:
                    Gun.GetComponent<SpriteRenderer>().sprite = cryptidGun;
                    bullet.GetComponent<SpriteRenderer>().sprite = cryptidBullet;
                    Type = GunType.Kill;
                    break;
            } 
        }
    }
}
