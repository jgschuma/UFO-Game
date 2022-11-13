using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class EnableStartRift : MonoBehaviour
{
    public static event Action WarpActive;
    void OnDisable(){
        WarpActive?.Invoke();
    }
}
