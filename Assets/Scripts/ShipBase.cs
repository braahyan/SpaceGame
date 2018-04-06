using System;
using UnityEngine;

public abstract class ShipBase : MonoBehaviour
{
    public float Acceleration = 10;

    protected virtual void Update()
    {
        Move(0, 0);
    }

    protected void Move(float x, float y)
    {
        var rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.AddForce(new Vector3(x, y) * Time.deltaTime * Acceleration);

        Vector2 direction = rigidBody.velocity.normalized;
        
        // this code converts axis input into degrees for rotation.
        // i don't understand the math enough to know why the rotation
        // requires the magic 90f subtraction
        // this guy explains some
        // https://answers.unity.com/questions/654222/make-sprite-look-at-vector2-in-unity-2d-1.html
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        rigidBody.MoveRotation(angle);
    }
}