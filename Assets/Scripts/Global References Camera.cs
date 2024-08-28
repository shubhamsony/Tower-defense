using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public static class GlobalReferencesCamera
{
    public static StartupGlobalReferences startupScript;

    public static Transform EnemyParent;
    public static Transform Misc;
    public static TextMeshProUGUI Score;
    public static TextMeshProUGUI Price;
    public static Camera UICamera;

    public static Texture HPEmpty;
    public static Texture HPFull;
    public static RawImage[] HPHandler;

    public static RawImage[] EYEHandler;

    public static float bulletSpeed;

    private static int score = 0;
    private static int HP = -1;

    public static int ScoreBeforeEye = 5;
    private static int scoreTillEye = 0;
    public static int Eyes = -1;
    // Implemented towers = 1
    public static int SelectedTower = 0;

    public static GameObject GameOverScreen;
    public static GameObject WinScreen;

    public static void ResetValues()
    {
        startupScript.isGameOver = false;
        score = 0;
        HP = -1;
        Eyes = -1;
        scoreTillEye = 0;
        SelectedTower = 0;
        for (int i = 0; i < 3; i++)
            HPHandler[i].texture = HPFull;
        Score.text = (score).ToString();
    }

    public static void UpdateScore()
    {
        score++;
        Score.text = (score).ToString();
        scoreTillEye ++;

        if (scoreTillEye > ScoreBeforeEye)
        {
            scoreTillEye = 0;
            UpdateEyes(1);
        }
    }

    public static void UpdateEyes( int x )
    {
        Eyes += x;
        if (Eyes < -1)
            Eyes = -1;
        if (Eyes > 4)
            Eyes = 4;
        for (int i = 0; i < 5; i++)
        {
            if (Eyes >= i)
                EYEHandler[i].gameObject.SetActive(true);
            else
                EYEHandler[i].gameObject.SetActive(false);
        }
    }

    public static void TakeDamage()
    {
        if (HP != 2)
            HP++;
        HPHandler[HP].texture = HPEmpty;
        if (HP == 2)
        {
            GameOverHandler();
        }
    }
    static void GameOverHandler()
    {
        // Handler Gameover
        GameOverScreen.SetActive(true);
        startupScript.isGameOver = true;
    }

    public static void WinHandler()
    {
        WinScreen.SetActive(true);
        startupScript.isGameOver = true;
    }

    public static void switchSelectedTower(int id, int price)
    {
        switch (id)
        {
            case 0: UICamera.transform.position = new Vector3(0, 50, 0);
                break;
            case 1:
                UICamera.transform.position = new Vector3(2, 50, 0);
                break;
            case 2:
                UICamera.transform.position = new Vector3(4, 50, 0);
                break;
            case 3:
                UICamera.transform.position = new Vector3(6, 50, 0);
                break;
            default: UICamera.transform.position = new Vector3(-2, 0, 0);
                break;
        }

        Price.text = price.ToString();
    }
}
