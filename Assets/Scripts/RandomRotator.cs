using UnityEngine;
using System.Collections;

public class RandomRotator : MonoBehaviour {

    private Rigidbody rb;

    public float tumble;

    public GameObject explosions;

    public GameObject playExplosions;

    private int attacked = 0;

    public int scoreValue;
    private GameController gameController;


    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        rb.angularVelocity = Random.insideUnitSphere * tumble;

        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        if (gameController == null)
        {
            Debug.Log("Cannot find 'GameController' script");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Boundary") return;

        if (other.tag == "Player") {

            Instantiate(playExplosions, other.transform.position, other.transform.rotation);
            gameController.GameOver();
        }

        if (other.tag == "Shoot") {
            attacked++;
            if (attacked == 3)
            {
                Instantiate(explosions, transform.position, transform.rotation);
                Destroy(gameObject);
                //这里去调用更新的函数
                gameController.AddScore(scoreValue);
            }
        }

 
        Destroy(other.gameObject);

    }


}
