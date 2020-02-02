using ScriptableObjectArchitecture;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] private GameEvent startGame;

    public void OnSwipe(Vector3 swipe)
    {
        gameObject.SetActive(false);

        startGame.Raise();
    }
}
