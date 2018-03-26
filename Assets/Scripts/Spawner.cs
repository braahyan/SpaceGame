using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private float LastSpawn;

    private List<GameObject> mSpawned;
    public int SpawnCount = 1;

    public float SpawnTime;

    public GameObject Subject;

    // Use this for initialization
    private void Start()
    {
        mSpawned = new List<GameObject>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Time.time - LastSpawn >= SpawnTime && mSpawned.Count < SpawnCount)
        {
            LastSpawn = Time.time;
            var obj = Instantiate(Subject);
            Subject.transform.position = transform.position;
            mSpawned.Add(obj);
        }

        // this is some jacked up unity nonsense.
        // this isn't actually null, and shouldn't be represeneted like this.
        // thanks Unity!
        mSpawned = mSpawned.Where(x => x != null).ToList();
    }
}
