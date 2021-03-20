using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Shelter : MonoBehaviour
{
    private void Start()
    {
        Vector3 pos = new Vector3(34f, -9f, 0f);
        gameObject.transform.Translate(pos);
        //GetComponent<SpriteRenderer>().sortingOrder = 1;
    }
    //5~-1
    // Update is called once per frame
    void Update()
    {
        Vector3 pos = gameObject.transform.position;
        Vector3 targetScreenPos = Camera.main.WorldToScreenPoint(pos);

        if (targetScreenPos.x < Screen.width || targetScreenPos.x > 0 || targetScreenPos.y < Screen.height || targetScreenPos.y > 0)
        {
            gameObject.transform.Translate(-8f * Time.deltaTime, 0f, 0f);
        }

        if (targetScreenPos.x + 788 < 0)
            Destroy(gameObject);
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            Destroy(gameObject);

            GameObject.Find("ChooseTrains").GetComponent<ChooseTrain>().AttiveUpgrade();
            GameObject.Find("GameManager").GetComponent<GameManager>().ScrollStop();
        }
    }
    
}