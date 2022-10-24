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
        if(other.CompareTag("Terrain") && isPrime == false){
            Debug.Log("Bomb Hit terrain");
            StartCoroutine(PrimeBomb());
        }
        else if (other.CompareTag("Enemy") && isPrime ==false){
            Debug.Log("Bomb Hit Enemy: " + other.gameObject.name);
            ThisRigidBody.constraints = RigidbodyConstraints2D.FreezePosition;
            StartCoroutine(PrimeBomb());
        }
    }

    public IEnumerator PrimeBomb()
    {
        isPrime = true;
        BombAnim.SetBool("IsPrime", true);
        yield return new WaitForSeconds(PrimeTimer);
        BombAnim.SetBool("IsBoom",true);
        yield return new WaitForSeconds(BoomTimer);
        GameObject Explosion = Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }



}
