using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FighterCntrl : MonoBehaviour
{
    [SerializeField] private GameDataSO gameData;
    [SerializeField] private GameObject firePoint;

    public void EngageEnemy() => engage = true;

    private Vector2 move = new Vector2();
    private Vector2 look = new Vector2();

    private void OnMove(Vector2 move) => this.move = move;
    private void OnLook(Vector2 look) => this.look = look;

    // Gun Shooting Attributes
    //========================
    private int ammoCount = 0;
    private int maxAmmoCount = 0;
    private float reloadTime = 3.0f;
    private bool isReloading = false;

    // Health Attributes
    //==================
    private int health = 100;
    private int maxHealth = 100;

    private WeaponSO ammo;
    private WeaponSO shield;
    private WeaponSO missile;

    private bool engage = false;

    private float totalShieldSec;

    private Color newColor;
    private Renderer meshRenderer;

    public void StartEngage() => engage = true;

    private TakeDamageCntrl tdc;


    // Start is called before the first frame update
    void Start()
    {
        EventManager.Instance.InvokeOnUpdateAmmoBar(1, 1);

        GetComponent<TakeDamageCntrl>().Init(100.0f);
    }

    /**
     * Update() - If the player is in engagment, coutinue reading commands
     * from the keyboard.  If there is no engagement, all keyboard input
     * is egnored.
     */
    void Update()
    {
        if (engage)
        {
            MoveFighterKeyBoard(Time.deltaTime);
        }
    }

    /**
     * SetLevel() - Sets the level of the ammo, shield and missiles.  This is
     * determined when the player selects the inventory. This initialization
     * must be done before the fighter can engage.
     */
    public void SetLevel(LevelData levelData)
    {
        this.ammo = levelData.Ammo;
        this.shield = levelData.Shield;
        this.missile = levelData.Missile;

        totalShieldSec = levelData.Shield.totalDurationSec;

        ammoCount = ammo.maxRounds;
        maxAmmoCount = ammo.maxRounds;
    }

    private void OnFire()
    {
        if (!isReloading)
        {
            StartCoroutine(FireAmmo());
        }
    }

    private void OnFireKey(int key)
    {

        switch(key)
        {
            case 1:
                if (!isReloading) StartCoroutine(FireAmmo());
                break;
            case 2:
                StartCoroutine(FireMissile());
                break;
            case 3:
                if (totalShieldSec > 0.0f) StartCoroutine(ShieldsUp());
                break;

        }
    }

    private IEnumerator ShieldsUp()
    {
        GameObject go = Instantiate(shield.shieldPrefab, transform);

        float duration = shield.durationSec;
        tdc.TurnShieldOn(shield.absorption / 100.0f);

        while (duration >= 0.0f)
        {
            duration -= Time.deltaTime;
            totalShieldSec -= Time.deltaTime;
            EventManager.Instance.InvokeOnUpdateShield(totalShieldSec, shield.totalDurationSec);
            yield return null;
        }

        tdc.TurnShieldOff();
        Destroy(go);
    }

    /**
     * ReLoad() - If the ammo is reloading stop all firing until the
     * reloading is complete.  When the reloading is finished, the ammo will
     * return to maximum.
     */
    private IEnumerator ReLoad()
    {
        isReloading = true;

        float timing = 0.0f;

        while (timing < reloadTime)
        {
            EventManager.Instance.InvokeOnUpdateReload(timing, reloadTime);

            timing += Time.deltaTime;
            yield return null;
        }

        ammoCount = maxAmmoCount;
        EventManager.Instance.InvokeOnUpdateAmmoBar(ammoCount, maxAmmoCount);

        isReloading = false;
    }

    private IEnumerator FireMissile()
    {
        GameObject go = Instantiate(missile.missilePrefab, firePoint.transform.position, transform.rotation);
        go.GetComponentInChildren<Rigidbody>().AddForce(transform.forward * missile.missileForce, ForceMode.Impulse);
        go.GetComponent<MissileCntrl>().Initialize(ammo);

        yield return new WaitForSeconds(0.1f);
    }

    /**
     * FireAmmo() - Fires one ammo round with the press of the "1" key.  If
     * the ammo has been exhausted, the process goes into reload.  The "1" is
     * is egnored during reload.
     */
    private IEnumerator FireAmmo()
    {
        GameObject missile = Instantiate(ammo.ammoPrefab, firePoint.transform.position, transform.rotation);
        missile.GetComponentInChildren<Rigidbody>().AddForce(transform.forward * ammo.force, ForceMode.Impulse);
        missile.GetComponent<AmmoCntrl>().Initialize(gameData.TAG_FIGHTER, ammo.destroyPrefab, ammo.damage, ammo.ammoSound);
        Destroy(missile, ammo.range);

        ammoCount -= 1;

        EventManager.Instance.InvokeOnUpdateAmmoBar(ammoCount, maxAmmoCount);

        if (ammoCount == 0)
        {
            StartCoroutine(ReLoad());
        }

        yield return new WaitForSeconds(0.1f);
    }

    private void MoveFighterKeyBoard(float dt)
    {
        float throttle = 1.0f;
        float speed = 50.0f;

        if (Mouse.current.leftButton.isPressed)
        {
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Vector3 target = new Vector3(hit.point.x, 0.0f, hit.point.z);
                Vector3 direction = (target - transform.position).normalized;

                Quaternion targetRotation = Quaternion.LookRotation(direction);
                Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, 25.0f * dt);
                transform.localRotation = playerRotation;
            }
        }

        transform.Translate(transform.forward * speed * throttle * dt, Space.World);
    }

    /**
     * StartTurn() - Turns the fighter, 360 degrees.  The fighter continues
     * to turn until the fighter is destroyed.  There is no function to stop
     * the fighter from turning.  This is used mainly to display the
     * fighter to the player.
     */
    public void StartTurn()
    {
        LeanTween.rotateAroundLocal(gameObject, Vector3.up, -360.0f, 10.0f).setLoopClamp();

        meshRenderer = GetComponentInChildren<Renderer>();
        newColor = meshRenderer.material.color;
        StartCoroutine(FadeObject(1.0f, 0.0f, 2.0f));
    }

    public void DestroyRequest()
    {
        Destroy(gameObject);
    }

    private IEnumerator FadeObject(float current, float required, float fadeTime)
    {
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / fadeTime)
        {
            newColor.a = Mathf.Lerp(current, required, t);

            meshRenderer.material.color = newColor;

            yield return null;
        }
    }

    private void OnEnable()
    {
        EventManager.Instance.OnInputLook += OnLook;
        EventManager.Instance.OnInputMove += OnMove;
        EventManager.Instance.OnFire += OnFire;
        EventManager.Instance.OnFireKey += OnFireKey;
        EventManager.Instance.OnDestroyAllShips += DestroyRequest;
    }

    private void OnDisable()
    {
        EventManager.Instance.OnInputLook -= OnLook;
        EventManager.Instance.OnInputMove -= OnMove;
        EventManager.Instance.OnFire -= OnFire;
        EventManager.Instance.OnFireKey -= OnFireKey;
        EventManager.Instance.OnDestroyAllShips -= DestroyRequest;
    }
}
