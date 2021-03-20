using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //default
    public Player player;
    public GameObject StartLogo;

    public GameObject MiddleBossPrefab;
    public GameObject LastBossPrefab;

    public Transform VicotryPanel;

    float elapsedTime = 0f;
    float TotalTime = 0f;
    private bool check = false;
    //TestEnemy
    int TestEnemycount = 0;
    int DiagolonCount = 0;

    //enum
    PROGRESS progress = PROGRESS.START;
    EnemyPattern WavePattern = EnemyPattern.TestEnemy;

    int lastStage = 0;

    enum PROGRESS
    {
        Stage0,
        Stage1,
        Stage2,
        Stage3,
        Stage4,
        START,
        SHELTER,
        BOSS
    }
    enum EnemyPattern
    {
        NONE,
        TestEnemy,

    }

    //Function
    private void Start()
    {

    }

    IEnumerator SpawnTestEnemy()
    {
        while (TestEnemycount <= 4)
        {
            yield return new WaitForSeconds(0.4f);

            //Random Pos
            float randomX = Random.Range(0f, 24f);
            //float X = 4f;

            ////랜덤 X값 저장
            Vector3 spos = new Vector3(randomX, 8f, 0f);
            ObjectPool.Instance.PopFromPool("Meteor").GetComponent<Meteor>().Init(spos);

            TestEnemycount++;
        }
    }

    void SpawnPattern(EnemyPattern pattern)
    {
        switch (pattern)
        {
            case EnemyPattern.TestEnemy:
                {//뱀 랜덤위치에 소환
                 //랜덤 Y값 생성
                 //float randomX = Random.Range(-0f, 0f);
                 //float X = 4f;

                    ////랜덤 X값 저장
                    //Vector3 pos = new Vector3(randomX, 16f, 0f);


                    ////뱀 오프셋
                    //Vector3 offset = new Vector3(4f, 0f, 0f);

                    //뱀 생성
                    if (TestEnemycount >= 5)
                        TestEnemycount = 0;

                    StartCoroutine(SpawnTestEnemy(/*transform.position*/));

                    break;
                }
        }
    }

    bool Checking = false;

    bool MiddleBossSpawn = false;
    GameObject MiddleBos;

    bool LastBossSpawn = false;
    GameObject LastBoss;



    void Update()
    {
        elapsedTime += Time.deltaTime;

        switch (progress)
        {

            //게임 시작
            case PROGRESS.START:
                if (elapsedTime >= 2)
                {
                    Instantiate(StartLogo);
                    progress = PROGRESS.Stage0;
                    elapsedTime = 0f;
                }
                break;

            case PROGRESS.SHELTER:
                {
                    // 업그레이드 선택 대기 상태
                    //선택 하면 다음 페이즈로 이동

                }
                break;

            case PROGRESS.Stage0:
                TotalTime += Time.deltaTime;



                if (elapsedTime >= 20)
                {
                    elapsedTime = 0f;

                }
                break;

            case PROGRESS.Stage1:
                TotalTime += Time.deltaTime;


                /*
                    적 생성
                 */

                //switch (WavePattern)
                //{
                //    case EnemyPattern.TestEnemy:
                //        if (elapsedTime >= 6f)
                //        {
                //            SpawnPattern(EnemyPattern.TestEnemy);
                //            elapsedTime = 0f;
                //        }


                //        break;


                //}

                if (elapsedTime >= 20)
                {
                    elapsedTime = 0f;

                }
                break;

            case PROGRESS.Stage2:
                TotalTime += Time.deltaTime;


                /*
                    적 생성
                 */

                //switch (WavePattern)
                //{
                //    case EnemyPattern.TestEnemy:
                //        if (elapsedTime >= 6f)
                //        {
                //            SpawnPattern(EnemyPattern.TestEnemy);
                //            elapsedTime = 0f;
                //        }


                //        break;


                //}

                if (elapsedTime >= 20)
                {
                    elapsedTime = 0f;

                }
                break;

            case PROGRESS.Stage3:
                TotalTime += Time.deltaTime;


                /*
                    적 생성
                 */

                //switch (WavePattern)
                //{
                //    case EnemyPattern.TestEnemy:
                //        if (elapsedTime >= 6f)
                //        {
                //            SpawnPattern(EnemyPattern.TestEnemy);
                //            elapsedTime = 0f;
                //        }


                //        break;


                //}

                if (elapsedTime >= 20)
                {
                    elapsedTime = 0f;

                }
                break;
            case PROGRESS.Stage4:
                TotalTime += Time.deltaTime;


                /*
                    적 생성
                 */

                //switch (WavePattern)
                //{
                //    case EnemyPattern.TestEnemy:
                //        if (elapsedTime >= 6f)
                //        {
                //            SpawnPattern(EnemyPattern.TestEnemy);
                //            elapsedTime = 0f;
                //        }


                //        break;


                //}

                if (elapsedTime >= 20)
                {
                    elapsedTime = 0f;

                }
                break;
            case PROGRESS.BOSS:

                break;

        }
    }



}
