using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{

    public GameObject titleScreen;
    public TMP_Text januszText;
    public TMP_Text grazynaText;

    private PlayerController player1Controller;
    private PlayerController player2Controller;

    private GameOver gameOverPlayer1;
    private GameOver gameOverPlayer2;

    private List<Brick> bricks = new List<Brick>();


    private AudioSource musicAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        gameOverPlayer1 = GameObject.Find("GameOver1").GetComponent<GameOver>();
        gameOverPlayer2 = GameObject.Find("GameOver2").GetComponent<GameOver>();

        player1Controller = GameObject.Find("Player1").GetComponent<PlayerController>();
        player2Controller = GameObject.Find("Player2").GetComponent<PlayerController>();

        musicAudioSource = GameObject.Find("Main Camera").GetComponent<AudioSource>();

        foreach(GameObject brickGameObject in GameObject.FindGameObjectsWithTag("Brick"))
            bricks.Add(brickGameObject.GetComponent<Brick>());

        ChangeSpeedToZero();
        player1Controller.FreezMovment();
        player2Controller.FreezMovment();
    }

    // Update is called once per frame
    void Update()
    {
        foreach(Brick brick in bricks)
        {
            if(brick.isFalling)
            {
                player1Controller.FreezMovment();
                player2Controller.FreezMovment();
                ChangeSpeedToZero();
            }
        }
        
        if(gameOverPlayer1.gameOver == true)
        {
            januszText.SetText("Janusz LOST");
            grazynaText.SetText("Grazyna WON");
            januszText.gameObject.SetActive(true);
            grazynaText.gameObject.SetActive(true);
        }
        else if(gameOverPlayer2.gameOver == true)
        {
            januszText.SetText("Janusz WON");
            grazynaText.SetText("Grazyna LOST");
            januszText.gameObject.SetActive(true);
            grazynaText.gameObject.SetActive(true);
        }
    }

    public void StartGame()
    {
        titleScreen.SetActive(false);
        player1Controller.EnableAttack();
        player2Controller.EnableAttack();
        ChangeSpeed();
        InvokeRepeating("ChangeSpeed", 0, 10f);
    }

    void ChangeSpeed()
    { 
        int level = Random.Range(1, 4);
        musicAudioSource.pitch = 1 + level / 20.0f;
        player1Controller.ChangeSpeed(level);
        player2Controller.ChangeSpeed(level);
    }
    void ChangeSpeedToZero()
    {
        musicAudioSource.pitch = 1 + 1 / 20.0f;
        player1Controller.ChangeSpeed(0);
        player2Controller.ChangeSpeed(0);
    }
}
