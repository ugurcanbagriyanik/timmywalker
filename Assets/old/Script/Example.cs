using UnityEngine;

public enum LegEnum
{
    right = 0,
    left = 1
}

public class Example : MonoBehaviour
{
    public GameObject[] PlayerParts;
    public ConfigurableJoint[] JointParts;
    Vector3 COM;
    JointDrive Spring0;

    public bool isDebugMod = false;
    public float uprate = 0.0f;
    public float forwardRate = 0.0f;

    private LegEnum legTurn = LegEnum.right;
    private bool isPressed = false;

    private Vector3 oldPosition = Vector3.zero;

    private bool gameOver = false;

    private void Start()
    {
        legTurn = LegEnum.right;
        isPressed = false;
        gameOver = false;
    }

    private void Awake()
    {
        Physics.IgnoreCollision(PlayerParts[2].GetComponent<Collider>(), PlayerParts[4].GetComponent<Collider>(), true);
        Physics.IgnoreCollision(PlayerParts[3].GetComponent<Collider>(), PlayerParts[7].GetComponent<Collider>(), true);

        Spring0 = new JointDrive();
        Spring0.positionSpring = 0;
        Spring0.positionDamper = 0;
        Spring0.maximumForce = Mathf.Infinity;

    }

    private void Update()
    {
        HandleInput();

        PlayerParts[12].transform.position = Vector3.Lerp(PlayerParts[12].transform.position, PlayerParts[2].transform.position, 2 * Time.unscaledDeltaTime);


        Calculate_COM();

        PlayerParts[10].transform.position = COM;


        PlayerParts[11].transform.LookAt(PlayerParts[10].transform.position);


    }

    void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SetOldPosition();
            isPressed = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isPressed = false;
            GetDownTheLeg();
            ChangeLegTurn();
            //Debug.Log("bıraktı");
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Application.LoadLevel(0);
            gameOver = false;
        }

        if (Input.touches.Length > 1)
        {
            Application.LoadLevel(0);
            gameOver = false;
        }

    }


    void MoveLeg()
    {
        if (legTurn == LegEnum.right)
        {
            PlayerParts[6].transform.position += forwardRate * Vector3.forward + uprate * Vector3.up;
            return;
        }

        PlayerParts[9].transform.position += forwardRate * Vector3.forward + uprate * Vector3.up;
    }

    void GetDownTheLeg()
    {
        if (legTurn == LegEnum.right)
        {
            PlayerParts[6].transform.position = new Vector3(PlayerParts[6].transform.position.x + (PlayerParts[6].transform.position.x - oldPosition.x) * 0.1f, 0, PlayerParts[6].transform.position.z);
            return;
        }

        PlayerParts[9].transform.position = new Vector3(PlayerParts[9].transform.position.x + (PlayerParts[9].transform.position.x - oldPosition.x) * 0.1f, 0, PlayerParts[9].transform.position.z);
    }

    void SetOldPosition()
    {
        if (legTurn == LegEnum.right)
        {
            oldPosition = PlayerParts[6].transform.position;
            return;
        }

        oldPosition = PlayerParts[9].transform.position;
    }

    void ChangeLegTurn()
    {
        if (this.legTurn == LegEnum.right)
        {
            this.legTurn = LegEnum.left;
            return;
        }

        this.legTurn = LegEnum.right;
        return;
    }

    private void FixedUpdate()
    {

        if (isPressed)
        {
            MoveLeg();
        }

        if (!gameOver)
        {
            if (Mathf.Abs(Vector3.Distance(PlayerParts[9].transform.position, PlayerParts[6].transform.position)) > 2.8f)
            {
                // gameOver = true;
                GameOver();
                Debug.Log("GAME OVER");
            }
        }
    }

    public void GameOver()
    {
        if (gameOver || isDebugMod)
        {
            return;
        }
        gameOver = true;
        PlayerParts[9].GetComponent<Rigidbody>().isKinematic = false;
        PlayerParts[9].GetComponent<Rigidbody>().mass = 1;
        PlayerParts[6].GetComponent<Rigidbody>().isKinematic = false;
        PlayerParts[6].GetComponent<Rigidbody>().mass = 1;
        foreach (var item in JointParts)
        {
            item.angularXDrive = Spring0;
            item.angularYZDrive = Spring0;
        }
        if (legTurn == LegEnum.left)
        {
            PlayerParts[0].GetComponent<Rigidbody>().AddExplosionForce(6000, PlayerParts[6].transform.position, 200.0f, 20.0f, ForceMode.Force);
            return;
        }

        PlayerParts[0].GetComponent<Rigidbody>().AddExplosionForce(6000, PlayerParts[9].transform.position, 200.0f, 20.0f, ForceMode.Force);

    }

    void Calculate_COM()
    {
        COM = (JointParts[0].GetComponent<Rigidbody>().mass * JointParts[0].transform.position +
            JointParts[1].GetComponent<Rigidbody>().mass * JointParts[1].transform.position +
            JointParts[2].GetComponent<Rigidbody>().mass * JointParts[2].transform.position +
            JointParts[3].GetComponent<Rigidbody>().mass * JointParts[3].transform.position +
            JointParts[4].GetComponent<Rigidbody>().mass * JointParts[4].transform.position +
            JointParts[5].GetComponent<Rigidbody>().mass * JointParts[5].transform.position +
            JointParts[6].GetComponent<Rigidbody>().mass * JointParts[6].transform.position +
            JointParts[7].GetComponent<Rigidbody>().mass * JointParts[7].transform.position +
            JointParts[8].GetComponent<Rigidbody>().mass * JointParts[8].transform.position +
            JointParts[9].GetComponent<Rigidbody>().mass * JointParts[9].transform.position) /
            (JointParts[0].GetComponent<Rigidbody>().mass + JointParts[1].GetComponent<Rigidbody>().mass +
            JointParts[2].GetComponent<Rigidbody>().mass + JointParts[3].GetComponent<Rigidbody>().mass +
            JointParts[4].GetComponent<Rigidbody>().mass + JointParts[5].GetComponent<Rigidbody>().mass +
            JointParts[6].GetComponent<Rigidbody>().mass + JointParts[7].GetComponent<Rigidbody>().mass +
            JointParts[8].GetComponent<Rigidbody>().mass + JointParts[9].GetComponent<Rigidbody>().mass);
    }
}
