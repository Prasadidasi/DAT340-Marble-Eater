using UnityEngine.UI;
using UnityEngine;

public class DisplayEnemiesEaten : MonoBehaviour
{
    private Text enemiesEaten;
    void Start()
    {
        enemiesEaten = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        enemiesEaten.text = "Enemies Eaten : "+ Agent.Instance.KilledMarbles.ToString();
    }
}
