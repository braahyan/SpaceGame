using UnityEngine;
using System.Collections;
 
public class SmoothFollow : MonoBehaviour {
     
	public float dampTime = 0.15f;
	private Vector3 velocity = Vector3.zero;
	public Transform target;

	void Start()
	{
		MyCamera = GetComponent<Camera>();
	}

	public Camera MyCamera { get; set; }

	// Update is called once per frame
	void Update () 
	{
		if (target != null)
		{
			Vector3 point = MyCamera.WorldToViewportPoint(target.position);
			Vector3 delta = target.position - MyCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z)); 
			Vector3 destination = transform.position + delta;
			transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
			Debug.Log(velocity.magnitude);
			if (velocity.magnitude >= 5 && transform.position.z >= -40)
			{
				var transformPosition = transform.position;
				transformPosition.z -= .1f;
				transform.position = transformPosition;
			}
			else if (velocity.magnitude < 5 && transform.position.z <= -10)
			{
				var transformPosition = transform.position;
				transformPosition.z += .1f;
				transform.position = transformPosition;
			}
		}
	}
}
