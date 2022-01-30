using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour
{
    // Start is called before the first frame update
    private float timmyFootZpos = 0;
    private bool haveCoin=false;

    GameManager gameManager;

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        CreateCoin();
        CreateShit();
    }

    // Update is called once per frame
    void Update()
    {

        CreateAndDestroy();
    }

    private void CreateAndDestroy()
    {
        timmyFootZpos = GameObject.FindGameObjectsWithTag("Foot")[0].transform.position.z;
        if (timmyFootZpos - transform.position.z > 40.0f)
        {
            gameManager.InitiateStone();
            Destroy(gameObject);
        }
    }

    private void CreateCoin()
    {
        if ((int)Random.Range(0, 100) < gameManager.coinRate)
        {
            haveCoin=true;
            gameManager.CreateCoin(this.transform.position.z, this.GetComponent<BoxCollider>().size.z);
        }
    }
    private void CreateShit()
    {
        if (haveCoin)
        {
            return;
        }

        if ((int)Random.Range(0, 100) < gameManager.shitRate)
        {
           
            gameManager.CreateShit(this.transform.position.z, this.GetComponent<BoxCollider>().size.z);
        }
    }
}
