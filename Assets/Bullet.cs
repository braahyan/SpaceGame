using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float BulletSpeed { get; set; }
    private float Time { get; set; }
    public Vector3 Direction;
    public int Penetration = 1;

    public void Start()
    {
        this.Time = UnityEngine.Time.time;
    }

    public void Update()
    {
        this.transform.position += Direction * BulletSpeed * UnityEngine.Time.deltaTime;
        if (UnityEngine.Time.time - Time >= 3)
        {
            Destroy(transform.root.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Enemy"))
        {
            Penetration--;
            Destroy(coll.transform.root.gameObject);
            if (Penetration == 0)
            {
                Destroy(this.transform.root.gameObject);
            }
        }
    }
}
