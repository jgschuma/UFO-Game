using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropOffHandler : MonoBehaviour
{
    private Animator anim;
    private GameObject acquireText;

    // Start is called before the first frame update
    void Start()
    {
        anim = transform.parent.transform.Find("Console").GetComponent<Animator>();
        acquireText = (GameObject)UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/Prefabs/ItemPopUp.prefab", typeof(GameObject));
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("Something touched the drop off");
        //Check to see if it's an item
        if (other.gameObject.tag == "ItemPickup")
        {
            Debug.Log("It's " + other.gameObject.name);
            //Update static screens
            if (other.gameObject.name == "BlackHolePickup")
                anim.SetBool("gotBlackHole", true);
            else if (other.gameObject.name == "BomberPickup")
                anim.SetBool("gotBomber", true);
            else if (other.gameObject.name == "FlamethrowerPickup")
                anim.SetBool("gotFlame", true);
            else if (other.gameObject.name == "GunnerPickup")
                anim.SetBool("gotGunner", true);
            else if (other.gameObject.name == "LaserPickup")
                anim.SetBool("gotLaser", true);
            else if (other.gameObject.name == "MissilePickup")
                anim.SetBool("gotMissile", true);
            else if (other.gameObject.name == "ShieldPickup")
                anim.SetBool("gotShield", true);
            else if (other.gameObject.name == "TwisterPickup")
                anim.SetBool("gotTwister", true);
            else if (other.gameObject.name == "WarpPickupActive")
                anim.SetBool("gotWarp", true);
            //Display acquisition 
            acquireText.GetComponent<SpriteRenderer>().sprite = other.gameObject.GetComponent<ItemPickup>().pickupNameSprite;
            acquireText.transform.Find("ItemSuffix").GetComponent<SpriteRenderer>().enabled = true;
            acquireText.transform.Find("ItemSuffix").GetComponent<SpriteRenderer>().sprite = (Sprite)other.gameObject.GetComponent<ItemPickup>().acquiredText;
            Instantiate(acquireText, transform.position + new Vector3(0,16,0), Quaternion.Euler(0, 0, 0));
            //Update console display
            anim.SetInteger("itemCount", anim.GetInteger("itemCount") + 1);
            //Remove item pickup
            Destroy(other.gameObject);
        }
    }
}
