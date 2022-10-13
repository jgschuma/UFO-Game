using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityHealth : MonoBehaviour
{
    public int health = 3; 

    public void doDamage(int _damageAmount)
    {
        health -= _damageAmount;
        if (health <= 0)
        {
            //Kill the entity
        }
    }
}
