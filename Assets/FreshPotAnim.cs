﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreshPotAnim : MonoBehaviour
{

    private Animation anim;


    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {

        anim.Play("freshpot");

    }
}
