using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizerHelper : MonoBehaviour
{
    public bool FirstPlatform = false, ItIsCoin = false;
    public void OnTriggerEnter2D(Collider2D Object)
    {
        if (Object.gameObject.tag != "Player" && FirstPlatform == false && ItIsCoin == true)
        {
            Destroy(gameObject);
        }
        if (Object.gameObject.tag != "Player" && Object.gameObject.tag != "Trap" && FirstPlatform == false)
        {
            Destroy(gameObject);
        }
    }
}
