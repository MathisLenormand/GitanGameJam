using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableObjectArchitecture;

public class StartScreen : MonoBehaviour
{
    [SerializeField] private GameEvent startGame;

    public void OnSwipe (Vector3 swipe)
    {
        startGame.Raise();
    }
}
