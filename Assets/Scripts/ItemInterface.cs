using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPickup
{
    // Methods required for ItemPickups
    void Start();

    void OnTriggerEnter2D(Collider2D other);

    void Pickup();
}
