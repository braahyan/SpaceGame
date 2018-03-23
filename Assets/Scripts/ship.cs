using System;
using UnityEngine;

public class ship : ShipBase
{
    public float BulletSpeed = 5f;
    public float FireRate = 0.1f;
    public GameObject bullet;
    public AudioSource LaserSound;

    private float mLastShot = -10000;

    // Update is called once per frame
    void Update()
    {
        var x = Input.GetAxis("Horizontal");
        var y = Input.GetAxis("Vertical");

        var x2 = Input.GetAxis("Horizontal2");
        var y2 = Input.GetAxis("Vertical2");

        Move(x, y);
        Shoot(x2, y2);
    }

    private void Shoot(float x2, float y2)
    {
        if ((Math.Abs(x2) > .0001f || Math.Abs(y2) > .0001f) && Time.time - mLastShot >= FireRate)
        {
            mLastShot = Time.time;
            var direction = new Vector3(x2, y2);
            AddSprite(direction.normalized);
        }
    }

    private GameObject AddSprite(Vector3 direction)
    {
        if (LaserSound != null)
        {
            LaserSound.Play();
        }

        // create gameobject
        var newSprite = Instantiate(bullet);

        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        newSprite.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        newSprite.transform.position = transform.position;
        newSprite.transform.localScale = new Vector3(.5f, .5f);
        var bulletComponent = newSprite.GetComponent<Bullet>();
        bulletComponent.Speed = this.BulletSpeed;
        bulletComponent.SetDirection(direction);

        return newSprite;
    }

    protected override bool IsHostileTo(ShipBase shipbase)
    {
        return shipbase.CompareTag("Enemy");
    }
}
