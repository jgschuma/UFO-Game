using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedEchoes : MonoBehaviour
{
    public int numOfEchoes = 3;
    public float timeBetweenEchoes = 0.2f;
    public GameObject speedEcho;
    private Object[] echoSprites;

    float timeLeftUntilEcho = 0;
    PlayerController playerCon;

    // Start is called before the first frame update
    void Start()
    {
        speedEcho.GetComponent<ProjectileDirection>().despawnTime = numOfEchoes * timeBetweenEchoes;
        playerCon = GetComponent<PlayerController>();
        echoSprites = UnityEditor.AssetDatabase.LoadAllAssetsAtPath("Assets/Sprites/Playable_UFO_Echoes-Sheet.png");
        //The full sheet and Sprite 0 are swapped for some reason, swap them back
        Object sprite0 = echoSprites[0];
        echoSprites[0] = echoSprites[1];
        echoSprites[1] = sprite0;
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
                //Isolate the exact sprite of the UFO
                string spriteID = GetComponent<SpriteRenderer>().sprite.name;
                spriteID = spriteID.Substring(spriteID.IndexOf('O') + 8);
                //Debug.Log("Sprite ID: " + spriteID);
                //Change sprite of echo to match current sprite
                speedEcho.GetComponent<SpriteRenderer>().sprite = (Sprite)echoSprites[int.Parse(spriteID) + 1];
                speedEcho.GetComponent<SpriteRenderer>().flipX = GetComponent<SpriteRenderer>().flipX;
                Instantiate(speedEcho, transform.position, Quaternion.Euler(0, 0, 0));
            }
        }
        else
            timeLeftUntilEcho = 0;
    }
}
