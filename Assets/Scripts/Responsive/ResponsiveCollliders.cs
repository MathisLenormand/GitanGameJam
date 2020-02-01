using ScriptableObjectArchitecture;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResponsiveCollliders : MonoBehaviour
{
    [Header("Colliders")]
    [SerializeField] private BoxCollider2D top;
    [SerializeField] private BoxCollider2D bottom;
    [SerializeField] private BoxCollider2D left;
    [SerializeField] private BoxCollider2D right;

    [Header("Screens infos")]
    [SerializeField] private Vector3Reference topLeft;
    [SerializeField] private Vector3Reference bottomRight;

    private void Start()
    {
        float width = topLeft.Value.x - bottomRight.Value.x;
        float height = topLeft.Value.y - bottomRight.Value.y;

        top.offset = new Vector2(0, height / 2 + 0.5f);
        top.size = new Vector2(width , 1);

        bottom.offset = new Vector2(0,- height / 2 - 0.5f);
        bottom.size = new Vector2(width, 1);

        right.offset = new Vector2(width / 2 + 0.5f, 0);
        right.size = new Vector2(1, height);

        left.offset = new Vector2(- width / 2 - 0.5f, 0);
        left.size = new Vector2(1, height);
    }
}
