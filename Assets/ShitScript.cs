using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShitScript : MonoBehaviour
{
    GameManager gameManager;

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
            GameObject.FindGameObjectWithTag("Timmy").GetComponent<Example>().GameOver();
            //Destroy(gameObject);
        }
    }

}
