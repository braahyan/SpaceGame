using System;
using System.Collections.Generic;
using UnityEngine;

public class ship : MonoBehaviour
{
    public float Speed = 3.0f;
    public float BulletSpeed = 5f;
    public float FireRate = 0.1f;
    public GameObject bullet;

    private float mLastShot=-10000;

    // Update is called once per frame
    void Update()
    {
        var x = Input.GetAxis("Horizontal");
        var y = Input.GetAxis("Vertical");
        
        var x2 = Input.GetAxis("Horizontal2");
        var y2 = Input.GetAxis("Vertical2");

        if (Math.Abs(x) > .0001f || Math.Abs(y) > .0001f)
        {
            // this code converts axis input into degrees for rotation.
            // i don't understand the math enough to know why the rotation
            // requires the magic 90f subtraction
            var angle = Mathf.Atan2(y, x) * Mathf.Rad2Deg - 90f;
            // Do something with the angle here.
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            var direction = new Vector3(x, y);
            // here we multiply our input vector by our speed value.
            transform.position += direction * Speed * Time.deltaTime;
        }
        
        if ((Math.Abs(x2) > .0001f || Math.Abs(y2) > .0001f) && Time.time - mLastShot >= FireRate)
        {
            mLastShot = Time.time;
            var direction = new Vector3(x2, y2);
            AddSprite(direction.normalized);
            
        }
    }

    private GameObject AddSprite(Vector3 direction)
    {
        // create gameobject
        var newSprite = Instantiate(bullet);
        
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        newSprite.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);;
        newSprite.transform.position = transform.position;
        newSprite.transform.localScale = new Vector3(.5f, .5f);
        var bulletComponent = newSprite.GetComponent<Bullet>();
        bulletComponent.BulletSpeed = this.BulletSpeed;
        bulletComponent.Direction = direction;

        return newSprite;
    }
}
