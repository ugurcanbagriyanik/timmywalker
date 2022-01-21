using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour
{
    // Start is called before the first frame update
    private float timmyFootZpos=0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timmyFootZpos= GameObject.FindGameObjectsWithTag("Foot")[0].transform.position.z;
        if(timmyFootZpos-transform.position.z>20.0f)
        {
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().InitiateStone();
            Destroy(gameObject);
        }
        
    }
}
