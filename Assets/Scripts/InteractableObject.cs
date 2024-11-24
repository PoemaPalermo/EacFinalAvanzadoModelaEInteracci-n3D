﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public bool activated;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!activated)
        {
            PlayObjectAnimation();
        }        
    }
    public void PlayObjectAnimation()
    {
        anim.SetTrigger("Open");
        activated = true;
    }
}
