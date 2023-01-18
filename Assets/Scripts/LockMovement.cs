using UnityEngine;

public class LockMovement : MonoBehaviour
{
    [SerializeField] float direction, topSpeed, incrementRate; 
    int incrementStep;
    LogicScript ls;
    public bool isPlaying;

    void Start()
    {
        direction = 65f;
        topSpeed = 210f;
        incrementRate = 13f;
        incrementStep = 4;
        ls = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, direction * Time.deltaTime), Space.Self);

        if (Input.GetKeyDown(KeyCode.Space))
        {

            if (direction < topSpeed && direction > 0 && ls.score % incrementStep == 0)
                direction += incrementRate;
            else if (direction > -topSpeed && direction < 0 && ls.score % incrementStep == 0)
                direction -= incrementRate;

            direction *= -1;
            // Debug.Log(direction);
        }

    }

    public void StopLock()
    {
        direction = 0;
    }
}
