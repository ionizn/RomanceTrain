using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public enum WeaponType
    {
        NORMAL,
        SHOTGUN,
        LASER
    }

    public WeaponType weapon_type;
    public float cool_time;
    public float ShootingRate;
    string bullet_name;
    Vector3 vec;
    public GameObject LazerPrefab;
    private GameObject Laser;
    Animator animator;
    AudioSource audioSource;

    public static int weapon_type_Int { get; set; }

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Start()
    {
        animator = transform.Find("New Sprite").GetComponent<Animator>();
    }
    public void Attack()
    {
        if(weapon_type_Int == 0)
        {
            weapon_type = WeaponType.NORMAL;
        }
        else if (weapon_type_Int == 1)
        {
            weapon_type = WeaponType.SHOTGUN;
        }
        else  if (weapon_type_Int == 2)
        {
            weapon_type = WeaponType.LASER;
        }

        if (CanAttack())
        {
            Debug.Log(weapon_type);
            switch (weapon_type)
            {
                case WeaponType.NORMAL:
                    {
                        animator.SetTrigger("Attack");

                        audioSource.Play();
                        if (gameObject.GetComponent<Health>().isEnemy == false)
                            bullet_name = "Bullet";
                        else
                            bullet_name = "EBullet";

                        if (gameObject.GetComponent<Health>().isEnemy == false)
                        {
                            vec = new Vector3(0.5f, -0.5f, 0f);
                            GameObject bullet = ObjectPool.Instance.PopFromPool(bullet_name);
                            bullet.gameObject.GetComponent<Shot>().SetShotToPlayer(false);
                            bullet.gameObject.transform.position = transform.Find("Tip").transform.position;
                            bullet.gameObject.GetComponent<Shot>().ShotAngle(transform.localRotation.z * 2.15f);
                            bullet.gameObject.SetActive(true);

                            cool_time = ShootingRate;
                        }
                        else
                        {
                            GameObject bullet = ObjectPool.Instance.PopFromPool(bullet_name);

                            bullet.gameObject.transform.position = transform.position;
                            bullet.gameObject.SetActive(true);
                            bullet.gameObject.GetComponent<Shot>().SetShotToPlayer(true);

                            cool_time = ShootingRate;
                        }
                    }
                    break;
                case WeaponType.SHOTGUN:
                    {
                        animator.SetTrigger("Attack");

                        audioSource.Play();
                        vec = new Vector3(5f, -0.5f, 0f);
                        bullet_name = "Shotgun";

                        float r = 0;
                        for (int i = 0; i < 5; i++)
                        {
                            r = Random.Range(0, 100) * 0.01f;
                            GameObject bullet = ObjectPool.Instance.PopFromPool(bullet_name);
                            bullet.gameObject.GetComponent<Shot>().ShotAngle(transform.localRotation.z * 1.6f + r);
                            bullet.gameObject.transform.position = transform.Find("Tip").transform.position;
                            bullet.gameObject.SetActive(true);

                            Debug.Log(transform.localRotation.z * 1.6f + r);
                        }
                        cool_time = ShootingRate;
                    }
                    break;
                case WeaponType.LASER:
                    {
                        audioSource.Play();
                        vec = new Vector3(-5f, -0.5f, 0f);
                        Laser = Instantiate(LazerPrefab);
                        Laser.transform.parent = transform.parent;

                        Laser.transform.rotation = transform.rotation;
                        Laser.transform.position = transform.position;

                        cool_time = ShootingRate;
                    }
                    break;
            }
        }
    }

    void Update()
    {
        cool_time -= Time.deltaTime;
        if (cool_time < 0)
            cool_time = 0;
    }

    public bool CanAttack()
    {
        if (cool_time <= 0)
            return true;
        return false;
    }
}