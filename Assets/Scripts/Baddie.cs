using UnityEngine;

public class Baddie : ShipBase {

	// Use this for initialization
	protected new void Start () {
		Player = GameObject.FindWithTag("Player");
		base.Start();
	}

	protected override bool IsHostileTo(ShipBase shipbase)
	{
		return !shipbase.CompareTag("Enemy");
	}

	private GameObject Player;
	
	
	// Update is called once per frame
	public void Update ()
	{
		Vector3 difference = (Player.transform.position - transform.position).normalized;

		this.Move(difference.x, difference.y);
	}
}
