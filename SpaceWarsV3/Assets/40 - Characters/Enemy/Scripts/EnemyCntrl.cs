using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCntrl : MonoBehaviour
{
    [SerializeField] private WeaponSO ammo;
    [SerializeField] private GameObject firePoint;

    // Get the player reference from the hierarchy
    private GameObject fighter = null;

    private int ammoCount = 0;

    private float speed = 0.0f;
    private float minDistance = 50.0f;
    private EnemyState currentState = EnemyState.IDLE;
    private Vector3 avoidDirection;
    private float disengageTimer;
    private EnemyState oldState = EnemyState.COMBAT;

    // Start is called before the first frame update
    void Start()
    {
        speed = 25.0f;

        ammoCount = ammo.maxRounds;
    }

    public void Set(GameObject fighter)
    {
        this.fighter = fighter;
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
     * State_Idle() - In this state the enemy will remain in an idle state
     * until a fighter has been introduced.  At this point, the enemy will
     * move into the combat state.
     */
    private EnemyState State_Idle()
    {
        EnemyState state = EnemyState.IDLE;

        if (fighter != null)
        {
            state = EnemyState.COMBAT;
        }

        return (state);
    }

    /**
     * State_Combat() - 
     */
    private EnemyState State_Combat()
    {
        EnemyState state = EnemyState.COMBAT;

        Vector3 playerPos = fighter.transform.position;
        Vector3 target = new Vector3(playerPos.x, 0.0f, playerPos.z);

        if (Vector3.Distance(target, transform.position) > minDistance)
        {
            Vector3 direction = (target - transform.position).normalized;

            Quaternion targetRotation = Quaternion.LookRotation(direction);
            Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, 2.0f * Time.deltaTime);

            transform.localRotation = playerRotation;
            transform.Translate(transform.forward * speed * Time.deltaTime, Space.World);
        } else
        {
            state = EnemyState.AVOID;
        }

        return (state);
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

        disengageTimer -= Time.deltaTime;

        Quaternion targetRotation = Quaternion.LookRotation(avoidDirection);
        Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, 2.0f * Time.deltaTime);

        transform.localRotation = playerRotation;
        transform.Translate(transform.forward * speed * Time.deltaTime, Space.World);

        return (state);
    }

    /**
     * DestroyRequest() - The system may ask for the enemies to be destroyed.
     * This could happen if the player decides to "Disengage" from an
     * engagement.  All of the enemies must be destroyed at the same time.
     */
    private void DestroyRequest()
    {
        Destroy(gameObject);
    }

    private IEnumerator DisengageTimer()
    {
        yield return new WaitForSeconds(2.0f);
        currentState = EnemyState.COMBAT;
    }

    private void FireMissle()
    {
        GameObject go = Instantiate(ammo.ammoPrefab, firePoint.transform.position, transform.rotation);
        go.GetComponentInChildren<Rigidbody>().AddForce(transform.forward * ammo.force, ForceMode.Impulse);
        Destroy(go, ammo.range);

        ammoCount -= 1;

        //EventManager.Instance.InvokeOnUpdateAmmoBar(ammoCount, maxAmmoCount);

        //if (ammoCount == 0)
        //{
          //  StartCoroutine(ReLoad());
        //}

        //yield return new WaitForSeconds(0.1f);
    }

    private void OnEnable()
    {
        EventManager.Instance.OnDestoryRequest += DestroyRequest;
    }

    private void OnDisable()
    {
        EventManager.Instance.OnDestoryRequest -= DestroyRequest;
    }

    private enum EnemyState
    {
        IDLE,
        COMBAT,
        AVOID,
        DISENGAGE,
        DESTROY_REQUEST
    }

}
