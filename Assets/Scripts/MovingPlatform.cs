using UnityEngine;

public class MovingPlatform : MonoBehaviour
{


    public float speed;
    public Transform posA;
    public Transform posB;
    private Transform target;




    void Start()
    {
        target = posA;


    }


    void FixedUpdate()
    {

        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) < 0.1f)
        {
            target = target != posA ? posA : posB;
        }



    }


    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        other.transform.parent = transform;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        other.transform.parent = null;
    }


}
