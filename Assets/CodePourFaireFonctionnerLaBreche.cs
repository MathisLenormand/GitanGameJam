using ScriptableObjectArchitecture;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodePourFaireFonctionnerLaBreche : MonoBehaviour
{
    [SerializeField] private float mattervalue;
    [SerializeField] private float waterLossPerSeconds = 0.01f;
    [SerializeField] private FloatReference _waterHeight;
    [SerializeField] private FloatReference _waterLevel;

    private void Update()
    {
        _waterLevel.Value -= waterLossPerSeconds * Time.deltaTime;

        if (transform.position.y > _waterHeight.Value)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            //collision.gameObject.GetComponent<RepairBoy>().matterlevel += mattervalue;
            Destroy(gameObject);

            collision.GetComponent<RepairBoy>().CurrentMatter -= mattervalue;
        }
    }
}
