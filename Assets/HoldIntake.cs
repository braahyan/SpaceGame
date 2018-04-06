using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldIntake : MonoBehaviour {
	
	private void OnTriggerEnter2D(Collider2D other)
	{
		var picker = other.GetComponent<Picker>();
		if (picker != null)
		{
			picker.Pickup();
		}
	}
}