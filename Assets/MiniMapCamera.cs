using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapCamera : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
		this.Camera = this.GetComponent<Camera>();
		this.Camera.cullingMask &= ~(1 << LayerMask.NameToLayer("Default"));
	}

	public Camera Camera { get; set; }

	// Update is called once per frame
	void Update () {
		
	}
}
