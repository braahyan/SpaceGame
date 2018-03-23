using UnityEngine;

public class Bullet : ShipBase
{
    private float Time { get; set; }
    private Vector3 Direction;
    public float Lifetime = 3.0f;

    public void SetDirection(Vector3 direction)
    {
        this.Direction = direction;
    }
    
    public new void Start()
    {
        this.Time = UnityEngine.Time.time;
        base.Start();
    }

    protected override bool IsHostileTo(ShipBase shipbase)
    {
        return shipbase.CompareTag("Enemy");;
    }

    public void Update()
    {
        if (UnityEngine.Time.time - Time >= Lifetime)
        {
            this.HP = 0;
        }
        this.Move(Direction.x, Direction.y);
    }
}
