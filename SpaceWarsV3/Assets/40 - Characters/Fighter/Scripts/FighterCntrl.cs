using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterCntrl : MonoBehaviour
{
    [SerializeField] private GameObject firePoint;
    [SerializeField] private WeaponSO ammo;

    private Vector2 move = new Vector2();
    private Vector2 look = new Vector2();

    private void OnMove(Vector2 move) => this.move = move;
    private void OnLook(Vector2 look) => this.look = look;

    private bool readyToFire = true;

    // Gun Shooting Attributes
    //========================
    private int ammoCount = 0;
    private int maxAmmoCount = 0;
    private float reloadTime = 3.0f;

    //private WeaponSO ammo;

    public void SetWeapon(WeaponSO ammo) => this.ammo = ammo;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        MoveFighterKeyBoard(move, look, Time.deltaTime);
    }

    private void OnFire()
    {
        StartCoroutine(FireMissle());
    }

    private IEnumerator ReLoad()
    {
        float timing = 0.0f;

        readyToFire = false;

        while (timing < reloadTime)
        {
            //EventManager.Instance.InvokeOnReloadAmmo(timing / reloadTime);

            timing += Time.deltaTime;
            yield return null;
        }

        ammoCount = maxAmmoCount;
        readyToFire = true;
        //EventManager.Instance.InvokeOnUpdateAmmo(1.0f);
    }

    private IEnumerator FireMissle()
    {
        readyToFire = false;

        GameObject go = Instantiate(ammo.ammoPrefab, firePoint.transform.position, transform.rotation);
        go.GetComponentInChildren<Rigidbody>().AddForce(transform.forward * ammo.force, ForceMode.Impulse);
        Destroy(go, ammo.range);

        ammoCount -= 1;

        //EventManager.Instance.InvokeOnUpdateAmmo((float)ammoCount / maxAmmoCount);

        yield return new WaitForSeconds(0.1f);
        readyToFire = true;
    }

    private void MoveFighterKeyBoard(Vector2 move, Vector2 look, float dt)
    {
        float throttle = move.y;
        float speed = 50.0f;
        Vector3 direction = Vector3.zero;

        Debug.Log($"Move/Look: {move}/{look}");

        if (look != Vector2.zero)
        {
            Ray ray = Camera.main.ScreenPointToRay(look);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Vector3 target = new Vector3(hit.point.x, 0.0f, hit.point.z);
                direction = (target - transform.position).normalized;

                Quaternion targetRotation = Quaternion.LookRotation(direction);
                Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, 25.0f * dt);
                transform.localRotation = playerRotation;

                look = Vector2.zero;
            }
        }

        if (throttle > 0.0f)
        {
            transform.Translate(transform.forward * speed * throttle * dt, Space.World);
        }
    }

    public void StartTurn()
    {
        LeanTween.rotateAroundLocal(gameObject, Vector3.up, -360.0f, 10.0f).setLoopClamp();
    }

    public void FadeOut()
    {
        LeanTween.alpha(gameObject, 0.0f, 2.0f);
    }

    private void OnEnable()
    {
        EventManager.Instance.OnInputLook += OnLook;
        EventManager.Instance.OnInputMove += OnMove;
        EventManager.Instance.OnFire += OnFire;
    }

    private void OnDisable()
    {
        EventManager.Instance.OnInputLook -= OnLook;
        EventManager.Instance.OnInputMove -= OnMove;
        EventManager.Instance.OnFire -= OnFire;
    }
}
