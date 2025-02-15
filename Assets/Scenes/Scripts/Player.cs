﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    GameObject TrainHead;

    List<string> names = new List<string>
    {
        "btn_v","btn_c","btn_x","btn_z",
    };

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
        TrainHead = GameObject.Find("TrainHead");
        myRigid = GetComponent<Rigidbody2D>();
        m_Animator = gameObject.GetComponent<Animator>();
        audio = gameObject.AddComponent<AudioSource>();
        audio.clip = sound;
        audio.loop = false;
        GameObject.Find("Earth").transform.position = train_poss[0].position;
        TrainHead.transform.position = new Vector3(train_poss[1].position.x, train_poss[1].position.y + 1f, TrainHead.transform.position.z);
        GetComponent<BoxCollider2D>().offset = new Vector2(-19f, GetComponent<BoxCollider2D>().offset.y);
        trains.Clear();
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
            //    AddTrain(Weapon.WeaponType.NORMAL);
            if (trains.Count >= 1)
                if (trains[0] && Input.GetKey(KeyCode.V))
                    trains[0].Attack();
            if (trains.Count >= 2)
                if (trains[1] && Input.GetKey(KeyCode.C))
                    trains[1].Attack();
            if (trains.Count >= 3)
                if (trains[2] && Input.GetKey(KeyCode.X))
                    trains[2].Attack();
            if (trains.Count >= 4)
                if (trains[3] && Input.GetKey(KeyCode.Z))
                    trains[3].Attack();
        }
    }

    public void AddTrain(Weapon.WeaponType type)
    {
        GameObject temp;

        Debug.Log(type);

        switch (type)
        {
            case Weapon.WeaponType.NORMAL:
                temp = Instantiate(Resources.Load("Prefabs/Normal") as GameObject);
                temp.GetComponentInChildren<Weapon>().weapon_type = Weapon.WeaponType.NORMAL;
                break;
            case Weapon.WeaponType.SHOTGUN:
                temp = Instantiate(Resources.Load("Prefabs/Shotgun") as GameObject);
                temp.GetComponentInChildren<Weapon>().weapon_type = Weapon.WeaponType.SHOTGUN;
                break;
            case Weapon.WeaponType.LASER:
                temp = Instantiate(Resources.Load("Prefabs/Laser") as GameObject);
                temp.GetComponentInChildren<Weapon>().weapon_type = Weapon.WeaponType.LASER;
                break;
            default:
                temp = new GameObject();
                break;
        }

        temp.GetComponentsInChildren<SpriteRenderer>()[1].sprite = Resources.Load<Sprite>($"Btn_UI/{names[trains.Count]}");

        temp.transform.parent = GameObject.Find("Player").transform;
        temp.transform.localScale = new Vector3(11, 11, 11);
        int i;
        for (i = 0; i < trains.Count; i++)
        {
            Debug.Log($"TRAIN INDEX : {i} / POS INDEX : {trains.Count - 1 - i}");
            trains[i].transform.parent.position = train_poss[trains.Count - i].position;
        }
        GameObject.Find("Earth").transform.position = train_poss[i + 1].position;
        TrainHead.transform.position = new Vector3(train_poss[i + 2].position.x, train_poss[i + 2].position.y + 1f, TrainHead.transform.position.z);
        GetComponent<BoxCollider2D>().offset = new Vector2(GetComponent<BoxCollider2D>().offset.x+7.5f, GetComponent<BoxCollider2D>().offset.y);
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
