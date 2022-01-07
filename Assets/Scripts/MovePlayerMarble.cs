using System.Runtime.CompilerServices;
using UnityEngine;

public class MovePlayerMarble : MonoBehaviour
{
    public float maxPlayerSize = 4;
    public float growthRate = 0.01f;
    public Vector3 initialSize;

    private Vector3 direction = new Vector3(0, 0, 0);
    private bool playerStatus = false;
    [SerializeField] private int lives = 5;
    private float deathTimer;
    [SerializeField] private bool enableHitEffect = true;
    [SerializeField] private GameObject hitEffect;
    void Start()
    {
        Invoke("NotifyScaleChange", 0.0f);
        //NotifyScaleChange();
        NotifyLives();
        deathTimer = 3;
        initialSize = transform.localScale;
    }

    private void Update()
    {
        if (playerStatus && lives > 0)
        {
            if (deathTimer > 0)
            {
                deathTimer -= Time.deltaTime;
            }
            else
            {
                Debug.Log("Player is Back!");
                deathTimer = 0;
                playerStatus = false; //player is Alive
                gameObject.GetComponent<MeshRenderer>().enabled = true;
                NotifyPlayerDeath();
            }
        }
        else if (lives <= 0)
        {
            Debug.Log("Game Over!"); //to be properly implemented later
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        direction = direction * -1;

        if (!other.gameObject.CompareTag("Marble")) return;

        if (!isGameStart()) return;

        if (transform.localScale.x > other.transform.localScale.x)
        {
            Eat(other.gameObject);
            other.gameObject.GetComponent<MarbleController>().Respawn(other.gameObject);
            NotifyScaleChange();
            GetComponent<Rigidbody>().mass = (GetComponent<Rigidbody>().mass + other.gameObject.GetComponent<Rigidbody>().mass * 0.33f);
        }
    }
    private void Eat(GameObject marble)
    {
        PlayHitEffect(marble.transform.position);
        marble.gameObject.SetActive(false);

        //Mix the scales
        float newScale = transform.localScale.x + Mathf.Abs((marble.transform.localScale.x) * growthRate);
        if (newScale > maxPlayerSize) newScale = maxPlayerSize;
        transform.localScale = new Vector3(newScale, newScale, newScale);
        marble.GetComponent<MarbleController>().OnPlayerMarbleScaleChange(newScale);
        //Debug.Log(marble.gameObject.name + " is Destroyed");

        //Mix the mass
        GetComponent<Rigidbody>().mass = (GetComponent<Rigidbody>().mass + marble.gameObject.GetComponent<Rigidbody>().mass * 0.33f);

        //Notify the Delegate
        NotifyScaleChange();
        Agent.Instance.KilledMarbles++;

    }

    //call this function when player dies
    public void handlePlayerDeath()
    {
        playerStatus = true;
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        deathTimer = 3;
        lives--;
        NotifyLives();
        NotifyPlayerDeath();
    }
    
    // Subject notifies the Agent when scale changes.
    // Agent forwards the notification to Particle System
    // then Particle System calls Observers' event function
    private void NotifyScaleChange()
    {
        if (Agent.Instance.OnPlayerScaleChangeEvent != null)
        {
            Agent.Instance.PlayerMarbleScale = transform.localScale.x;
            Agent.Instance.OnPlayerScaleChangeEvent(Agent.Instance.PlayerMarbleScale);
        }
    }

    private void NotifyLives()
    {
        Agent.Instance.PlayerMarbleLives = lives;
    }

    private void NotifyPlayerDeath()
    {
        if (playerStatus)
            Agent.Instance.playerStatus = -1;
        else
            Agent.Instance.playerStatus = 1;
    }

    private bool isGameStart()
    {
        if (Agent.Instance.GameStartTimer <= 0)
            return true;
        else
            return false;
    }

    private void PlayHitEffect(Vector3 position)
    {
        if (!enableHitEffect) return;
        GameObject effect = Instantiate(hitEffect, position, Quaternion.identity);
        effect.GetComponent<ParticleSystem>().Play();
        Destroy(effect, 3.0f);
    }
}
