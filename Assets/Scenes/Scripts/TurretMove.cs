﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretMove : MonoBehaviour
{
    float angle;
    Vector2 target, mouse;

    private void Start()
    {
        target = transform.position;
    }
    private void Update()
    {
        mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        angle = Mathf.Atan2(mouse.y - target.y, mouse.x - target.x) * Mathf.Rad2Deg;
        
        if(angle >= 0 ) 
            this.transform.rotation = Quaternion.AngleAxis(angle , Vector3.forward );

    }
}
