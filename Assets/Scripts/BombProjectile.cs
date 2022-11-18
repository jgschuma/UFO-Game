using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombProjectile : MonoBehaviour
{
    public Rigidbody2D ThisRigidBody;
    public float DespawnTimer;
    public float PrimeTimer;
    public float BoomTimer;
    public bool isPrime;
    public Animator BombAnim;
    public GameObject ExplosionPrefab;

    void Awake()
    {
        ThisRigidBody.freezeRotation = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // If it's an enemy freeze it's position so it won't keep falling
        if (other.CompareTag("Enemy") && isPrime ==false){
            Debug.Log("Bomb Hit Enemy: " + other.gameObject.name);
            isPrime = true;
            BombAnim.SetBool("IsPrime", true);
            StartCoroutine(Explode());
            /*            ThisRigidBody.constraints = RigidbodyConstraints2D.FreezePosition;
                        StartCoroutine(PrimeBomb());*/
        }
        // Start blowing up the bomb when it impacts with anything but the player
        else if(!other.CompareTag("Player") && isPrime == false){
            Debug.Log("Bomb Hit something");
            StartCoroutine(PrimeBomb());
        }
    }

    public IEnumerator PrimeBomb()
    {
        isPrime = true;
        BombAnim.SetBool("IsPrime", true);
        yield return new WaitForSeconds(PrimeTimer);
        StartCoroutine(Explode());
    }

    private IEnumerator Explode()
    {
        BombAnim.SetBool("IsBoom", true);
        FindObjectOfType<AudioManager>().Play("Explosion");
        yield return new WaitForSeconds(BoomTimer);
        GameObject Explosion = Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
