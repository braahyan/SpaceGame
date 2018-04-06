using UnityEngine;

public class SmoothFollow : MonoBehaviour
{
    public float dampTime = 0.15f;
    public string TagTarget = "Player";
    private GameObject target;
    private Vector3 velocity = Vector3.zero;

    public Camera MyCamera { get; set; }

    private void Start()
    {
        MyCamera = GetComponent<Camera>();
        target = GameObject.FindWithTag(TagTarget);
    }

    // Update is called once per frame
    private void Update()
    {

        if (target != null)
        {
            var point = MyCamera.WorldToViewportPoint(target.transform.position);
            var delta = target.transform.position - MyCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));
            var destination = transform.position + delta;
            transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
            
            var transformPosition = transform.position;
            if (velocity.magnitude >= 5 && transform.position.z >= -40)
            {
                
                transformPosition.z -= Time.deltaTime;
                transform.position = transformPosition;
            }
            else if (velocity.magnitude < 5 && transform.position.z <= -10)
            {
                transformPosition.z += Time.deltaTime;
                transform.position = transformPosition;
            }
        }
        else
        {
            target = GameObject.FindWithTag(TagTarget);
        }
    }
}
