using UnityEngine.UI;
using UnityEngine;

public class DisplayPlayerLives : MonoBehaviour
{
    private Text enemiesEaten;
    void Start()
    {
        enemiesEaten = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        enemiesEaten.text = "Remaining Lives: " + Agent.Instance.PlayerMarbleLives.ToString();
    }
}
