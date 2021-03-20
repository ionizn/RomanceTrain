using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform shotPrefab;
    public int ShootingRate;

    private int CoolTime = 100;
    private string bulletName;

    // Update is called once per frame
    void Update()
    {
        CoolTime -= 1;
        if (CoolTime < 0)
            CoolTime = 0;
    }

    public bool CanAttack()
    {
        if (CoolTime <= 0)
            return true;

        return false;
    }

    public bool Laser()
    {
        if (CanAttack())
        {
            CoolTime = ShootingRate;
            return true;
        }
        return false;
    }
    public void Shotgun()
    {
        if (CanAttack())
        {
            Vector3 vec = new Vector3(5f, -0.5f, 0f);
            bulletName = "Shotgun";

            //// 추적 총알 취소
            //for (float i = 0.5f; i >= -0.5f; i -= 0.1f)
            //{
            //    //총알 생성
            //    GameObject bullet = ObjectPool.Instance.PopFromPool(bulletName);

            //    bullet.gameObject.GetComponent<Shot>().ShotAngle(i);
            //    bullet.gameObject.transform.position = transform.position + vec;
            //    bullet.gameObject.SetActive(true);
            //}

            float r = 0;
            ////총알 생성
            for (int i = 0; i < 5; i++)
            {
                r = Random.Range(0, 100) * 0.01f;
                GameObject bullet = ObjectPool.Instance.PopFromPool(bulletName);
                bullet.gameObject.GetComponent<Shot>().ShotAngle(transform.localRotation.z * 2.7f + r);
                bullet.gameObject.transform.position = transform.Find("Tip").transform.position;
                bullet.gameObject.SetActive(true);
            }
            CoolTime = ShootingRate;
            //총알 발사 이펙트
            //GameObject temp = ObjectPool.Instance.PopFromPool("BulletShot");
            //temp.SetActive(true);
            //temp.transform.position = gameObject.transform.position + vec;
        }
    }

    public void Attack(bool isEnemy)
    {
        if (CanAttack())
        {
            if (gameObject.GetComponent<Health>().isEnemy == false)
                bulletName = "Bullet";
            else
                bulletName = "EBullet";

            //적이 아니면
            if (gameObject.GetComponent<Health>().isEnemy == false)
            {
                Vector3 vec = new Vector3(0.5f, -0.5f, 0f);

                //총알 생성
                GameObject bullet = ObjectPool.Instance.PopFromPool(bulletName);

                //Debug.Log(transform.parent.transform.localEulerAngles.z);
                //추적 총알 취소
                bullet.gameObject.GetComponent<Shot>().SetShotToPlayer(false);
                //bullet.gameObject.transform.parent = transform;
                bullet.gameObject.transform.position = transform.Find("Tip").transform.position;
                
                bullet.gameObject.GetComponent<Shot>().ShotAngle(transform.localRotation.z * 2.7f);
                
                bullet.gameObject.SetActive(true);
                

                //총알 발사 이펙트
                //GameObject temp = ObjectPool.Instance.PopFromPool("BulletShot");
                //temp.SetActive(true);
                //temp.transform.position = gameObject.transform.position + vec;

                CoolTime = ShootingRate;
            }

            //적이면
            else
            {
                //적 총알 생성
                GameObject bullet = ObjectPool.Instance.PopFromPool(bulletName);

                bullet.gameObject.transform.position = transform.position;
                bullet.gameObject.SetActive(true);
                bullet.gameObject.GetComponent<Shot>().SetShotToPlayer(true);

                CoolTime = ShootingRate;
            }
        }
    }
}