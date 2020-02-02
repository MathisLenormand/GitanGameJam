using ScriptableObjectArchitecture;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] private UIManager uiManager;

    public void OnSwipe(Vector3 swipe)
    {
        uiManager.OnRestart();
    }
}
