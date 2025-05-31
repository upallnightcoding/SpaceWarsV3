using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidCntrl : MonoBehaviour
{
    [SerializeField] private float viewRadius = 1.0f;
    [SerializeField] private LayerMask boidMask;

    private Collider[] colliders = null;

    private Vector3 direction;

    private float boidSpeed = 0.0f;
    private float viewAngle = 0.0f;

    private GameObject hero = null;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void Set(
        float viewRadius, 
        GameObject hero, 
        float boidSpeed, 
        float viewAngle
    )
    {
        this.viewRadius = viewRadius;
        this.hero = hero;
        this.boidSpeed = boidSpeed;
        this.viewAngle = viewAngle;
    }

    // Update is called once per frame
    void Update()
    {
        CalculateNeighbors();

        Vector3 initDirection = direction;

        direction = (hero.transform.position - transform.position);
        //direction = transform.position;

        direction += SteerSeparation();

        direction = direction.normalized;

        if (Vector3.Angle(initDirection, direction) > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, 5.0f * Time.deltaTime);
            transform.localRotation = playerRotation;
        }

        transform.Translate(direction * boidSpeed * Time.deltaTime, Space.World);
        //transform.localRotation = targetRotation;
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

    private void CalculateNeighbors()
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
