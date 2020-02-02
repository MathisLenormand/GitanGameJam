using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject startScreen;
    [SerializeField] private GameObject gameOverScreen;

    private void Start()
    {
        startScreen.SetActive(true);
        gameOverScreen.SetActive(false);
    }

    public void OnRestart ()
    {
        startScreen.SetActive(true);
        gameOverScreen.SetActive(false);
    }

    public void OnStart ()
    {
        startScreen.SetActive(false);
    }

    public void onEnd ()
    {
        gameOverScreen.SetActive(true);
    }
}
