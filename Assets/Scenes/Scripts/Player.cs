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

    public GameObject BombPrefab;

    private new AudioSource audio;
    public AudioClip sound;

    public List<Transform> train_poss;
    List<Weapon> trains = new List<Weapon>();

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
        GameObject.Find("Earth").transform.position = train_poss[0].position;
        GameObject.Find("TrainHead").transform.position = train_poss[1].position;
    }

    ~Player()
    {
        OnDestroy();
    }

    void Update()
    {
        if (trains.Count > 0)
        {
            //if (Input.GetKeyDown(KeyCode.A))
              //  AddTrain(Weapon.WeaponType.NORMAL);
            if (trains.Count >= 1)
                if (trains[0] && Input.GetKey(KeyCode.Z))
                    trains[0].Attack();
            if (trains.Count >= 2)
                if (trains[1] && Input.GetKey(KeyCode.X))
                    trains[1].Attack();
            if (trains.Count >= 3)
                if (trains[2] && Input.GetKey(KeyCode.C))
                    trains[2].Attack();
            if (trains.Count >= 4)
                if (trains[3] && Input.GetKey(KeyCode.V))
                    trains[3].Attack();
        }
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
        temp.transform.parent = GameObject.Find("Player").transform;
        temp.transform.localScale = new Vector3(9, 9, 9);
        int i;
        for (i = 0; i < trains.Count; i++)
        {
            Debug.Log($"{i} : trains[i] : {trains[i]} : size : {trains.Count}");
            trains[i].transform.parent.position = train_poss[i + 1].position;
        }
        GameObject.Find("Earth").transform.position = train_poss[i + 1].position;
        GameObject.Find("TrainHead").transform.position = train_poss[i + 2].position;
        temp.transform.position = train_poss[0].position;
        trains.Add(temp.GetComponentInChildren<Weapon>());
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
