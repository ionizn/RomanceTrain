using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{   
    public static int score = 0;
        
    // Start is called before the first frame update
    void Start()
    { 

    }

    public void inc()
    {
        if (score < 999999)
        { 
            score += 1 ;
        }

    }

    public void Dec()
    {
        if (score > 0)
        {
            score -= 100;
        }

    }


}