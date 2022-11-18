﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEndOnInteract : MonoBehaviour
{
    public GameObject player;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

/*    // Update is called once per frame
    void Update()
    {

    }*/

    void OnTriggerStay2D(Collider2D other)
    {
        //It's the player, end the level
        if(other.gameObject.name == player.name && other.gameObject.tag == player.tag)
        {
            other.gameObject.transform.Find("Main Camera").parent = null;
            other.gameObject.transform.Find("UI").parent = null;
            Destroy(other.gameObject);
            anim.SetTrigger("levelEnded");
            //end the game, with a reason other than death
            AustinEventManager.GameOver(false);
        }
    }
}
