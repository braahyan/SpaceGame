using UnityEngine;

public class HitPoints : MonoBehaviour
{
    public int CollisionDamage = 1;
    public bool DestroyOnDeath = true;
    public AudioSource ExplosionSound;
    private bool ExplosionSoundPlayed;

    public bool HasLifetime;
    public AudioSource HitSound;
    public float HP = 1;

    public bool HUD;
    public float Lifetime = 3.0f;

    public Rect Location;

    private float mLastHit;
    private float mMaxHP;
    private float mSpawnTime;

    public string Name;
    public bool Regenerate = true;
    public float RegenerateAmount = 5f;
    public float RegenerateDelay = 5f;
    protected SpriteRenderer SpriteRenderer { get; set; }
    public bool IsDead => HP <= 0;

    protected void Start()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
        DeathEvents = GetComponents<IDeath>();

        if (ExplosionSound != null)
            ExplosionSound.playOnAwake = false;

        if (HitSound != null)
            HitSound.playOnAwake = false;
        mMaxHP = HP;

        mSpawnTime = Time.time;
    }

    public IDeath[] DeathEvents { get; set; }

    protected void Update()
    {
        if (HasLifetime && Lifetime <= Time.time - mSpawnTime)
            HP = 0;

        if (IsDead)
        {
            SpriteRenderer.enabled = false;
            if (ExplosionSound != null && !ExplosionSoundPlayed)
            {
                ExplosionSound.Play();
                ExplosionSoundPlayed = true;
            }

            if (DestroyOnDeath &&
                (HitSound == null || !HitSound.isPlaying) &&
                (ExplosionSound == null || !ExplosionSound.isPlaying))
            {
                foreach (var deathEvent in DeathEvents)
                {
                    deathEvent.Die();
                }
                Destroy(transform.root.gameObject);
            }
            else
            {
                SpriteRenderer.enabled = false;
                GetComponent<Collider2D>().enabled = false;
            }
        }

        if (!IsDead)
            if (Regenerate && Time.time - mLastHit > RegenerateDelay && HP < mMaxHP)
            {
                HP += RegenerateAmount * Time.deltaTime;
                if (HP > mMaxHP)
                    HP = mMaxHP;
            }
    }

    protected virtual bool IsHostileTo(HitPoints shipbase)
    {
        return !shipbase.CompareTag(tag);
    }

    private void OnCollisionEnter2D(Collision2D coll){
    
        var hitPoints = coll.gameObject.GetComponent<HitPoints>();
        if (hitPoints != null && hitPoints.IsHostileTo(this) && (HitSound == null || !HitSound.isPlaying)
            && hitPoints.CollisionDamage > 0)
        {
            if (HitSound != null)
                HitSound.Play();
            HP = HP - hitPoints.CollisionDamage;
            mLastHit = Time.time;
        }
    }

    protected void OnGUI()
    {
        if (!HUD) return;
        GUI.Label(Location, Name + ":" + HP);
    }
}
