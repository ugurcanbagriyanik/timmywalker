using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TWHelpers;
using System.Linq;

[System.Serializable]
public class ObjectPositioner
{
    public Vector3 posiblePosition;
    public Vector3 posibleRotation;
}

[System.Serializable]
public class GroupPlacer
{
    [Tooltip("Aşağıdaki position ve rotation'a denk gelebilecek object listesi")]
    public List<GameObject> gameObjectGroup;

    [Tooltip("Yukarıdaki objelerin olası position ve rotation listesi")]
    public List<ObjectPositioner> posiblePositions;

    [Tooltip("objeler arasındaki minimum uzaklık")]
    public float minDistance;

    [Tooltip("aynı anda oluşabilecek maks obje sayısı")]
    public int maxSize;

    [Tooltip("her denemede obje oluşturma olasılığı (0-100)")]
    public int probability;

    [Tooltip("aynı objeler oluşsun mu?")]
    public bool dublicateObjects;


}
public class BackgroundCarPlacer : MonoBehaviour
{
    [SerializeField]
    public List<GroupPlacer> groupPlacerConfigs;



    private List<ObjectPositioner> usedPositions = new List<ObjectPositioner>();
    private List<GameObject> usedObjects = new List<GameObject>();

    void Start()
    {
        PlaceObjects();

    }

    void Update()
    {

    }

    void PlaceObjects()
    {

        foreach (var gp in groupPlacerConfigs)
        {
            this.usedObjects = new List<GameObject>();
            for (int i = 0; i < Mathf.Min(gp.maxSize, gp.gameObjectGroup.Count); i++)
            {
                GameObject futureObject = null;

                if (gp.dublicateObjects)
                {
                    futureObject = gp.gameObjectGroup.GetRandomElement();

                    var posibleP = gp.posiblePositions.Except(usedPositions).Where(x => usedPositions.Where(l => Vector3.Distance(l.posiblePosition, x.posiblePosition) <= gp.minDistance).FirstOrDefault() == null).ToList().GetRandomElement();
                    if (Random.Range(0, 100) <= gp.probability)
                    {
                        this.usedPositions.Add(posibleP);
                        Instantiate(futureObject, this.transform.position + posibleP.posiblePosition, Quaternion.Euler(posibleP.posibleRotation.x, posibleP.posibleRotation.y, posibleP.posibleRotation.z));
                    }

                }
                else
                {
                    futureObject = gp.gameObjectGroup.Except(usedObjects).ToList().GetRandomElement();

                    var posibleP = gp.posiblePositions.Except(usedPositions).Where(x => usedPositions.Where(l => Vector3.Distance(l.posiblePosition, x.posiblePosition) <= gp.minDistance).FirstOrDefault() == null).ToList().GetRandomElement();
                    if (Random.Range(0, 100) <= gp.probability && futureObject != null)
                    {
                        this.usedPositions.Add(posibleP);
                        this.usedObjects.Add(futureObject);
                        Instantiate(futureObject, this.transform.position + posibleP.posiblePosition, Quaternion.Euler(posibleP.posibleRotation.x, posibleP.posibleRotation.y, posibleP.posibleRotation.z));
                    }
                }
            }

        }

    }



}
