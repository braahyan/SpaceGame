using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Baddie : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Player = GameObject.FindWithTag("Player");
		mTarget = Player.transform.position;
	}

	private GameObject Player;
	public float Speed = 4;
	public float ReactionSpeed = 1;
	private Vector3 mTarget;
	private float LastReact = 0;
	
	// Update is called once per frame
	void Update ()
	{
		Vector3 difference = (mTarget - transform.position).normalized;
		var vector = ((difference.normalized * Speed * Time.deltaTime) /5) * 4;
		transform.root.position += vector;
		
		// this is a dirty hack to emulate inertia.
		//if (Time.time - LastReact >= ReactionSpeed - Random.Range(0.0f, ReactionSpeed / 3))
		//{
			LastReact = Time.time;
			mTarget = Player.transform.position;
		//}
	}
}
