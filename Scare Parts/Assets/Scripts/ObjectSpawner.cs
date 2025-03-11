using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    // === FIELDS ===
    [SerializeField] List<GameObject> objectTypes = new List<GameObject>();
    private float timer = 1.0f;
    private Vector2 location;
    private GameObject obj;


    // === PROPERTIES ===
    public List<GameObject> Objects
    {
        get { return _objects; }
    }
    private List<GameObject> _objects = new List<GameObject>();


    // === METHODS ===
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        Spawn();

        for (int i = 0; i < Objects.Count; i++)
        {
            if (Objects[i].GetComponent<SpriteRenderer>().bounds.center.y < -10)
            {
                Destroy(Objects[i]);
                Objects.RemoveAt(i);
                i--;
                continue;
            }
        }
    }

    private void Spawn()
    {
        if (timer < 0)
        {
            location = new Vector2(Random.Range(-3, 3), 15);
            Objects.Add(Instantiate(objectTypes[Random.Range(0, objectTypes.Count)], location, Quaternion.identity));
            timer = 1.0f;
        }
    }
}
