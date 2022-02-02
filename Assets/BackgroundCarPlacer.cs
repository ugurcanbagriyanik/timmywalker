using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundCarPlacer : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] cars;
    public float defaultX = -3.41f;
    public float defaultY = -0.083f;
    public float[] defaultZList;
    void Start()
    {
        CreateCars();

    }

    // Update is called once per frame
    void Update()
    {

    }

    void CreateCars()
    {


        foreach (var item in defaultZList)
        {
            int index = (int)Random.Range(0f, cars.Length);
            var tempObject = cars[index];
            if ((int)Random.Range(0f, 100f) > 40)
            {
                Instantiate(tempObject, new Vector3(defaultX, defaultY, this.transform.position.z + item), Quaternion.identity);
            }
        }
    }


}
