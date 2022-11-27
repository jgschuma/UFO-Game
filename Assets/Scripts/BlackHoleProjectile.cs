using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleProjectile : MonoBehaviour
{
    public float timeUntilDetonation = 1.5f;
    public GameObject blackHole;
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.GetComponent<Rigidbody2D>().freezeRotation = true;
        StartCoroutine(Detonate());
    }

    IEnumerator Detonate()
    {
        yield return new WaitForSeconds(timeUntilDetonation);
        Instantiate(blackHole, transform.position, Quaternion.Euler(0, 0, 0));
        Destroy(gameObject);
    }
}
