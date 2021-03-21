using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseTrain : MonoBehaviour
{
    public GameObject UpGrade;
    public Image case1;
    public Image case2;

    GameObject player;

    private void Start()
    {
        player = GameObject.Find("Player");
    }

    public void AttiveUpgrade()
    {
        UpGrade.SetActive(true);
    }

    public void OnClickCase1()
    {
        int ran = Random.Range(1, 10);
        
        Debug.Log(ran);

        if(1 <= ran  && ran <= 4)
        {
            player.GetComponent<Player>().AddTrain(Weapon.WeaponType.NORMAL);
            case1.sprite = Resources.Load<Sprite>("Sprites/Normal");
            Weapon.weapon_type_Int = 0;
        }
        else if (5 <= ran && ran <= 8)
        {
            player.GetComponent<Player>().AddTrain(Weapon.WeaponType.SHOTGUN);
            case1.sprite = Resources.Load<Sprite>("Sprites/Shotgun");
            Weapon.weapon_type_Int = 1;
        }
        else
        {
            player.GetComponent<Player>().AddTrain(Weapon.WeaponType.LASER);
            case1.sprite = Resources.Load<Sprite>("Sprites/Laser");
            Weapon.weapon_type_Int = 2;
        }

        GameManager.StartStage = true;

        UpGrade.SetActive(false);
    }

    public void OnClickCase2()
    {
        int ran = Random.Range(1, 10);

        if (1 <= ran && ran <= 4)
        {
            player.GetComponent<Player>().AddTrain(Weapon.WeaponType.NORMAL);
            case2.sprite = Resources.Load<Sprite>("Sprites/Normal");
            Weapon.weapon_type_Int = 0;
        }
        else if (5 <= ran && ran <= 8)
        {
            player.GetComponent<Player>().AddTrain(Weapon.WeaponType.SHOTGUN);
            case2.sprite = Resources.Load<Sprite>("Sprites/Shotgun");
            Weapon.weapon_type_Int = 1;
        }
        else
        {
            player.GetComponent<Player>().AddTrain(Weapon.WeaponType.LASER);
            case2.sprite = Resources.Load<Sprite>("Sprites/Laser");
            Weapon.weapon_type_Int =2;
        }

        GameManager.StartStage = true;

        UpGrade.SetActive(false);
    }
}
