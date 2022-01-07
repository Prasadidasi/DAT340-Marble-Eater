using UnityEngine.UI;
using UnityEngine;

public class DisplayTimer : MonoBehaviour
{
    private Text enemiesEaten;
    private int timer = 0;
    void Start()
    {
        enemiesEaten = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
        timer = Agent.Instance.GameStartTimer;
       // Debug.Log(timer);
        if (timer > 0)
            enemiesEaten.text = "GAME STARTS IN " + timer.ToString();
        else { 
            enemiesEaten.text = "";
            Agent.Instance.GameStartTimer = 0;
        }
            
    }

}
