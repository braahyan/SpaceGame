using UnityEngine;

public class Bullet : ShipBase
{
    protected override void Update()
    {
        Move(Direction.x, Direction.y);
    }

    public Vector3 Direction { get; set; }
}
