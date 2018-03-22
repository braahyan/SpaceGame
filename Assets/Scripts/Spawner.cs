using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Spawner : MonoBehaviour
{

	public GameObject Subject;

	private List<GameObject> mSpawned;
	// Use this for initialization
	void Start ()
	{
		this.mSpawned = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time - LastSpawn >= SpawnTime && mSpawned.Count < SpawnCount)
		{
			LastSpawn = Time.time;
			var obj = Instantiate(Subject);
			mSpawned.Add(obj);
		}
	
		// this is some jacked up unity nonsense.
		// this isn't actually null, and shouldn't be represeneted like this.
		// thanks Unity!
		mSpawned = mSpawned.Where(x => x != null).ToList();
	}

	private float LastSpawn;

	public float SpawnTime;
	public int SpawnCount = 1;
}
