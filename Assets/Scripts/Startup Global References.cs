using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartupGlobalReferences : MonoBehaviour
{
    [SerializeField] private int startingEyes = 2;

    [SerializeField] private Transform EnemyParent;
    [SerializeField] private Transform Misc;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private TextMeshProUGUI Score;
    [SerializeField] private TextMeshProUGUI Price;
    [SerializeField] private Camera UICamera;

    [SerializeField] private Texture HPEmpty;
    [SerializeField] private Texture HPFull;
    [SerializeField] private RawImage[] HPHandler;

    [SerializeField] private RawImage[] EYEHandler;

    [SerializeField] private int ScoreBeforeEye = 5;

    [SerializeField] private GameObject GameOverScreen;
    [SerializeField] private GameObject WinScreen;

    public bool isGameOver = false;
    public bool noMoreEnemies = false;

    void Awake()
    {
        UpdateReferences();
    }

    void UpdateReferences()
    {
        GlobalReferencesCamera.startupScript = transform.GetComponent<StartupGlobalReferences>();

        GlobalReferencesCamera.EnemyParent = EnemyParent;
        GlobalReferencesCamera.Misc = Misc;
        GlobalReferencesCamera.Score = Score;
        GlobalReferencesCamera.Price = Price;
        GlobalReferencesCamera.UICamera = UICamera;

        GlobalReferencesCamera.HPEmpty = HPEmpty;
        GlobalReferencesCamera.HPFull = HPFull;
        GlobalReferencesCamera.HPHandler = HPHandler;

        GlobalReferencesCamera.EYEHandler = EYEHandler;

        GlobalReferencesCamera.ScoreBeforeEye = ScoreBeforeEye;

        GlobalReferencesCamera.ResetValues();
        GlobalReferencesCamera.UpdateEyes(startingEyes);
        GlobalReferencesCamera.bulletSpeed = bulletSpeed;

        GlobalReferencesCamera.GameOverScreen = GameOverScreen;
        GlobalReferencesCamera.WinScreen = WinScreen;

    }

    private void FixedUpdate()
    {
        if (isGameOver)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene("level 1");
            }
        }

        if (EnemyParent.GetComponentInChildren<EnemyMove>() == null && noMoreEnemies)
            GlobalReferencesCamera.WinHandler();
    }
}
