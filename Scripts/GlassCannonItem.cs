using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassCannonItem : MonoBehaviour
{
    public GameBehavior gameManager;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameBehavior>();
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            gameManager.Timer = 15f;
            gameManager.GlassCannon = true;
            Destroy(this.transform.parent.gameObject);
            Debug.Log("1 hit 1 kill mode activated: Good Luck!");

            gameManager.Items += 1;
        }
    }
}

