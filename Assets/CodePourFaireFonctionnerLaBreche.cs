using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodePourFaireFonctionnerLaBreche : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Salu");

        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            Debug.Log("Sal2u");
        }
    }
}
