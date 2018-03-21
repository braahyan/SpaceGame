using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

	public GameObject Subject;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time - LastSpawn >= SpawnTime)
		{
			LastSpawn = Time.time;
			Instantiate(Subject);
		}
	}

	private double LastSpawn;

	public double SpawnTime;
}
