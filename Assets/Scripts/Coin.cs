using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public GameObject CoinSound;
    public static int CoinPower;
    private void Start()
    {
        CoinSound = GameObject.FindWithTag("CoinSound");
    }
    private void OnTriggerEnter2D(Collider2D Object)
    {
        if (Object.gameObject.tag == "Player")
        {
            CoinSound.GetComponent<AudioSource>().Play();
            Movement.Coins += CoinPower;
            Object.GetComponent<Movement>().Health+=1/Shop.HpBuff;
            Destroy(gameObject);
        }
    }
}
