using System;
using UnityEngine;

public class ship : ShipBase
{
    public GameObject Bullet;
    public float FireRate = 0.1f;
    public AudioSource LaserSound;
    public GameObject Missile;

    private float mLastShot = -10000;

    // Update is called once per frame
    protected override void Update()
    {
        var x = Input.GetAxis("Horizontal");
        var y = Input.GetAxis("Vertical");

        var x2 = Input.GetAxis("Horizontal2");
        var y2 = Input.GetAxis("Vertical2");

        var shootDrone = Input.GetButtonDown("Fire1");

        Move(x, y);
        Shoot(x2, y2);
        if (shootDrone)
            ShootMissile(Velocity);
    }

    private void ShootMissile(Vector3 velocity)
    {
        SpawnObject<Seeker>(velocity, Missile);
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

    private void SpawnBullet(Vector3 direction)
    {
        if (LaserSound != null)
            LaserSound.Play();

        var bullet = SpawnObject<Bullet>(direction, Bullet);
        bullet.GetComponent<Bullet>();
        bullet.Direction = direction;
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
        bulletComponent.Velocity = direction + Velocity;

        return bulletComponent;
    }
}
