using UnityEngine;

public class Seeker : ShipBase
{
    private GameObject Target;
    public string TargetTag = "Player";
    public string ReturnTag = null;
    public float LookInterval = .5f;
    public float SeekDistance = 20f;
    private float mLastLook;

    // Update is called once per frame
    protected override void Update()
    {
        var delta = Time.time - mLastLook;
        if (Target != null && delta <= LookInterval)
        {
            var difference = (Target.transform.position - transform.position).normalized;
            Move(difference.x, difference.y);
        }
        else if (string.IsNullOrWhiteSpace(ReturnTag) || delta >= LookInterval)
        {
            Move(Velocity.x, Velocity.y);
            mLastLook = Time.time;
            var target = GameObject.FindWithTag(TargetTag);
            if (SeekDistance <= 0 || (target.transform.position - this.transform.position).magnitude <= SeekDistance)
            {
                Target = target;
            }
        }
        else
        {
            Move(Velocity.x, Velocity.y);
            mLastLook = Time.time;
            Target = GameObject.FindWithTag(ReturnTag);
        }
    }
}
