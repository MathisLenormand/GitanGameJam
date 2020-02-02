using ScriptableObjectArchitecture;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] private GameEvent startGame;
    [SerializeField] private FloatReference score;

    [SerializeField] private Text text;

    public void OnSwipe(Vector3 swipe)
    {
        gameObject.SetActive(false);

        startGame.Raise();
    }

    public void RefreshText ()
    {
        text.text = Mathf.Round(score.Value * 100).ToString();
    }
}
