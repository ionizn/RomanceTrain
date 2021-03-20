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

    private new AudioSource audio;
    public AudioClip sound;

    public List<Transform> train_poss;
    List<Weapon> trains;

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
        if (trains.Count >= 1)
            if (!trains[0] && Input.GetKeyDown(KeyCode.Z))
                trains[0].Attack();
        if (trains.Count >= 2)
            if (!trains[1] && Input.GetKeyDown(KeyCode.X))
                trains[1].Attack();
        if (trains.Count >= 3)
            if (!trains[2] && Input.GetKeyDown(KeyCode.C))
                trains[2].Attack();
        if (trains.Count >= 4)
            if (!trains[3] && Input.GetKeyDown(KeyCode.V))
                trains[3].Attack();
    }

    public void AddTrain(Weapon.WeaponType type)
    {
        GameObject temp;
        switch (type)
        {
            case Weapon.WeaponType.NORMAL:
                temp = Instantiate(Resources.Load("Prefabs/Normal") as GameObject);
                break;
            case Weapon.WeaponType.SHOTGUN:
                temp = Instantiate(Resources.Load("Prefabs/Shotgun") as GameObject);
                break;
            case Weapon.WeaponType.LASER:
                temp = Instantiate(Resources.Load("Prefabs/Laser") as GameObject);
                break;
            default:
                temp = new GameObject();
                break;
        }
        temp.transform.position = train_poss[trains.Count].position;
        trains.Add(temp.GetComponent<Weapon>());
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
