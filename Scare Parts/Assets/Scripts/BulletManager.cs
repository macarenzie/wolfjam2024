using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public enum GunType
{
    Kill,
    Capture,
}
public class BulletManager : MonoBehaviour
{
    // === FIELDS ===

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Sprite cryptidBullet;
    [SerializeField] private Sprite spiritGun;
    [SerializeField] private Sprite cryptidGun;
    [SerializeField] private Sprite spiritBullet;

    private GameObject gun;
    private float speed = 8.0f;
    private float timer = 0.0f;
    private Vector3 offset = new Vector3(0, 3.3f, 0);


    // === PROPERTIES ===

   public GunType Type
   {
       get { return _type; }
       set { _type = value; }
   }
   private GunType _type = GunType.Capture;


    public List<GameObject> bullets
    {
        get { return _bullets; }
        set { _bullets = value; }
    }
    private List<GameObject> _bullets = new List<GameObject>();


    // === METHODS ===

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        for (int i = 0; i < bullets.Count; i++)
        {
            // move the bullets
            bullets[i].transform.Translate(Vector3.up * speed * Time.deltaTime);
        
            // delete bullets if they go out of bounds
            if (bullets[i].GetComponent<SpriteRenderer>().bounds.center.y > 10)
            {
                Destroy(bullets[i]);
                bullets.RemoveAt(i);
                i--;
            }
        }
    }

    /// <summary>
    /// InputAction.CallbackContext context
    /// Fire a bullet
    /// </summary>
    public void OnFire()
    {
        // limit bullets to once every 0.3 seconds
        if (timer < 0.0f)
        {
            bullets.Add(Instantiate(bullet, player.GetComponent<SpriteRenderer>().bounds.center + offset, Quaternion.identity));
            timer = 0.3f;
        }
    }

    // <summary>
    // InputAction.CallbackContext context
    // Switch gun type
    // </summary>
    public void OnSwitch()
    {
        // switch gun types
        switch (Type)
        {
            case GunType.Kill:
                gun.GetComponent<SpriteRenderer>().sprite = spiritGun;
                bullet.GetComponent<SpriteRenderer>().sprite = spiritBullet;
                Type = GunType.Capture;
                break;
            default:
                gun.GetComponent<SpriteRenderer>().sprite = cryptidGun;
                bullet.GetComponent<SpriteRenderer>().sprite = cryptidBullet;
                Type = GunType.Kill;
                break;
        }
    }
}
