using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FighterCntrl : MonoBehaviour
{
    [SerializeField] private GameDataSO gameData;
    [SerializeField] private GameObject firePoint;
    [SerializeField] private int fighterId = -1;

    //public void EngageEnemy() => engage = true;

    private Transform clickingPlane;

    private Vector3 currentdirection = new Vector3();

    //private void OnMove(Vector2 move) => this.move = move;
    //private void OnLook(Vector2 look) => this.look = look;

    public int getFighterId() => fighterId;

    public void StartEngage() => engage = true;

    // Gun Shooting Attributes
    //========================
    private int ammoCount = 0;
    private int maxAmmoCount = 0;
    private float reloadTime = 3.0f;
    private bool isReloading = false;

    // Weapons
    //========
    private WeaponSO ammo;
    private WeaponSO shield;
    private WeaponSO missile;

    private bool engage = false;

    private float totalShieldSec;

    private Color newColor;
    private Renderer meshRenderer;

    private TakeDamageCntrl tdc;

    // Start is called before the first frame update
    void Start()
    {
        //EventManager.Instance.InvokeOnUpdateAmmoBar(1, 1);
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
            //MoveFighterKeyBoard(Time.deltaTime);
            ConstMoveFighterController();
        }
    }

    /**
     * SetLevel() - Sets the level of the ammo, shield and missiles.  This is
     * determined when the player selects the inventory. This initialization
     * must be done before the fighter can engage.
     */
    public void SetLevel(LevelData levelData, Transform clickingPlane)
    {
        this.clickingPlane = clickingPlane;

        ammo = levelData.Ammo;
        shield = levelData.Shield;
        missile = levelData.Missile;

        totalShieldSec = levelData.Shield.totalDurationSec;

        ammoCount = ammo.maxRounds;
        maxAmmoCount = ammo.maxRounds;

        float initialHealth = levelData.IsBerserk() ? 1000.0f : 100.0f;
        GetComponent<TakeDamageCntrl>().Init(initialHealth);

        tdc = GetComponent<TakeDamageCntrl>();
    }

    /**
     * OnFireKey() - 
     */
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

    /**
     * ShieldsUp() - Turn on the filter shield for the time length of the
     * shield duration.  At the end of the shield duration, turn the shield
     * off and destroy the shield.
     */
    private IEnumerator ShieldsUp()
    {
        GameObject go = Instantiate(shield.shieldPrefab, transform);

        float duration = shield.durationSec;
        tdc.TurnShieldOn(shield.absorption);

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

    /**
     * FireMissile() - Fires a missile round with the press of the "2" key.  This
     * countinues until all missiles have been fired.  There is no reload time
     * for missiles. 
     */
    private IEnumerator FireMissile()
    {
        GameObject go = Instantiate(missile.missilePrefab, firePoint.transform.position, transform.rotation);
        go.GetComponentInChildren<Rigidbody>().AddForce(transform.forward * missile.missileForce, ForceMode.Impulse);
        go.GetComponent<MissileCntrl>().Initialize(missile, ammo, firePoint);

        yield return new WaitForSeconds(0.1f);
    }

    /**
     * FireAmmo() - Fires one ammo round with the press of the "1" key.  If
     * the ammo has been exhausted, the process goes into reload.  The "1" is
     * is egnored during reload.
     */
    private IEnumerator FireAmmo()
    {
        GameObject go = Instantiate(ammo.ammoPrefab, firePoint.transform.position, transform.rotation);
        go.GetComponentInChildren<Rigidbody>().AddForce(transform.forward * ammo.force, ForceMode.Impulse);
        go.GetComponent<AmmoCntrl>().Initialize(gameData.TAG_FIGHTER, ammo.destroyPrefab, ammo.damage, ammo.ammoSound);
        Destroy(go, ammo.range);

        ammoCount -= 1;

        EventManager.Instance.InvokeOnUpdateAmmoBar(ammoCount, maxAmmoCount);

        if (ammoCount == 0)
        {
            StartCoroutine(ReLoad());
        }

        yield return new WaitForSeconds(0.1f);
    }

    private void MoveFighterController(Vector2 move)
    {
        float speed = 50.0f;
        float throttle = 1.0f;

        Debug.Log($"MoveFighterController => {move}");

        if (engage)
        {
            if (move.magnitude > 0.1f)
            {
                Vector3 direction = new Vector3(move.x, 0.0f, move.y).normalized;
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, 25.0f * Time.deltaTime);
                transform.localRotation = playerRotation;
                currentdirection = direction;
            }
        }

    }

    private void ConstMoveFighterController()
    {
        float speed = 50.0f;
        float throttle = 1.0f;

        transform.Translate(transform.forward * speed * throttle * Time.deltaTime, Space.World);
        clickingPlane.position = transform.position;
    }

    /**
     * MoveFighterKeyBoard() - 
     */
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

        transform.Translate(transform.forward * speed * throttle * Time.deltaTime, Space.World);
        clickingPlane.position = transform.position;
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
        //EventManager.Instance.OnInputLook += OnLook;
        //EventManager.Instance.OnInputMove += OnMove;
        EventManager.Instance.OnFireKey += OnFireKey;
        EventManager.Instance.OnDestroyAllShips += DestroyRequest;
        EventManager.Instance.OnInputMove += MoveFighterController;
    }

    private void OnDisable()
    {
        //EventManager.Instance.OnInputLook -= OnLook;
        //EventManager.Instance.OnInputMove -= OnMove;
        EventManager.Instance.OnFireKey -= OnFireKey;
        EventManager.Instance.OnDestroyAllShips -= DestroyRequest;
        EventManager.Instance.OnInputMove -= MoveFighterController;
    }
}
