using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretGun : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
		mCollider = GetComponent<Collider2D>();
	}
	
	// Update is called once per frame
	void Update () {
		var x2 = Input.GetAxis("Horizontal2");
		var y2 = Input.GetAxis("Vertical2");
		Shoot(x2, y2);
		
	}
	public GameObject Bullet;
	public float FireRate = 0.1f;
	public AudioSource LaserSound;
	private double mLastShot;
	private Collider2D mCollider;

	private void SpawnBullet(Vector3 direction)
	{
		if (LaserSound != null)
			LaserSound.Play();

		var bullet = SpawnObject<Bullet>(direction, Bullet);
		bullet.Direction = direction;
	}
	
	private void Shoot(float x2, float y2)
	{
		if ((Math.Abs(x2) > .0001f || Math.Abs(y2) > .0001f) && Time.time - mLastShot >= FireRate)
		{
			mLastShot = Time.time;
			var direction = new Vector3(x2, y2);
			SpawnBullet(direction.normalized);
		}
	}
	
	
	private T SpawnObject<T>(Vector3 direction, GameObject prefab) where T : ShipBase
	{
		// create gameobject
		var newSprite = Instantiate(prefab);

		var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
		newSprite.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
		newSprite.transform.position = transform.position;
		newSprite.transform.localScale = new Vector3(.5f, .5f);
		var bulletComponent = newSprite.GetComponent<T>();
		var bulletCollider = newSprite.GetComponent<Collider2D>();
		if (bulletCollider)
		{
			Physics2D.IgnoreCollision(bulletCollider, mCollider);
		}

		return bulletComponent;
	}
}
