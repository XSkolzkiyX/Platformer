using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public int CostDoubleJump, CostStrongerJump, CostMoreHealth;
    public static int HpBuff = 1, CanBuyStrongerJump = 0, CanBuyMoreHealth = 0;
    public Text ScoreText, LevelText;
    public Button ButtonDoubleJump, ButtonStrongerJump, ButtonMoreHealth;
    public static bool DoubleJumpBought = false;
    public static float SpeedUpgrade = 70;
    void Start()
    {
        Movement.CanJump = 1;
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            NextLevel();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            DoubleJumpBought = false;
            HpBuff = 1;
            SpeedUpgrade = 70;
            CanBuyMoreHealth = 0;
            CanBuyStrongerJump = 0;
            Movement.Coins = 0;
            SceneRandomizer.Level = 1;
            SceneRandomizer.QuantityOfEnemy *= SceneRandomizer.Level;
            SceneRandomizer.QuantityOfPlatform *= SceneRandomizer.Level;
            SceneRandomizer.QuantityOfStar *= SceneRandomizer.Level;
            Movement.Border *= SceneRandomizer.Level;
            Movement.BorderWarning *= SceneRandomizer.Level;
            SceneRandomizer.RangeOfStars *= SceneRandomizer.Level;
            SceneRandomizer.RangeOfEnemiesAndCoins *= SceneRandomizer.Level;
            SceneRandomizer.RangeOfPlatforms *= SceneRandomizer.Level;
            SceneManager.LoadScene(0);
        }
        ScoreText.text = "Score: " + Movement.Coins;
        LevelText.text = "Level: " + SceneRandomizer.Level;
        if (DoubleJumpBought == false)
        {
            ButtonDoubleJump.GetComponentInChildren<Text>().text = "Cost: " + CostDoubleJump;
        }
        else
        {
            ButtonDoubleJump.GetComponentInChildren<Text>().text = "Max Level";
        }
        ButtonStrongerJump.GetComponentInChildren<Text>().text = "Cost: " + CostStrongerJump + "\nLevel: " + CanBuyStrongerJump;
        ButtonMoreHealth.GetComponentInChildren<Text>().text = "Cost: " + CostMoreHealth + "\nLevel: " + CanBuyMoreHealth;
        if (Movement.Coins < CostDoubleJump || DoubleJumpBought == true)
        {
            ButtonDoubleJump.interactable = false;
        }
        if (Movement.Coins < CostStrongerJump || CanBuyStrongerJump >= SceneRandomizer.Level)
        {
            ButtonStrongerJump.interactable = false;
        }
        if (Movement.Coins < CostMoreHealth || CanBuyMoreHealth >= SceneRandomizer.Level)
        {
            ButtonMoreHealth.interactable = false;
        }
    }
    public void NextLevel()
    {
        SceneRandomizer.QuantityOfEnemy *= SceneRandomizer.Level;
        SceneRandomizer.QuantityOfPlatform *= SceneRandomizer.Level;
        SceneRandomizer.QuantityOfStar *= SceneRandomizer.Level;
        Movement.Border *= SceneRandomizer.Level;
        Movement.BorderWarning *= SceneRandomizer.Level;
        SceneRandomizer.RangeOfStars *= SceneRandomizer.Level;
        SceneRandomizer.RangeOfEnemiesAndCoins *= SceneRandomizer.Level;
        SceneRandomizer.RangeOfPlatforms *= SceneRandomizer.Level;
        SceneManager.LoadScene(1);
    }
    public void BuyDoubleJump()
    {
        DoubleJumpBought = true;
        Movement.Coins -= CostDoubleJump;
        Movement.CanJump++;
    }
    public void BuyStrongerJump()
    {
        CanBuyStrongerJump++;
        Movement.Coins -= CostStrongerJump;
        SpeedUpgrade += 30;
    }
    public void BuyMoreHP()
    {
        CanBuyMoreHealth++;
        Movement.Coins -= CostMoreHealth;
        HpBuff++;
    }
}