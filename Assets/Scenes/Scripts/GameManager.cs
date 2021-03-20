﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //default
    public Player player;
    public GameObject StartLogo;

    public GameObject Shelter;
    public GameObject ArrivePlanet;

    public GameObject MiddleBossPrefab;
    public GameObject LastBossPrefab;

    public Transform VicotryPanel;

    float elapsedTime = 0f;
    float TotalTime = 0f;
    private bool check = false;
    //TestEnemy
    int TestEnemycount = 0;
    int DiagolonCount = 0;
    int StageCount = 0;
    int Count = 0;

    public static bool StartStage = false;

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
        Stage1Enemy,
        Stage2Enemy,
        Stage3Enemy,
        Stage4Enemy
    }

    IEnumerator Stage1Enemy()
    {
        while (TestEnemycount <= 20)
        {
            yield return new WaitForSeconds(0.5f);

            //Random Pos
            float randomX = Random.Range(0f, 30f);
            //float X = 4f;

            ////랜덤 X값 저장
            Vector3 spos = new Vector3(randomX, 12f, 0f);
            ObjectPool.Instance.PopFromPool("Meteor").GetComponent<Meteor>().Init(spos);

            TestEnemycount++;
        }
    }
    IEnumerator Stage2Enemy()
    {
        while (TestEnemycount <= 40)
        {
            yield return new WaitForSeconds(0.25f);

            //Random Pos
            float randomX = Random.Range(0f, 30f);
            //float X = 4f;

            ////랜덤 X값 저장
            Vector3 spos = new Vector3(randomX, 12f, 0f);
            ObjectPool.Instance.PopFromPool("Meteor").GetComponent<Meteor>().Init(spos);

            TestEnemycount++;
        }
    }
    IEnumerator Stage3Enemy()
    {
        while (TestEnemycount <= 80)
        {
            yield return new WaitForSeconds(0.12f);

            //Random Pos
            float randomX = Random.Range(0f, 30f);
            //float X = 4f;

            ////랜덤 X값 저장
            Vector3 spos = new Vector3(randomX, 12f, 0f);
            ObjectPool.Instance.PopFromPool("Meteor").GetComponent<Meteor>().Init(spos);

            TestEnemycount++;
        }
    }
    IEnumerator Stage4Enemy()
    {
        while (TestEnemycount <= 100)
        {
            yield return new WaitForSeconds(0.1f);

            //Random Pos
            float randomX = Random.Range(0f, 30f);
            //float X = 4f;

            ////랜덤 X값 저장
            Vector3 spos = new Vector3(randomX, 12f, 0f);
            ObjectPool.Instance.PopFromPool("Meteor").GetComponent<Meteor>().Init(spos);

            TestEnemycount++;
        }
    }


    void SpawnPattern(EnemyPattern pattern)
    {
        switch (pattern)
        {
            case EnemyPattern.Stage1Enemy:
                {
                    if (TestEnemycount >= 21)
                        TestEnemycount = 0;

                    StartCoroutine(Stage1Enemy(/*transform.position*/));

                    break;
                }
            case EnemyPattern.Stage2Enemy:
                {
                    if (TestEnemycount >= 41)
                        TestEnemycount = 0;

                    StartCoroutine(Stage2Enemy(/*transform.position*/));

                    break;
                }
            case EnemyPattern.Stage3Enemy:
                {
                    if (TestEnemycount >= 81)
                        TestEnemycount = 0;

                    StartCoroutine(Stage3Enemy(/*transform.position*/));

                    break;
                }
            case EnemyPattern.Stage4Enemy:
                {
                    if (TestEnemycount >= 101)
                        TestEnemycount = 0;

                    StartCoroutine(Stage4Enemy(/*transform.position*/));

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
                    if (StartStage == true)
                    {
                        switch (StageCount)
                        {
                            case 1:
                                progress = PROGRESS.Stage1;
                                WavePattern = EnemyPattern.Stage1Enemy;
                                StartStage = false;
                                break;
                            case 2:
                                progress = PROGRESS.Stage2;
                                WavePattern = EnemyPattern.Stage2Enemy;
                                StartStage = false;
                                break;
                            case 3:
                                progress = PROGRESS.Stage3;
                                WavePattern = EnemyPattern.Stage3Enemy;
                                StartStage = false;
                                break;
                            case 4:
                                progress = PROGRESS.Stage4;
                                WavePattern = EnemyPattern.Stage4Enemy;
                                StartStage = false;
                                break;
                        }
                    }
                }
                break;

            case PROGRESS.Stage0:
                TotalTime += Time.deltaTime;

                if (elapsedTime >= 2)
                {
                    Instantiate(Shelter);
                    progress = PROGRESS.SHELTER;
                    StageCount = 1;
                    elapsedTime = 0f;
                }
                break;

            case PROGRESS.Stage1:
                TotalTime += Time.deltaTime;

                switch (WavePattern)
                {
                    case EnemyPattern.Stage1Enemy:

                        if (elapsedTime >= 5f)
                        {
                            SpawnPattern(EnemyPattern.Stage1Enemy);
                            elapsedTime = 0f;
                            Count++;
                        }
                        break;
                }
                if (Count == 20)
                {
                    StopCoroutine("Stage1Enemy");

                    Instantiate(Shelter);
                    progress = PROGRESS.SHELTER;
                    StageCount = 2;
                    Count = 0;
                    elapsedTime = 0f;
                }
                break;

            case PROGRESS.Stage2:
                TotalTime += Time.deltaTime;

                // 적 생성
                switch (WavePattern)
                {
                    case EnemyPattern.Stage2Enemy:

                        if (elapsedTime >= 5f)
                        {
                            SpawnPattern(EnemyPattern.Stage2Enemy);
                            elapsedTime = 0f;
                            Count++;
                        }
                        break;
                }
                if (Count == 24)
                {
                    StopCoroutine("Stage2Enemy");

                    Instantiate(Shelter);
                    progress = PROGRESS.SHELTER;
                    StageCount = 3;
                    Count = 0;
                    elapsedTime = 0f;
                }
                break;

            case PROGRESS.Stage3:
                TotalTime += Time.deltaTime;

                // 적 생성
                switch (WavePattern)
                {
                    case EnemyPattern.Stage3Enemy:

                        if (elapsedTime >= 5f)
                        {
                            SpawnPattern(EnemyPattern.Stage3Enemy);
                            elapsedTime = 0f;
                            Count++;
                        }
                        break;
                }
                if (Count == 36)
                {
                    StopCoroutine("Stage3Enemy");

                    Instantiate(Shelter);
                    progress = PROGRESS.SHELTER;
                    StageCount = 4;
                    Count = 0;
                    elapsedTime = 0f;
                }
                break;

            case PROGRESS.Stage4:
                TotalTime += Time.deltaTime;

                // 적 생성
                switch (WavePattern)
                {
                    case EnemyPattern.Stage4Enemy:

                        if (elapsedTime >= 4f)
                        {
                            SpawnPattern(EnemyPattern.Stage4Enemy);
                            elapsedTime = 0f;
                            Count++;
                        }
                        break;
                }
                if (Count == 36)
                {
                    StopCoroutine("Stage4Enemy");

                    Instantiate(ArrivePlanet);

                    StageCount = 4;
                    Count = 0;
                    elapsedTime = 0f;
                }
                break;

            case PROGRESS.BOSS:

                break;

        }
    }



}
