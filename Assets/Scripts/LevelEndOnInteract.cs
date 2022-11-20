using System.Collections;
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

    void OnTriggerStay2D(Collider2D other)
    {
        //It's the player, end the level
        if(other.gameObject.name == player.name && other.gameObject.tag == player.tag)
        {
            FindObjectOfType<AudioManager>().StopInteractable("UFOMovement");
            AustinEventManager.GameOver(false);
            other.gameObject.transform.Find("UI").GetComponent<Timer>().timerOn = false;
            other.gameObject.transform.Find("Main Camera").parent = null;
            other.gameObject.transform.Find("UI").parent = null;
            other.gameObject.SetActive(false);
            anim.SetTrigger("levelEnded");
            //end the game, with a reason other than death
        }
    }
}
