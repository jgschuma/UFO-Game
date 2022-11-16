using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTester : MonoBehaviour
{
    public Collider2D col;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerStay2D(Collider2D col){
        AustinEventManager.ScorePoints(1);
    }
}
