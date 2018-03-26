using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HyperDrive : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
		this.ShipBase = this.GetComponent<ShipBase>();
	}

	private ShipBase ShipBase { get; set; }

	// Update is called once per frame
	void Update ()
	{
		var hyperDrive = Input.GetButton("Fire2");
		var hyperDriveRelease = Input.GetButtonUp("Fire2");
		if (hyperDrive)
		{
			this.ShipBase.IgnoreTopSpeed = true;
			this.ShipBase.Velocity += Vector3.ClampMagnitude(this.ShipBase.Velocity, this.ShipBase.Speed * 5 * Time.deltaTime);
		}
		
		if(hyperDriveRelease)
		{
			this.ShipBase.IgnoreTopSpeed = false;
			this.ShipBase.Velocity = Vector3.ClampMagnitude(this.ShipBase.Velocity, 0.01f);
		}
	}
}
