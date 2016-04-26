using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    public GameObject[] asteroids;

    public Vector3 spawnValues;

    public int waveRate;

    public int spanRate;

    public int asteroidCount;

    public int sleepTime;




    public GameObject scoreObeject;
    public GameObject restartObeject;
    public GameObject gameOverObeject;

    private GUIText scoreText;
    private GUIText restartText;
    private GUIText gameOverText;

    private bool gameOver = false;
    private bool restart = false;

    private int score = 0;


    // Use this for initialization
    void Start()
    {

        if (scoreObeject != null)
        {
            scoreText = scoreObeject.GetComponent<GUIText>();
            if (scoreText == null) Debug.Log("there is no found scoreText");
            UpdateScore();

        }
        if (restartObeject != null)
        {
            restartText = restartObeject.GetComponent<GUIText>();
            if (restartText == null) Debug.Log("there is no found restartText");
            restartText.text = "";
        }
        if (gameOverObeject != null)
        {
            gameOverText = gameOverObeject.GetComponent<GUIText>();
            if (gameOverText == null) Debug.Log("there is no found gameOverText");
            gameOverText.text = "";
        }



        StartCoroutine(SpawnWaves());
    }

    // Update is called every frame, if the MonoBehaviour is enabled
    public void Update()
    {
        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }


    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(sleepTime);
        while (true)
        {
            for (int i = 0; i < asteroidCount; i++)
            {
                GameObject asteroid = asteroids[Random.Range(0, asteroids.Length)];
                Vector3 v3 = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion q = Quaternion.identity;
                Instantiate(asteroid , v3, q);
                yield return new WaitForSeconds(spanRate);

            }
            yield return new WaitForSeconds(waveRate);

            if (gameOver)
            {
                RestartGame();
                break;
            }
        }

    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }

    public void GameOver()
    {
        gameOver = true;
        gameOverText.text = "我承认我赢了";
    }

    public void RestartGame()
    {
        restartText.text = "按R代表畅辉是你爹";
        restart = true;
    }

}
