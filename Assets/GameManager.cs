using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    //scalez * 1.5 = posz
    //baslangic position = 1
    //her eklemede baslangic position = scale * 10* 1.5
    public GameObject[] stones;
    private GameObject oldObject=null;

    //private List<GameObject> objectList = new List<GameObject>();
    void Start()
    {
        for (int i = 0; i < 30; i++)
        {
            InitiateStone();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void InitiateStone()
    {
        int index = (int)Random.Range(0f, stones.Length);
        var tempObject = stones[index];
        if (oldObject==null)
        {
            oldObject= Instantiate(tempObject, new Vector3(0, -0.054f, 0), Quaternion.identity);
            return;
        }
        
        oldObject= Instantiate(tempObject,  oldObject.transform.position
        +new Vector3(0.0f, 0.0f,oldObject.GetComponent<BoxCollider>().size.z/2*oldObject.transform.localScale.z)
        +new Vector3(0.0f, 0.0f,tempObject.GetComponent<BoxCollider>().size.z/2*tempObject.transform.localScale.z), Quaternion.identity);
    }
}
