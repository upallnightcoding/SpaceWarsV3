using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCntrl : MonoBehaviour
{
    [SerializeField] private WeaponSO ammo;
    [SerializeField] private GameObject firePoint;
    [SerializeField] private GameDataSO gameData;

    // Get the player reference from the hierarchy
    private GameObject fighter = null;

    private IEnumerator destoryRequestFunc;

    private int ammoCount = 0;

    // Keep the enemy from firing until game play is ready
    private bool readyToFire = false;

    private float speed = 100.0f;
    private float combatRing = 40.0f;

    private float minDistance = 150.0f;
    private EnemyState currentState = EnemyState.IDLE;
    private Vector3 avoidDirection;
    public int enemyIndex;
    private bool runRadar = true;

    private float neighborRadius = 25.0f;
    private float boidSpeed = 20.0f;
    private float viewAngle = 180.0f;
    private LayerMask boidMask;

    private Collider[] colliders = null;

    private Vector3 flankPosVec;

    private Vector3 direction = new Vector3();

    // Start is called before the first frame update
    void Start()
    {
        ammoCount = ammo.maxRounds;

        GetComponent<TakeDamageCntrl>().Init(100.0f);

        StartCoroutine(PauseFiringDuringStartup());
    }

    private void Update()
    {
        Collider[] boids = CalculateNeighbors();

        UpdateBoid(boids);
    }

    public void UpdateBoid(Collider[] boids)
    {
        Vector3 direction = Vector3.zero;

        direction += Separation(boids);
        direction += Alignment(boids);
        direction += Cohesion(boids);

        direction = direction.normalized;

        UpdatePosition(direction);
    }

    /**
     * CalculateNeighbors() - 
     */
    private Collider[] CalculateNeighbors()
    {
        Collider[] overlapBoids = Physics.OverlapSphere(transform.position, neighborRadius, boidMask);

        List<Collider> boid = new List<Collider>();

        if ((overlapBoids != null) && (overlapBoids.Length > 0))
        {
            foreach (Collider element in overlapBoids)
            {
                EnemyCntrl ec = element.gameObject.GetComponent<EnemyCntrl>();
                FighterCntrl fc = element.gameObject.GetComponent<FighterCntrl>();

                if (((ec != null) && (ec.enemyIndex != this.enemyIndex)) || (fc != null))
                {
                    float angle = Vector3.Angle(transform.forward, element.transform.position - transform.position);

                    if (Mathf.Abs(angle) < viewAngle) boid.Add(element);
                }
            }
        }

        return ((boid.Count == 0) ? new Collider[0] : boid.ToArray());
    }

    /**
     * UpdatePosition() - The rotation of the enemy will always follow the 
     * fighter.  But the directional movement of the enemy is based on the
     * position of the other enemies so that all ememies can keep a 
     * distance from one another.
     */
    private void UpdatePosition(Vector3 direction)
    {
        Vector3 follow = (fighter.transform.position - transform.position).normalized;
        UpdateRotation(follow);

        if (direction.magnitude < 0.001f)
        {
            transform.Translate(follow * (1.0f * boidSpeed) * Time.deltaTime, Space.World);
        } else
        {
            transform.Translate(direction * boidSpeed * Time.deltaTime, Space.World);
        }
    }

    /**
     * UpdateRotation() - Rotate the enemy based on the direction vector.  
     * This function should not be called if the diretion vector is zero.
     */
    private void UpdateRotation(Vector3 direction)
    {
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, 5.0f * Time.deltaTime);
        transform.localRotation = playerRotation;
    }

    /**
     * Separation() - 
     */
    private Vector3 Separation(Collider[] boids)
    {
        Vector3 separationVelocity = Vector3.zero;
        int numOfBoidsToAvoid = 0;

        foreach(Collider otherBoid in boids)
        {
            EnemyCntrl ec = otherBoid.gameObject.GetComponent<EnemyCntrl>();
            FighterCntrl fc = otherBoid.gameObject.GetComponent<FighterCntrl>();

            if (((ec != null) && (ec.enemyIndex != this.enemyIndex)) || (fc != null))
            {
                Vector3 otherBoidPosition = otherBoid.transform.position;
                Vector3 currBoidPosition = transform.position;

                float dist = Vector3.Distance(currBoidPosition, otherBoidPosition);
                if (dist < neighborRadius)
                {
                    Vector3 otherBoidToCurrBoid = currBoidPosition - otherBoidPosition;
                    Vector3 dirToTravel = otherBoidToCurrBoid.normalized;
                    Vector3 weightedVelocity = dirToTravel / dist;

                    separationVelocity += weightedVelocity;
                    numOfBoidsToAvoid++;
                }
            }
        }

        if (numOfBoidsToAvoid > 0)
        {
            separationVelocity /= (float)numOfBoidsToAvoid;
            separationVelocity *= 10.0f;
        }

        return (separationVelocity);
    }

    private Vector3 Alignment(Collider[] boids)
    {
        return (Vector3.zero);
    }

    private Vector3 Cohesion(Collider[] boids)
    {
        return (Vector3.zero);
    }

    /**
     * Set() - 
     */
    public void Set(GameObject fighter, int enemyIndex, LayerMask boidMask)
    {
        this.fighter        = fighter;
        this.enemyIndex     = enemyIndex;
        this.boidMask       = boidMask;

        this.flankPosVec = transform.position - fighter.transform.position;

        destoryRequestFunc = UpdateRadar();
        StartCoroutine(destoryRequestFunc);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawWireSphere(transform.position, neighborRadius);

        Gizmos.color = Color.white;

        if ((colliders != null) && (colliders.Length > 0))
        {
            foreach (Collider collider in colliders)
            {
                Gizmos.DrawLine(collider.transform.position, transform.position);
                Debug.Log($"OnDrawGizmos: {Vector3.Distance(collider.transform.position, transform.position)}");
            }
        }

        /*Gizmos.DrawWireSphere(fighter.transform.position, 10.0f);

        Gizmos.color = Color.white;

        Gizmos.DrawWireSphere(flankPos, combatRing);

        if (Vector3.Distance(transform.position, flankPos) > combatRing)
        {
            Gizmos.color = Color.blue;
        }
        else
        {
            Gizmos.color = Color.green;
        }

        Gizmos.DrawLine(FlankPos(), transform.position);*/
    }

   

    private IEnumerator UpdateRadar()
    {
        while (runRadar)
        {
            float xDiff = transform.position.x - fighter.transform.position.x;
            float zDiff = transform.position.z - fighter.transform.position.z;
            Vector3 normalizedPos = new Vector3(xDiff, 0.0f, zDiff);

            EventManager.Instance.InvokeOnRadarUpdate(true, enemyIndex, normalizedPos);
            yield return new WaitForSeconds(1.0f);
        }
    }
   

    /**
     * DestroyRequest() - The system may ask for the enemies to be destroyed.
     * This could happen if the player decides to "Disengage" from an
     * engagement.  All of the enemies must be destroyed at the same time.
     */
    private void DestroyRequest()
    {
        runRadar = false;
        StopCoroutine(destoryRequestFunc);
        Debug.Log($"Enemy Destroyed: {enemyIndex}");
        Destroy(gameObject);
    }

    private IEnumerator PauseFiringDuringStartup()
    {
        yield return new WaitForSeconds(1.0f);
        readyToFire = true;
    }

    private IEnumerator DisengageTimer()
    {
        yield return new WaitForSeconds(2.0f);
        currentState = EnemyState.COMBAT;
    }

    private void FireMissle(Vector3 direction)
    {
        GameObject missile = Instantiate(ammo.ammoPrefab, firePoint.transform.position, transform.rotation);
        missile.GetComponentInChildren<Rigidbody>().AddForce(direction * ammo.force, ForceMode.Impulse);
        missile.GetComponent<AmmoCntrl>().Initialize(gameData.TAG_ENEMY, ammo.destroyPrefab, ammo.damage, ammo.ammoSound, gameData.sparksPrefab);
        Destroy(missile, ammo.range);

        ammoCount -= 1;
    }

    private void OnEnable()
    {
        EventManager.Instance.OnDestroyAllShips += DestroyRequest;
    }

    private void OnDisable()
    {
        EventManager.Instance.OnDestroyAllShips -= DestroyRequest;
    }

    private void OnDestroy()
    {
        runRadar = false;
        EventManager.Instance.InvokeOnRadarUpdate(false, enemyIndex, Vector3.zero);
    }

    private enum EnemyState
    {
        IDLE,
        COMBAT,
        AVOID,
        MOVE,
        DISENGAGE,
        DESTROY_REQUEST
    }

}
