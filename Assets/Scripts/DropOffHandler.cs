using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropOffHandler : MonoBehaviour
{
    private Animator anim;
    public GameObject acquireText;
    private Timer timer;
    private int numItemsAquired = 0;

    public int AllClearBonus;
    public float minPVPercentageMult;

    [Header("Base Item Point Values")]
    public int basePV_BlackHole;
    public int basePV_Bomber;
    public int basePV_Flamethrower;
    public int basePV_Gunner;
    public int basePV_Laser;
    public int basePV_Missile;
    public int basePV_Shield;
    public int basePV_Twister;
    public int basePV_Warp;

    // Start is called before the first frame update
    void Start()
    {
        anim = transform.parent.transform.Find("Console").GetComponent<Animator>();
        timer = FindObjectOfType<Timer>();
    }

    void FixedUpdate(){
        if (numItemsAquired == 9){
            numItemsAquired = 0;
            AustinEventManager.ScorePoints(AllClearBonus);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("Something touched the drop off");
        //Check to see if it's an item
        if (other.gameObject.tag == "ItemPickup")
        {
            Debug.Log("It's " + other.gameObject.name);
            //Update static screens
            if (other.gameObject.name == "BlackHolePickup"){
                anim.SetBool("gotBlackHole", true);
                ScorePickup(basePV_BlackHole);
                numItemsAquired++;
            } else if (other.gameObject.name == "BomberPickup"){
                anim.SetBool("gotBomber", true);
                ScorePickup(basePV_Bomber);
                numItemsAquired++;
            } else if (other.gameObject.name == "FlamethrowerPickup"){
                anim.SetBool("gotFlame", true);
                ScorePickup(basePV_Flamethrower);
                numItemsAquired++;
            } else if (other.gameObject.name == "GunnerPickup"){
                anim.SetBool("gotGunner", true);
                ScorePickup(basePV_Gunner);
                numItemsAquired++;
            } else if (other.gameObject.name == "LaserPickup"){
                anim.SetBool("gotLaser", true);
                ScorePickup(basePV_Laser);
                numItemsAquired++;
            } else if (other.gameObject.name == "MissilePickup"){
                anim.SetBool("gotMissile", true);
                ScorePickup(basePV_Missile);
                numItemsAquired++;
            } else if (other.gameObject.name == "ShieldPickup"){
                anim.SetBool("gotShield", true);
                ScorePickup(basePV_Shield);
                numItemsAquired++;
            } else if (other.gameObject.name == "TwisterPickup"){
                anim.SetBool("gotTwister", true);
                ScorePickup(basePV_Twister);
                numItemsAquired++;
            } else if (other.gameObject.name == "WarpPickupActive"){
                anim.SetBool("gotWarp", true);
                ScorePickup(basePV_Warp);
                numItemsAquired++;
                //Disable warp rift start
                Destroy(GameObject.Find("WarpRiftStart"));
            }
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

    void ScorePickup(int basePV){
        // Get the current time
        float currentTime = timer.CalculateCurrentTime();
        Debug.Log("time on deposit: " + currentTime + "s");

        // Get a percentage of time with 100% being the start, and 0% being the par time defined in the timer
        float timePercentage = (timer.parTimeInSeconds - currentTime) / timer.parTimeInSeconds;
        Debug.Log("timePercentage: " + timePercentage);

        // If the time percentage is too low, items are worth nothing. This check stops that
        // and makes the items worth at least a little bit
        if (timePercentage < minPVPercentageMult){
            timePercentage = minPVPercentageMult;
        }
        Debug.Log("timePercentage after Check: " + timePercentage);

        // Calculate the score by multipling the basePV by the timePercentage
        int pvToScore = (int)Mathf.Floor(basePV * timePercentage);
        Debug.Log("PV to Score: " + pvToScore);

        AustinEventManager.ScorePoints(pvToScore);
    }
}
