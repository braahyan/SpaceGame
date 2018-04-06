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

        Move(x, y);
    }
}
