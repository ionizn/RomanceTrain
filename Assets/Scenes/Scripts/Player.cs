using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform GameOverPanel;
    private Animator m_Animator;
    public Vector2 Speed = new Vector2(30, 30);
    public Vector2 MoveMent;
    private Rigidbody2D myRigid;

    public GameObject LazerPrefab;
    private GameObject Laser;

    public GameObject BombPrefab;

    private AudioSource audio;
    public AudioClip sound;
    public Weapon turret_z;
    public Weapon turret_x;
    public Weapon turret_c;
    public Weapon turret_v;

    void OnDestroy()
    {
        if(GameOverPanel != null)
        {
            GameOverPanel.GetComponent<CanvasGroup>().alpha = 1;
            GameOverPanel.GetComponent<CanvasGroup>().interactable = true;
            GameObject g = GameObject.Find("UI");
            g.gameObject.GetComponent<CanvasGroup>().alpha = 0;
                
        }
    }



    // Start is called before the first frame update
    void Start()
    {

        myRigid = GetComponent<Rigidbody2D>();
        m_Animator = gameObject.GetComponent<Animator>();
        audio = gameObject.AddComponent<AudioSource>();
        audio.clip = sound;
        audio.loop = false;
    }

    ~Player()
    {
        OnDestroy();
    }

    void Update()
    {

        //bool shoot = Input.GetButton("Fire1");

        //if (shoot)
        //{
        //    //Weapon tempWeapon = GetComponent<Weapon>();
        //    if (turret_1 != null)
        //    {
        //        turret_1.Attack(false);
        //    }
        //}

        if (Input.GetKey(KeyCode.Z))
        {
            //Weapon tempWeapon = GetComponent<Weapon>();
            if (turret_z != null)
            {
                turret_z.Shotgun();
            }

        }
        if (Input.GetKey(KeyCode.X))
        {
            if (turret_x != null)
            {
                //laser cooltime
                if( turret_x.Laser() )
                {
                    Vector3 vec = new Vector3(-5f, -0.5f, 0f);
                    //turret_x.Attack(false);
                    Laser = Instantiate(LazerPrefab);
                    Laser.transform.parent = turret_x.transform.parent;

                    Laser.transform.rotation = turret_x.transform.rotation;
                    Laser.transform.position = turret_x.transform.position;

                }
                //                audio.PlayOneShot(sound, 1);
            }
        }

        if (Input.GetKey(KeyCode.C))
        {
            if (turret_c != null)
            {
                turret_c.Attack(false);
            }
        }
        if (Input.GetKey(KeyCode.V))
        {
            if (turret_v != null)
            {
                turret_v.Attack(false);
            }
        }

        //if (Laser != null)
        //{
        //    Vector3 vec = new Vector3(30f, -0.5f, 0f);
        //    Laser.gameObject.transform.position = gameObject.transform.position + vec;
        //}

        //float dist = (transform.position - Camera.main.transform.position).z;

        //float leftborder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).x;
        //float rightborder = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, dist)).x;
        //float topborder = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, dist)).y;
        //float bottomborder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).y;

        //transform.position = new Vector3(
        //    Mathf.Clamp(transform.position.x, leftborder, rightborder)
        //    , Mathf.Clamp(transform.position.y, bottomborder, topborder)
        //    , transform.position.z
        //     );
    }

    private void FixedUpdate()
    {
        myRigid.velocity = MoveMent;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        bool PlayerDamage = false;

        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if(enemy != null)
        {
            Health health = enemy.gameObject.GetComponent<Health>();
            if(health != null)
            {
                health.Damage(health.hp);
                PlayerDamage = true;
            }
        }

        if(PlayerDamage)
        {
            Health health = gameObject.GetComponent<Health>();
            if(health != null)
                health.Damage(25);
        }

    }
}
