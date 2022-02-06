using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroyer : MonoBehaviour
{
    // Start is called before the first frame update

    float timmyFootZpos = 0;
    public float farFromTimmy=40.0f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DestroyIfItsDone();
    }


    private void DestroyIfItsDone()
    {
        timmyFootZpos = GameObject.FindGameObjectsWithTag("Foot")[0].transform.position.z;
        if (timmyFootZpos - transform.position.z > farFromTimmy)
        {
            Destroy(gameObject);
        }
    }
}
