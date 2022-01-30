using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    GameManager gameManager;

    float timmyFootZpos = 0;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Foot")
        {

            gameManager.AddCoin();
            Instantiate(gameManager.coinEffect, this.transform.position, Quaternion.Euler(90, 0, 0));
            Destroy(gameObject);

        }
    }

    private void DestroyIfItsDone()
    {
        timmyFootZpos = GameObject.FindGameObjectsWithTag("Foot")[0].transform.position.z;
        if (timmyFootZpos - transform.position.z > 40.0f)
        {
            gameManager.InitiateStone();
            Destroy(gameObject);
        }
    }
}
