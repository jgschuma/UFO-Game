using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<breaksOneWays>().breaksOW)
        {
            GetComponent<Animator>().SetTrigger("destroy");
        }
    }
}
