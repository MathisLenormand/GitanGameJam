using FMODUnity;
using UnityEngine;

public class CollectibleScript : MonoBehaviour
{

    public float mattervalue;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            RuntimeManager.PlayOneShot("event:/SD/SFX/SFX_Pick", transform.position);

            //collision.gameObject.GetComponent<RepairBoy>().matterlevel += mattervalue;
            Destroy(gameObject);

            collision.GetComponent<RepairBoy>().CurrentMatter += mattervalue;
        }
    }
}
