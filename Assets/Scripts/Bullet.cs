using System;
using UnityEngine;

public class Bullet : ShipBase
{
    protected void Start()
    {
        
        var thing = Direction.normalized * Speed;
        GetComponent<Rigidbody2D>().velocity = thing;
    }
    
    public Vector3 Direction { get; set; }
    public int Speed = 30;
}
