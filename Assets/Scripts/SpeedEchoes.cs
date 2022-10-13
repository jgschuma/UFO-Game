using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedEchoes : MonoBehaviour
{
    public int numOfEchoes = 3;
    public float timeBetweenEchoes = 0.2f;
    public GameObject speedEcho;

    //public Sprite[] sprites;
    float timeLeftUntilEcho = 0;
    PlayerController playerCon;

    // Start is called before the first frame update
    void Start()
    {
        //sprites = Resources.LoadAll<Sprite>("Assets/Sprites/Playable_UFO_Echoes-Sheet.png");
        //speedEcho = (GameObject)Resources.Load("Assets/Prefabs/SpeedEcho.prefab", typeof(GameObject));
        speedEcho.GetComponent<ProjectileDirection>().despawnTime = numOfEchoes * timeBetweenEchoes;
        playerCon = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerCon.dashDecay != 0)
        {
            timeLeftUntilEcho -= Time.deltaTime;
            //Time for an echo
            if (timeLeftUntilEcho <= 0)
            {
                timeLeftUntilEcho += timeBetweenEchoes;
                //Assets/Sprites/Playable_UFO-Sheet.png
                //Isolate the exact sprite of the UFO
                string spriteID = GetComponent<SpriteRenderer>().sprite.name;
                spriteID = spriteID.Substring(spriteID.IndexOf('O') + 8);
                Debug.Log("Sprite ID: " + spriteID);
                //Change sprite of echo to match current sprite
                //speedEcho.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Assets/Sprites/Playable_UFO_Echoes-Sheet-" + spriteID + ".png");
                //speedEcho.GetComponent<SpriteRenderer>().sprite = sprites[int.Parse(spriteID)];
                Instantiate(speedEcho, transform.position, Quaternion.Euler(0, 0, 0));
            }
        }
        else
            timeLeftUntilEcho = 0;
    }
}
