using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody2D rigidBody;
    public float despawnTime, damage, speed;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody.velocity = transform.right * speed;
        StartCoroutine(CountdownTimer());
    }

    void OnCollisionEnter2D(Collision2D col) {
        Destroy(gameObject);
    }

    IEnumerator CountdownTimer(){
        yield return new WaitForSeconds(despawnTime);
        Destroy(gameObject);
    }
}
