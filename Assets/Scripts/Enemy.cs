using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject EnemySound;
    public int Damage;
    public float coolDown;
    private GameObject Player;
    private void Start()
    {
        EnemySound = GameObject.FindWithTag("EnemySound");
    }
    private void OnTriggerEnter2D(Collider2D Object)
    {
        if (Object.gameObject.tag == "Player")
        {
            EnemySound.GetComponent<AudioSource>().Play();
            Player = Object.gameObject;
            Player.GetComponent<Movement>().Health -= Damage / Shop.HpBuff;
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            StartCoroutine(CoolDown());
        }
        if (Object.gameObject.tag == "Trap")
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            StartCoroutine(CoolDown());
        }
    }
    IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(coolDown);
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }
}
