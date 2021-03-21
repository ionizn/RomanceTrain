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
    public Sprite normalButtonImage;
    public Sprite shotgunButtonImage;
    public Sprite laserButtonImage;

    int sel1;
    int sel2;

    private void Start()
    {
        player = GameObject.Find("Player");
   
    }

    AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void AttiveUpgrade()
    {

        //Active 되기 전에 무기 선택

        int sel1 = Random.Range(1, 10);
        int sel2 = Random.Range(1, 10);


        if (1 <= sel1 && sel1 <= 4)
        {
            case1.sprite = normalButtonImage;

        }
        else if (5 <= sel1 && sel1 <= 8)
        {

            case1.sprite = shotgunButtonImage;

        }
        else
        {
            case1.sprite = laserButtonImage;

        }

        if (1 <= sel2 && sel2 <= 4)
        {

            case2.sprite = normalButtonImage;

        }
        else if (5 <= sel2 && sel2 <= 8)
        {

            case2.sprite = shotgunButtonImage;

        }
        else
        {

            case2.sprite = laserButtonImage;

        }


        UpGrade.SetActive(true);
    }

    public void OnClickCase1()
    {

      
        if(1 >= sel1  && sel1 <= 4)
        {
            Debug.Log("RANDOM -> NORMAL");
            player.GetComponent<Player>().AddTrain(Weapon.WeaponType.NORMAL);
            //case1.sprite = Resources.Load<Sprite>("Sprites/Normal");
        }
        else if (5 >= sel1 && sel1 <= 8)
        {
            Debug.Log("RANDOM -> SHOTGUN");
            player.GetComponent<Player>().AddTrain(Weapon.WeaponType.SHOTGUN);
            //case1.sprite = Resources.Load<Sprite>("Sprites/Shotgun");
        }
        else
        {
            Debug.Log("RANDOM -> LASER");
            player.GetComponent<Player>().AddTrain(Weapon.WeaponType.LASER);
            //case1.sprite = Resources.Load<Sprite>("Sprites/Laser");
        }


        audioSource.Play();

        GameManager.StartStage = true;
        GameObject.Find("Player").GetComponent<Health>().hp = 10;
        UpGrade.SetActive(false);
    }

    public void OnClickCase2()
    {


        if (1 >= sel2 && sel2 <= 4)
        {
            player.GetComponent<Player>().AddTrain(Weapon.WeaponType.NORMAL);
            //case2.sprite = Resources.Load<Sprite>("Sprites/Normal");
        }
        else if (5 >= sel2 && sel2 <= 8)
        {
            player.GetComponent<Player>().AddTrain(Weapon.WeaponType.SHOTGUN);
            //case2.sprite = Resources.Load<Sprite>("Sprites/Shotgun");
        }
        else
        {
            player.GetComponent<Player>().AddTrain(Weapon.WeaponType.LASER);
            //case2.sprite = Resources.Load<Sprite>("Sprites/Laser");
        }

        audioSource.Play();

        GameManager.StartStage = true;
        GameObject.Find("Player").GetComponent<Health>().hp = 10;

        UpGrade.SetActive(false);
    }
}
