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
    [SerializeField] private FloatReference width;
    [SerializeField] private FloatReference height;

    public void SetColliders ()
    {
        top.offset = new Vector2(0, height.Value / 2 + 0.5f);
        top.size = new Vector2(width.Value, 1);

        bottom.offset = new Vector2(0, -height.Value / 2 - 0.5f);
        bottom.size = new Vector2(width.Value, 1);

        right.offset = new Vector2(width.Value / 2 + 0.5f, 0);
        right.size = new Vector2(1, height.Value);

        left.offset = new Vector2(-width.Value / 2 - 0.5f, 0);
        left.size = new Vector2(1, height.Value);
    }
}
