using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseTrain : MonoBehaviour
{
    public GameObject UpGrade;

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
            GameObject.Find("Player").GetComponent<Player>().AddTrain(Weapon.WeaponType.NORMAL);
            Weapon.weapon_type_Int = 0;
        }
        else if (5 <= ran && ran <= 8)
        {
            GameObject.Find("Player").GetComponent<Player>().AddTrain(Weapon.WeaponType.SHOTGUN);
            Weapon.weapon_type_Int = 1;
        }
        else
        {
            GameObject.Find("Player").GetComponent<Player>().AddTrain(Weapon.WeaponType.LASER);
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
            GameObject.Find("Player").GetComponent<Player>().AddTrain(Weapon.WeaponType.NORMAL);
            Weapon.weapon_type_Int = 0;
        }
        else if (5 <= ran && ran <= 8)
        {
            GameObject.Find("Player").GetComponent<Player>().AddTrain(Weapon.WeaponType.SHOTGUN);
            Weapon.weapon_type_Int = 1;
        }
        else
        {
            GameObject.Find("Player").GetComponent<Player>().AddTrain(Weapon.WeaponType.LASER);
            Weapon.weapon_type_Int =2;
        }

        GameManager.StartStage = true;

        UpGrade.SetActive(false);
    }
}
