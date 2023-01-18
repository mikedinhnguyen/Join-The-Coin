using UnityEngine;
using UnityEngine.SceneManagement;
using FirstGearGames.SmoothCameraShaker;
public class LockHoleCollision : MonoBehaviour
{
    LockHolePlacement lhp;
    LogicScript ls;
    LockMovement lm;
    public ParticleSystem ps;
    int scoreIncrement;
    bool isTouching, gameIsOver;
    public GameObject gameOver;
    public GameObject timeCompleted;
    public ShakeData sd;

    // Start is called before the first frame update
    void Start()
    {
        lhp = GameObject.FindGameObjectWithTag("LockHole").GetComponent<LockHolePlacement>();
        ls = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        lm = GameObject.FindGameObjectWithTag("Lock").GetComponent<LockMovement>();
        scoreIncrement = 1;
        gameIsOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isTouching && !gameIsOver)
        {
            ParticlePlay();

            // tweaking max angle of rotation for lock placement when player reaches a certain level
            if (ls.score == 32)
                ls.SetColors("purple", "hardPurple");

            if (ls.score == 31)
            {
                ls.IncreaseText();
                ls.SetColor("purple");
                lhp.minAngle = 60;
                lhp.maxAngle = 230;
            }
            
            if (ls.score == 22)
                ls.SetColors("green", "hardGreen");

            if (ls.score == 21)
            {
                ls.SetColor("green");
                ls.IncreaseText();
                lhp.minAngle = 60;
                lhp.maxAngle = 180;
            }

            if (ls.score == 12)
                ls.SetColors("orange", "hardOrange");

            if (ls.score == 11) 
            {
                ls.SetColor("orange");
                ls.IncreaseText();
                lhp.minAngle = 50;
                lhp.maxAngle = 130;
            }

            if (ls.score == 7)
                ls.SetColors("hardRed", "harderRed");
            if (ls.score == 6)
            {
                ls.SetColor("hardRed");
                ls.IncreaseText();
                lhp.minAngle = 35;
                lhp.maxAngle = 90;
            }

            ls.DecreaseScore(scoreIncrement);

            if (ls.score == 0)
            {
                gameOver.SetActive(true);
                timeCompleted.SetActive(true);

                lm.StopLock();
                ls.SetColor("ogBlue");
                ls.SetColors("ogBlue", "hardBlue");
                ls.YouWinText();
                ls.ConfettiParty();
                ls.PlayWinSound();
                gameIsOver = true;
            } else
            {
                lhp.ChangeLocks();
            }
            
        } else if (Input.GetKeyDown(KeyCode.Space) && !isTouching && !gameIsOver)
        {
            gameOver.SetActive(true);
            lm.StopLock();
            ls.PlayLoseSound();
            ls.SetColor("softRed");
            ls.SetColors("hardRed", "harderRed");
            gameIsOver = true;
            CameraShakerHandler.Shake(sd);
        } else if (gameIsOver && Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(1);
        }
    }

    void ParticlePlay()
    {
        ps.transform.position = new Vector3(transform.position.x, transform.position.y, -2.5f);
        ps.Play();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Debug.Log("enter"); 2D collision sometimes doesn't work???
        isTouching = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Debug.Log("exit");
        isTouching = false;
    }

}
