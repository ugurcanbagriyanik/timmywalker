using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    //scalez * 1.5 = posz
    //baslangic position = 1
    //her eklemede baslangic position = scale * 10* 1.5
    public GameObject[] stones;
    public GameObject[] backGrounds;
    private GameObject oldObject = null;
    private GameObject oldBackGround = null;

    float lastCheckPoint = 0;
    float lastGoldCheckpoint = 10;
    public GameObject coin;
    //public GameObject banana;//TODO: MUZ KABUĞU EKLENECEK
    public GameObject shit;

    public float coinRate=5.0f;
    public float shitRate=5.0f;

    public GameObject coinEffect;

    GameObject score_m;
    GameObject highScore;
    GameObject goldText;
    int initialScore = 0;
    //private List<GameObject> objectList = new List<GameObject>();
    void Start()
    {
        score_m = GameObject.FindGameObjectWithTag("Score_m");
        highScore = GameObject.FindGameObjectWithTag("HighScore");
        goldText = GameObject.FindGameObjectWithTag("GoldText");

        lastCheckPoint = PlayerPrefs.GetInt("lastCheckPoint") < 0 ? 0 : PlayerPrefs.GetInt("lastCheckPoint");

        for (int i = 0; i < 30; i++)
        {
            InitiateStone();
        }
        for (int i = 0; i < 5; i++)
        {
            InitiateBackground();
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateScore();
    }

    public void InitiateStone()
    {
        int index = (int)Random.Range(0f, stones.Length);
        var tempObject = stones[index];
        if (oldObject == null)
        {
            oldObject = Instantiate(tempObject, new Vector3(0, -0.054f, 0), Quaternion.identity);
            return;
        }

        oldObject = Instantiate(tempObject, oldObject.transform.position
        + new Vector3(0.0f, 0.0f, oldObject.GetComponent<BoxCollider>().size.z / 2 * oldObject.transform.localScale.z)
        + new Vector3(0.0f, 0.0f, tempObject.GetComponent<BoxCollider>().size.z / 2 * tempObject.transform.localScale.z), Quaternion.identity);
    }
    public void InitiateBackground()
    {
        int index = (int)Random.Range(0f, backGrounds.Length);
        var tempObject = backGrounds[index];
        if (oldBackGround == null)
        {
            oldBackGround = Instantiate(tempObject, new Vector3(-33.6f, -0.083f, 31.76f), Quaternion.Euler(0, -90, 0));
            return;
        }

        oldBackGround = Instantiate(tempObject, oldBackGround.transform.position
        + new Vector3(0.0f, 0.0f, oldBackGround.GetComponent<BoxCollider>().size.x / 2 * oldBackGround.transform.localScale.x)
        + new Vector3(0.0f, 0.0f, tempObject.GetComponent<BoxCollider>().size.x / 2 * tempObject.transform.localScale.x), Quaternion.Euler(0, -90, 0));
    }


    public void UpdateScore()
    {
        score_m.transform.position = new Vector3(score_m.transform.position.x, score_m.transform.position.y, (GameObject.FindGameObjectsWithTag("Foot")[0].transform.position.z + GameObject.FindGameObjectsWithTag("Foot")[1].transform.position.z) / 2 + lastCheckPoint + 2);
        initialScore = ((int)((score_m.transform.position.z - 2) * 0.75));
        score_m.GetComponent<TextMeshPro>().text = initialScore.ToString() + " m";

        GetHighScore();
        CheckForGold();
    }

    private void GetHighScore()
    {
        if (PlayerPrefs.GetInt("highScore") < initialScore)
        {
            PlayerPrefs.SetInt("highScore", initialScore);
        }
        highScore.GetComponent<Text>().text = "Best Score : " + PlayerPrefs.GetInt("highScore").ToString() + " m";
    }

    private void CheckForGold()
    {
        if (initialScore != 0 && lastGoldCheckpoint == initialScore)
        {
            lastGoldCheckpoint += 10;

            //PlayerPrefs.SetInt("gold", PlayerPrefs.GetInt("gold") + 1);
            MoreShit();
        }
        goldText.GetComponent<TextMeshPro>().text = PlayerPrefs.GetInt("gold").ToString();

    }

    public void AddCoin(){

        PlayerPrefs.SetInt("gold", PlayerPrefs.GetInt("gold") + 1);
        goldText.GetComponent<TextMeshPro>().text = PlayerPrefs.GetInt("gold").ToString();
    }

    public void CreateCoin(float center_z, float bound_z)
    {
        int r = (int)Random.Range(0, 10);
        float x = r < 5 ? 0.370f : -0.370f;

        Instantiate(coin, new Vector3(x, 2, Random.Range(center_z - (bound_z / 2), center_z + (bound_z / 2))), Quaternion.identity);
    }
    public void CreateShit(float center_z, float bound_z)
    {
        int r = (int)Random.Range(0, 10);
        float x = r < 5 ? 0.370f : -0.370f;

        Instantiate(shit, new Vector3(x, 1, Random.Range(center_z - (bound_z / 2), center_z + (bound_z / 2))), Quaternion.Euler(-90,0,0));
    }

    private void MoreShit(){
        shitRate+=0.1f;
    }
}
