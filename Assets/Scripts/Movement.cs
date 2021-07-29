using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Movement : MonoBehaviour
{
    bool AlreadyPlayedDeath = false;
    public GameObject NextButton, OST, JumpSound, DeathSound;
    public float Speed, MaxSpeed, Health = 100f;
    public static int Coins = 0, CanJump, CoinsNeedToNext, Border = 250, BorderWarning = 205;
    private Rigidbody2D Rb;
    public bool  IsDead = false;
    public Text LevelText, CoinText, PressText, CoinsNeedToNextText, WarningText;
    void Start()
    {
        Speed = Shop.SpeedUpgrade;
        Cursor.visible = false;
        Rb = gameObject.GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        if(Coins>= CoinsNeedToNext)
        {
            Cursor.visible = true;
            NextButton.SetActive(true);
            if (Input.GetKey(KeyCode.Space))
            {
                NextLevel();
            }
        }
        if(Rb.velocity.x> MaxSpeed)
        {
            Rb.velocity = new Vector3(MaxSpeed, Rb.velocity.y);
        }
        if (Rb.velocity.x <-MaxSpeed)
        {
            Rb.velocity = new Vector3(-MaxSpeed, Rb.velocity.y);
        }
        if (Rb.velocity.y > MaxSpeed)
        {
            Rb.velocity = new Vector3(Rb.velocity.x, MaxSpeed);
        }
        if (Rb.velocity.y < -MaxSpeed)
        {
            Rb.velocity = new Vector3(Rb.velocity.x,-MaxSpeed);
        }
        if (transform.position.x > Border)
        {
            Health = 0;
        }
        if (transform.position.x < -Border)
        {
            Health = 0;
        }
        if (transform.position.y > Border)
        {
            Health = 0;
        }
        if (transform.position.y < -Border)
        {
            Health = 0;
        }
        if (transform.position.x > BorderWarning)
        {
            WarningText.gameObject.SetActive(true);
        }
        else if (transform.position.x < -BorderWarning)
        {
            WarningText.gameObject.SetActive(true);
        }
        else if (transform.position.y > BorderWarning)
        {
            WarningText.gameObject.SetActive(true);
        }
        else if (transform.position.y < -BorderWarning)
        {
            WarningText.gameObject.SetActive(true);
        }
        else
        {
            WarningText.gameObject.SetActive(false);
        }
        LevelText.text = "Level: " + SceneRandomizer.Level;
        CoinText.text = "Scores: " + Coins;
        CoinsNeedToNextText.text = "(-" + CoinsNeedToNext + ")";
        if (Health > 100)
        {
            Health = 100;
        }
        if (Health > 0 && IsDead == false)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1f - Health / 100f, 0f, Health / 100f);
            Camera.main.fieldOfView = Mathf.Abs(Rb.velocity.y) / 2 + 120;
            Camera.main.fieldOfView = Mathf.Abs(Rb.velocity.x) / 2 + 120;
            Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, Camera.main.transform.position.z);
            if (Input.GetKeyDown(KeyCode.UpArrow) && CanJump > 0)
            {
                JumpSound.GetComponent<AudioSource>().Play();
                Rb.AddForce(Vector2.up * Speed);
                CanJump--;
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                Rb.AddForce(Vector2.left);
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                Rb.AddForce(Vector2.right);
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Coins = 0;
            SceneRandomizer.Level = 1;
            SceneManager.LoadScene(0);
            Shop.DoubleJumpBought = false;
            Shop.HpBuff = 1;
            Shop.SpeedUpgrade = 70;
            Shop.CanBuyMoreHealth = 0;
            Shop.CanBuyStrongerJump = 0;
            SceneRandomizer.QuantityOfEnemy *= SceneRandomizer.Level;
            SceneRandomizer.QuantityOfPlatform *= SceneRandomizer.Level;
            SceneRandomizer.QuantityOfStar *= SceneRandomizer.Level;
            Border *= SceneRandomizer.Level;
            BorderWarning *= SceneRandomizer.Level;
            SceneRandomizer.RangeOfStars *= SceneRandomizer.Level;
            SceneRandomizer.RangeOfEnemiesAndCoins *= SceneRandomizer.Level;
            SceneRandomizer.RangeOfPlatforms *= SceneRandomizer.Level;
        }
        if(Health <= 0)
        {
            IsDead = true;
            Health = 0;
        }
        if (IsDead == true)
        {
            WarningText.gameObject.SetActive(false);
            if(AlreadyPlayedDeath == false)
            {
                DeathSound.GetComponent<AudioSource>().Play();
                AlreadyPlayedDeath = true;
            }
            Cursor.visible = false;
            Shop.DoubleJumpBought = false;
            Shop.HpBuff = 1;
            Shop.SpeedUpgrade = 70;
            Shop.CanBuyMoreHealth = 0;
            Shop.CanBuyStrongerJump = 0;
            Health = 0;
            Coins = 0;
            NextButton.SetActive(false);
            StopAllCoroutines();
            LevelText.enabled = false;
            CoinText.enabled = false;
            CoinsNeedToNextText.enabled = false;
            PressText.text = "You Dead\nPress R";
            Coins = 0;
            if (Input.GetKey(KeyCode.R))
            {
                SceneRandomizer.Level = 1;
                SceneRandomizer.QuantityOfEnemy *= SceneRandomizer.Level;
                SceneRandomizer.QuantityOfPlatform *= SceneRandomizer.Level;
                SceneRandomizer.QuantityOfStar *= SceneRandomizer.Level;
                Border *= SceneRandomizer.Level;
                BorderWarning *= SceneRandomizer.Level;
                SceneRandomizer.RangeOfStars *= SceneRandomizer.Level;
                SceneRandomizer.RangeOfEnemiesAndCoins *= SceneRandomizer.Level;
                SceneRandomizer.RangeOfPlatforms *= SceneRandomizer.Level;
                SceneManager.LoadScene(1);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Shop.DoubleJumpBought == true)
        {
            CanJump = 2;
        }
        else
        {
            CanJump = 1;
        }
    }
    public void NextLevel()
    {
        Coins -= CoinsNeedToNext;
        SceneRandomizer.Level++;
        SceneManager.LoadScene(2);
    }
}
