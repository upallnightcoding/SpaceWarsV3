using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidCntrl : MonoBehaviour
{
    [SerializeField] private float viewRadius = 1.0f;
    [SerializeField] private LayerMask boidMask;
    [SerializeField] private float speed = 10.0f;
    [SerializeField] private float throttle = 1.0f;
    [SerializeField] private float viewAngle = 10.0f;

    private Collider[] colliders = null;

    private Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        direction = new Vector3(Random.Range(-1.0f, 1.0f), 0.0f, Random.Range(-1.0f, 1.0f));
    }

    public void Set(float viewRadius) => this.viewRadius = viewRadius;

    // Update is called once per frame
    void Update()
    {
        //colliders = Physics.OverlapSphere(transform.position, viewRadius, boidMask);
        calculateInView();

        direction += SteerSeparation();

        direction = direction.normalized;

        transform.Translate(direction * speed * throttle * Time.deltaTime, Space.World);

        Quaternion targetRotation = Quaternion.LookRotation(direction);

        Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, 5.0f * Time.deltaTime);
        transform.localRotation = playerRotation;

        //transform.localRotation = targetRotation;
    }

    private void calculateInView()
    {
        Collider[] boidList = Physics.OverlapSphere(transform.position, viewRadius, boidMask);
        colliders = new Collider[0];

        List<Collider> boid = new List<Collider>();

        if ((boidList != null) && (boidList.Length > 0))
        {
            foreach (Collider element in boidList)
            {
                float angle = Vector3.Angle(transform.forward, element.transform.position - transform.position);

                if (Mathf.Abs(angle) < viewAngle) boid.Add(element);
            }

            colliders = boid.ToArray();
        }
    }

    private Vector3 SteerSeparation()
    {
        Vector3 direction = Vector3.zero;

        if ((colliders != null) && (colliders.Length > 0))
        {
            foreach (Collider boid in colliders)
            {
                float ratio = Mathf.Clamp01((boid.transform.position - transform.position).magnitude / viewRadius);
                direction -= ratio * (boid.transform.position - transform.position);
            }
        }

        return (direction);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawLine(transform.position, transform.position + transform.forward * viewRadius);
        Gizmos.DrawWireSphere(transform.position, viewRadius);

        Gizmos.color = Color.green;
        if ((colliders != null) && (colliders.Length > 0))
        {
            foreach(Collider collider in colliders)
            {
                Gizmos.DrawLine(transform.position, collider.transform.position);
            }
        }
    }
}
