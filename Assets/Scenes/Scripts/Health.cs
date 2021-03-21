using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public int hp = 100;
    public bool isEnemy = true;
    public string itemName = string.Empty;
    public bool MemoryDelete = false;
    public GameObject expolodePrefab;

    private AudioSource audio;
    public AudioClip sound;
    AudioSource audioSource;
    private void Start()
    {
        audio = gameObject.AddComponent<AudioSource>();
        audioSource = GameObject.Find("Meteor").GetComponent<AudioSource>();
        audio.clip = sound;
        audio.loop = false;

    }

    public void Damage(int value)
    {
        hp -= value;

        if(isEnemy)
        {
            //Vector3 vec = new Vector3(Random.Range(-1f,1f) ,Random.Range(-1f, 1f) , 0f);
            //GameObject temp = ObjectPool.Instance.PopFromPool("BulletExplode");
            //temp.SetActive(true);

            //temp.transform.position = gameObject.transform.position + vec;

            AL.ALUtil.Shaker.Instance.Shake();            
        }

        if (hp <= 0)
        {

            if (isEnemy)
            {
                audioSource.Play();
                if (GetComponent<Animator>() != null)
                {
                    GetComponent<Animator>().SetTrigger("isDestroy");
                    GetComponent<BoxCollider2D>().enabled = false;
                    StartCoroutine("DelayDestroy");
                }
            }
            if(gameObject.name.Equals("Player"))
            {
                //게임 오버
                StartCoroutine("GameOver");

            }


            else
                if (MemoryDelete == false)
                {
                    ObjectPool.Instance.PushToPool(itemName, gameObject);

                    ScoreSystem.score += 1;
                    GameObject p = GameObject.Find("Player");
                    //audio.PlayOneShot(sound, 1);
                }
                else
                {
                    ScoreSystem.score += 1;
                    GameObject p = GameObject.Find("Player");
                    Destroy(gameObject);
                }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Shot tempshot = collision.GetComponent<Shot>();
        if(tempshot != null)
        {
            if(gameObject.name != "Player")
            {
                Damage(tempshot.Damage);
                tempshot.PushPool();
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(gameObject.tag != collision.tag && gameObject.name != "Player")
        {
            Damage(1);
        }
    }

    IEnumerator DelayDestroy()
    {

        yield return new WaitForSeconds(0.4f);
        ObjectPool.Instance.PushToPool(itemName, gameObject);
    }

    IEnumerator GameOver()
    {
        GameObject.Find("FadeController").GetComponent<FadeController>().FadeIn(2.0f);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("GameOver");
    }
}
