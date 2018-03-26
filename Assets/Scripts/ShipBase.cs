using System;
using UnityEngine;

public abstract class ShipBase : MonoBehaviour
{
    public float Acceleration = 3;
    public float Speed = 3.0f;
    public float TrackingSpeed;
    public Vector3 Velocity { get; set; }
    public bool IgnoreTopSpeed { get; set; }

    protected virtual void Update()
    {
        Move(0, 0);
    }

    protected void Move(float x, float y)
    {
        Velocity = CalculateVelocity(x, y, Speed, Acceleration, Velocity);

        // this code converts axis input into degrees for rotation.
        // i don't understand the math enough to know why the rotation
        // requires the magic 90f subtraction
        // this guy explains some
        // https://answers.unity.com/questions/654222/make-sprite-look-at-vector2-in-unity-2d-1.html
        var angle = Mathf.Atan2(Velocity.y, Velocity.x) * Mathf.Rad2Deg - 90f;
        transform.root.rotation = Quaternion.Lerp(transform.root.rotation,
            Quaternion.AngleAxis(angle, Vector3.forward), TrackingSpeed * Time.deltaTime);

        transform.root.position += Velocity * Time.deltaTime;
    }


    private Vector3 CalculateVelocity(float x, float y, float maxSpeed, float acceleration,
        Vector3 currentVelocity)
    {
        if (Math.Abs(x) > .0001f || Math.Abs(y) > .0001f)
        {
            var v = new Vector3(x, y) * acceleration * Time.deltaTime + currentVelocity;
            if (!IgnoreTopSpeed)
            {
                return Vector3.ClampMagnitude(v, maxSpeed);
            }
            else
            {
                return v;
            }
        }
        // we want to slow down the ship in an inertial fashion.
        return currentVelocity - currentVelocity / 2 * Time.deltaTime;
    }
}
