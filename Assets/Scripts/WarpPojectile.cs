using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpPojectile : MonoBehaviour
{
    private Animator Anim;
    private Collider2D[] WarpZone;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().freezeRotation = true;
        Anim = GetComponentInChildren(typeof(Animator)) as Animator;
        Physics2D.IgnoreLayerCollision(14,18, true);
        Physics2D.IgnoreLayerCollision(15,18, true);
        Physics2D.IgnoreLayerCollision(16,18, true);
        Physics2D.IgnoreLayerCollision(17,18, true);
    }

    void Update(){
        //Debug.Log("Open is true: " + Anim.GetBool("open"));
    }

    // void OnCollisionStay2D(Collision2D collision)
    // {
    //     if(collision.gameObject.tag == "Terrain"){
    //         Anim.SetBool("open", true);
    //     }
    // }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Terrain")){
            //Debug.Log("Warp Puck hit the enviromnet");
            // Create the rift above the puck
            Anim.SetBool("open", true);
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Terrain")){
            //Debug.Log("Warp Puck left the environment");
            // Create the rift above the puck
            Anim.SetBool("open", false);
        }
    }
}
