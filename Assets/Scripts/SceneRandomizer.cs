using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneRandomizer : MonoBehaviour
{
    public static int Level = 1, QuantityOfStar = 25000, QuantityOfCoin = 2400, QuantityOfEnemy = 3000, QuantityOfPlatform = 360;
    public static float RangeOfStars = 300, RangeOfEnemiesAndCoins = 200, RangeOfPlatforms = 20;
    public GameObject StarPrefab, CoinPrefab, EnemyPrefab, PlatformPrtefab;
    public int StarsForMenu;
    public bool ItIsMenu = false;
    void Start()
    {
        Movement.CoinsNeedToNext = 10 * Level;
        Coin.CoinPower = Level;
        if (ItIsMenu == true)
        {
            for (int i = 0; i < StarsForMenu; i++)
            {
                GameObject star = Instantiate(StarPrefab, new Vector3(Random.Range(-10.0f, 10.0f), Random.Range(-8.0f, 8.0f), 1), Quaternion.identity);
                float Scale = Random.Range(0.005f, 0.01f);
                star.transform.localScale = new Vector3(Scale, Scale, 0);
            }
        }
        else
        {
            for (int i = 0; i < QuantityOfStar; i++)
            {
                GameObject star = Instantiate(StarPrefab, new Vector3(Random.Range(-RangeOfStars, RangeOfStars), Random.Range(-RangeOfStars, RangeOfStars), 1), Quaternion.identity);
                float Scale = Random.Range(0.005f, 0.01f);
                star.transform.localScale = new Vector3(Scale, Scale, 0);
            }
            for (int i = 0; i < QuantityOfCoin; i++)
            {
                GameObject coin = Instantiate(CoinPrefab, new Vector3(Random.Range(-RangeOfEnemiesAndCoins, RangeOfEnemiesAndCoins), Random.Range(-RangeOfEnemiesAndCoins, RangeOfEnemiesAndCoins), 0), Quaternion.identity);
            }
            float QE = QuantityOfEnemy * Level;
            for (int i = 0; i < QE; i++)
            {
                GameObject enemy = Instantiate(EnemyPrefab, new Vector3(Random.Range(-RangeOfEnemiesAndCoins, RangeOfEnemiesAndCoins), Random.Range(-RangeOfEnemiesAndCoins, RangeOfEnemiesAndCoins), 0), Quaternion.identity);
                enemy.transform.Rotate(0, 0, 45);
            }
            int NeedToBeTrap = Random.Range(0, 10);
            for (int i = 0; i < QuantityOfPlatform; i++)
            {
                float x = Random.Range(-RangeOfPlatforms, RangeOfPlatforms) * 10;
                float y = Random.Range(-RangeOfPlatforms, RangeOfPlatforms) * 10;
                GameObject platform = Instantiate(PlatformPrtefab, new Vector3(x, y, 0), Quaternion.identity);
                int RandomTrap = Random.Range(0, 10);
                if(RandomTrap == NeedToBeTrap)
                {
                    //Debug.Log("Trap Installed");
                    platform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                    platform.GetComponent<RandomizerHelper>().FirstPlatform = true;
                    platform.gameObject.tag = "Trap";
                }
            }
        }
    }
}
