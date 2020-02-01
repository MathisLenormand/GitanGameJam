using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleScript : MonoBehaviour
{

    public float mattervalue;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //collision.gameObject.GetComponent<RepairBoy>().matterlevel += mattervalue;
            Destroy(gameObject);

            collision.GetComponent<RepairBoy>().CurrentMatter += mattervalue;
        }
    }
}
