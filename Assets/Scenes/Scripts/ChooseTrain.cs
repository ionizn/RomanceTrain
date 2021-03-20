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

        if(1 <= ran  && ran <= 4)
        {
            GameObject.Find("Player").GetComponent<Player>().AddTrain(Weapon.WeaponType.NORMAL);
        }
        else if (5 <= ran && ran <= 8)
        {
            GameObject.Find("Player").GetComponent<Player>().AddTrain(Weapon.WeaponType.SHOTGUN);
        }
        else
        {
            GameObject.Find("Player").GetComponent<Player>().AddTrain(Weapon.WeaponType.LASER);
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
        }
        else if (5 <= ran && ran <= 8)
        {
            GameObject.Find("Player").GetComponent<Player>().AddTrain(Weapon.WeaponType.SHOTGUN);
        }
        else
        {
            GameObject.Find("Player").GetComponent<Player>().AddTrain(Weapon.WeaponType.LASER);
        }

        GameManager.StartStage = true;

        UpGrade.SetActive(false);
    }
}
