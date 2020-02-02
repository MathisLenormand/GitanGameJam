using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableObjectArchitecture;
using FMODUnity;

public class StartScreen : MonoBehaviour
{
    [SerializeField] private GameEvent startGame;

    private bool firstTime = true;

    public void OnSwipe (Vector3 swipe)
    {
        startGame.Raise();

        if (firstTime)
        {
            RuntimeManager.PlayOneShot("event:/MUSIC/MUSIC_Level", transform.position);

            firstTime = !firstTime;
        }

        RuntimeManager.PlayOneShot("event:/SD/SFX/SFX_Start_Game", transform.position);
    }
}
