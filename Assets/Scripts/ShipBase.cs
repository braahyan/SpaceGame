using System;
using UnityEngine;

public abstract class ShipBase : MonoBehaviour
{
    public float Speed = 3.0f;
    protected Vector3 Velocity = new Vector2(0, 0);
    public float TrackingSpeed;
    public float Acceleration = 3;
    public int HP = 1;
    public AudioSource ExplosionSound;
    public AudioSource HitSound;
    public int CollisionDamage = 1;

    protected void Start()
    {
        this.SpriteRenderer = GetComponent<SpriteRenderer>();
        if (this.ExplosionSound != null)
        {
            this.ExplosionSound.playOnAwake = false;
        }
        
        if (this.HitSound != null)
        {
            this.HitSound.playOnAwake = false;
        }
    }

    private SpriteRenderer SpriteRenderer { get; set; }

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

        this.IsDead = HP <= 0;
        
        if (HP <= 0 && (this.ExplosionSound != null && !this.ExplosionSoundPlayed))
        {
            this.SpriteRenderer.enabled = false;
            ExplosionSound.Play();
            this.ExplosionSoundPlayed = true;
        }
        
        if (HP <= 0 && (HitSound == null || !HitSound.isPlaying) &&
            (ExplosionSound == null || !ExplosionSound.isPlaying))
        {
            Destroy(this.transform.root.gameObject);
        }
    }

    private bool ExplosionSoundPlayed;

    protected abstract bool IsHostileTo(ShipBase shipbase);
    
    public void OnTriggerEnter2D(Collider2D coll)
    {
        var shipBase = coll.gameObject.GetComponent<ShipBase>();
        if (shipBase != null && shipBase.IsHostileTo(this) && (this.HitSound == null || !this.HitSound.isPlaying) 
            && shipBase.CollisionDamage > 0 && !shipBase.IsDead)
        {
            if (HitSound != null)
            {
                HitSound.Play();
            }
            this.HP = this.HP - shipBase.CollisionDamage;
        }

    }

    public bool IsDead { get; set; }


    private static Vector3 CalculateVelocity(float x, float y, float maxSpeed, float acceleration,
        Vector3 currentVelocity)
    {
        if (Math.Abs(x) > .0001f || Math.Abs(y) > .0001f)
        {
            var v = new Vector3(x, y) * acceleration * Time.deltaTime + currentVelocity;
            return Vector3.ClampMagnitude(v, maxSpeed);
        }
        // we want to slow down the ship in an inertial fashion.
        return currentVelocity - (currentVelocity / 2) * Time.deltaTime;
    }
}
