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
    private int enemyIndex;
    private bool runRadar = true;

    private Vector3 flankPosVec;

    // Start is called before the first frame update
    void Start()
    {
        ammoCount = ammo.maxRounds;

        GetComponent<TakeDamageCntrl>().Init(100.0f);

        StartCoroutine(PauseFiringDuringStartup());
    }

    /**
     * Set() - 
     */
    public void Set(GameObject fighter, int enemyIndex)
    {
        this.fighter = fighter;
        this.enemyIndex = enemyIndex;

        this.flankPosVec = transform.position - fighter.transform.position;

        destoryRequestFunc = UpdateRadar();

        StartCoroutine(destoryRequestFunc);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Vector3 flankPos = FlankPos();

        Gizmos.DrawWireSphere(flankPos, 10.0f);

        Gizmos.DrawWireSphere(fighter.transform.position, 10.0f);

        Gizmos.color = Color.white;

        Gizmos.DrawWireSphere(flankPos, combatRing);

        if (Vector3.Distance(transform.position, flankPos) > combatRing)
        {
            Gizmos.color = Color.blue;
        } else
        {
            Gizmos.color = Color.green;
        }

        Gizmos.DrawLine(FlankPos(), transform.position);
    }

    private void Update()
    {
        if (fighter != null)
        {
            switch (currentState)
            {
                case EnemyState.IDLE:
                    currentState = State_Idle();
                    break;
                case EnemyState.MOVE:
                    currentState = State_Move();
                    break;
                case EnemyState.COMBAT:
                    currentState = State_Combat();
                    break;
            }

            float d = Vector3.Distance(transform.position, FlankPos());
            Debug.Log($"State: {currentState}/{d}");
        }
    }

    /**
    * State_Idle() - In this state the enemy will remain in an idle state
    * until a fighter has been introduced.  At this point, the enemy will
    * move into the combat state.
    */
    private EnemyState State_Idle()
    {
        EnemyState state = EnemyState.IDLE;

        if (Vector3.Distance(transform.position, FlankPos()) > combatRing)
        {
            state = EnemyState.MOVE;
        } else 
        {
            state = EnemyState.COMBAT;
        }

        return (state);
    }

    private EnemyState State_Combat()
    {

        EnemyState state = EnemyState.IDLE;

        if (Vector3.Distance(transform.position, FlankPos()) > combatRing)
        {
            state = EnemyState.MOVE;
        }
        else
        {
            state = EnemyState.COMBAT;
        }

        return (state);
    }

    private EnemyState State_Move()
    {
        MoveTurn(FlankPos(), fighter.transform.position);

        EnemyState state = EnemyState.IDLE;

        if (Vector3.Distance(transform.position, FlankPos()) > combatRing)
        {
            state = EnemyState.MOVE;
        }
        else
        {
            state = EnemyState.COMBAT;
        }

        return (state);
    }

    private void MoveTurn(Vector3 movePoint, Vector3 turnPoint)
    {
        if (Vector3.Distance(movePoint, transform.position) > 1.0f)
        {
            Vector3 direction = (movePoint - transform.position).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.localRotation = targetRotation;
            transform.Translate(direction * speed * Time.deltaTime, Space.Self);
        }
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
    
    private Vector3 FlankPos()
    {
        return (flankPosVec + fighter.transform.position);
    }

    //=========================================================================

    private void xxxUpdate()
    {
        if (fighter != null)
        {
            switch (currentState)
            {
                case EnemyState.IDLE:
                    currentState = State_Idle();
                    break;
                case EnemyState.COMBAT:
                    currentState = State_Combat();
                    break;
                case EnemyState.AVOID:
                    currentState = State_Avoid();
                    break;
                case EnemyState.DISENGAGE:
                    currentState = State_Disengage();
                    break;
                case EnemyState.DESTROY_REQUEST:
                    DestroyRequest();
                    break;
            }
        }
    }


   


    /**
     * State_Combat() - 
     */
    private EnemyState xxxState_Combat()
    {
        EnemyState state = EnemyState.COMBAT;

        Vector3 playerPos = fighter.transform.position;
        Vector3 target = new Vector3(playerPos.x, 0.0f, playerPos.z);

        if (Random.Range(0, 200) == 0)
        {
            StartCoroutine(AttackRange(transform.position, transform.forward, playerPos));
        }

        Vector3 direction = (target - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        //Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, 70.0f * Time.deltaTime);
        Quaternion playerRotation = targetRotation;

        transform.localRotation = playerRotation;
        transform.Translate(transform.forward * speed * Time.deltaTime, Space.Self);

        if (Vector3.Distance(target, transform.position) < minDistance)
        {
            if (readyToFire)
            {
                //FireMissle(transform.forward);
            }

            //state = EnemyState.AVOID;
        }

        return (state);
    }

    /**
     * AttackRange() - 
     */
    private IEnumerator AttackRange(Vector3 enemyPos, Vector3 enemyForward, Vector3 fighterPos)
    {
        float minAttackRange = 90.0f;
        float maxAttackAngle = 0.5f;

        Vector3 targetVector = fighterPos - enemyPos;

        yield return null;

        if (targetVector.magnitude < minAttackRange)
        {
            float angle = Vector3.Dot(targetVector.normalized, enemyForward.normalized);

            if (Vector3.Dot(targetVector, enemyForward) > maxAttackAngle)
            {
                FireMissle(targetVector);
            }
        }
    }

    private EnemyState State_Avoid()
    {
        Vector3 playerPos = fighter.transform.position;
        Vector3 target = new Vector3(playerPos.x, 0.0f, playerPos.z);

        avoidDirection = -(transform.forward + fighter.transform.forward).normalized;

        StartCoroutine(DisengageTimer());

        return (EnemyState.DISENGAGE);
    }

    private EnemyState State_Disengage()
    {
        EnemyState state = EnemyState.DISENGAGE;

        Quaternion targetRotation = Quaternion.LookRotation(avoidDirection);
        Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, 2.0f * Time.deltaTime);

        transform.localRotation = playerRotation;
        //transform.Translate(transform.forward * speed * Time.deltaTime, Space.World);

        return (state);
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
