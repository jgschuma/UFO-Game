using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomberPower : MonoBehaviour
{
    public GameObject BombSpawn;
    public GameObject BombPrefab;
    public float CooldownDuration;
    private bool OnCooldown;
    
    void Start(){
        BeamController.DeactivatePower += DropPower;
        OnCooldown = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButton("Fire2") && OnCooldown == false)
        {
            StartCooldown();
            GameObject BomberInstance = Instantiate(BombPrefab, BombSpawn.transform.position, Quaternion.identity);
            FindObjectOfType<AudioManager>().Play("BombRelease");
        }
    }

    public IEnumerator BomberCooldown(){
        OnCooldown = true;

        yield return new WaitForSeconds(CooldownDuration);

        OnCooldown = false;
    }

    public void StartCooldown(){
        StartCoroutine(BomberCooldown());
    }

    /* If the TractorBeam drops an item, we set OnCooldown to false so we
    don't lock ourselves out if the bomber is on cooldown when we drop it
    */
    private void DropPower()
    {
        OnCooldown = false;
    }
}
