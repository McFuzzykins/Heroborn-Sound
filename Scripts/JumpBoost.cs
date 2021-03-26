using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBoost : MonoBehaviour
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
            gameManager.JumpBoost = true;
            Destroy(this.transform.parent.gameObject);
            Debug.Log("To the moon and beyond! You got a jump boost!");

            gameManager.Items += 1;
        }
    }
}

