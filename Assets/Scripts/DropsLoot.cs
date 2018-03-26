using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropsLoot : MonoBehaviour, IDeath
{

	public GameObject DeathSpawn;

	public void Die()
	{
		var dspawn = Instantiate(DeathSpawn);
		dspawn.transform.position = this.transform.position;
	}
}
