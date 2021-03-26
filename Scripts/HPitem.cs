using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPitem : MonoBehaviour
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
            gameManager.HP = 10;
            gameManager.HPBoost = true;
            Destroy(this.transform.parent.gameObject);
            Debug.Log("If your Health Boost lasts longer than 4 hours, please consult your doctor.");

            gameManager.Items += 1;
        }
    }
}

