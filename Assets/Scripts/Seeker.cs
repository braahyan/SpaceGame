using System.Runtime.InteropServices;
using UnityEngine;

public class Seeker : ShipBase
{
    private GameObject Target;
    public string TargetTag = "Player";
    public string ReturnTag;
    public float SeekDistance = 20f;

    // Update is called once per frame
    protected override void Update()
    {
        if (Target != null)
        {
            var difference = (Target.transform.position - transform.position).normalized;
            Move(difference.x, difference.y);
        }
        else
        {
            Move(0, 0);
            var target = GameObject.FindWithTag(TargetTag);
            if (target != null && (SeekDistance <= 0 ||
                                   (transform.position - target.transform.position).magnitude <= SeekDistance))
            {
                Target = target;
            }

            if (Target == null && !string.IsNullOrEmpty(ReturnTag))
            {
                Target = GameObject.FindWithTag(ReturnTag);
            }
        }
    }
}