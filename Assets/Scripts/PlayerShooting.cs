using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShooting : MonoBehaviour
{

    int AmmoType = 0;

    public GameObject AmmoTypeDisplay;
    public GameObject Projectile;
    public GameObject Camera;
    public GameObject Gun;
    public GameObject Graphics;
    public GameObject Border;
    public float DisplayLerpSpeed;
    public float StartingOffest;
    public float ColorLerp;
    public Color[] ammotypeColors;
    AudioSource GunSound;
    public Animator anim;
    public float shootCooldown;
    float cooldownTimer = 0;
    Color currentAmmoColor;
    bool MouseDown = true;
    bool fired = true;

    GameController game;

    // Start is called before the first frame update
    void Start()
    {
        GunSound = GetComponent<AudioSource>();
        game = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        //If game isn't over
        if (!game.gameOver)
        {
            HandleAmmoTypeDisplay();
            HandleShooting();
        }
        //If game is over
        else
        {

        }
        
        
    }

    void HandleAmmoTypeDisplay()
    {
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {

            if (Input.GetAxis("Mouse ScrollWheel") > 0.0f)
            {
                AmmoType += 1;
            }
            if (Input.GetAxis("Mouse ScrollWheel") < 0.0f)
            {
                AmmoType -= 1;
            }
            if (AmmoType >= 4) AmmoType -= 4;
            if (AmmoType <= -1) AmmoType += 4;

        }
        currentAmmoColor = Color.Lerp(currentAmmoColor, ammotypeColors[AmmoType], Time.deltaTime * ColorLerp);
        Border.GetComponent<Image>().color = currentAmmoColor;
        AmmoTypeDisplay.transform.localEulerAngles = new Vector3(0, 0, Mathf.LerpAngle(AmmoTypeDisplay.transform.localEulerAngles.z, 90.0f * AmmoType, Time.deltaTime * DisplayLerpSpeed));
    }

    void HandleShooting()
    {
        Vector3 MousePosition = Input.mousePosition;
        MousePosition.z = 0.0f;
        Vector3 WorldMousePosition = Camera.GetComponent<Camera>().ScreenToWorldPoint(MousePosition);
        Vector3 ProjectileDirection = new Vector3(WorldMousePosition.x - transform.position.x, WorldMousePosition.y - transform.position.y, 0).normalized;
        Gun.transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2((WorldMousePosition.y - Gun.transform.position.y) * Graphics.transform.localScale.x, (WorldMousePosition.x - Gun.transform.position.x) * Graphics.transform.localScale.x) * Mathf.Rad2Deg);
        Graphics.transform.localScale = new Vector3(Mathf.Sign(ProjectileDirection.x)*1.0f,Graphics.transform.localScale.y,Graphics.transform.localScale.z);
        anim.SetBool("Shoot", false);
        cooldownTimer += Time.deltaTime;
        

        if (MouseDown)
        {
            //anim.SetBool("Hold", true);
            fired = false;
        }
        else if (!MouseDown && !fired)
        {
            fired = true;
            cooldownTimer = 0;
            anim.SetTrigger("Shooting");
            /*
            anim.SetBool("Hold", false);
            anim.SetBool("Shoot", true);
            */
            GameObject NewProjectile = Instantiate(Projectile, transform.position + ProjectileDirection * StartingOffest, Quaternion.identity);
            NewProjectile.GetComponent<ProjectileMover>().MovementDirection = ProjectileDirection;
            NewProjectile.GetComponent<ProjectileMover>().AmmoType = AmmoType;
            // Debug.Log(AmmoType);
            GunSound.pitch = Random.Range(0.9f, 1.1f);
            GunSound.Play();

            Camera.GetComponent<CameraShaker>().ShakeAmplitude += Camera.GetComponent<CameraShaker>().ShotShake;
            Camera.GetComponent<CameraShaker>().JerkOffest += ProjectileDirection * Camera.GetComponent<CameraShaker>().ShotJerk;
            GetComponent<PlayerMovement>().push(ProjectileDirection * -100);
        }
        if (Input.GetMouseButton(0))
        {
            MouseDown = true;
        }
        if (!Input.GetMouseButton(0) && cooldownTimer >= shootCooldown)
        {
            MouseDown = false;
        }
    }
}
