using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoofIntoNothing : MonoBehaviour
{
    public float secondUntilPoof;
    public GameObject poof;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SecondsUntilPoof());
    }

    IEnumerator SecondsUntilPoof()
    {
        yield return new WaitForSeconds(secondUntilPoof);
        Instantiate(poof, gameObject.transform.position, Quaternion.Euler(0, 0, 0));
        Destroy(gameObject);
    }
}
