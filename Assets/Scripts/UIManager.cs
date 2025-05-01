using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI score;
    public TextMeshProUGUI time;
    public TextMeshProUGUI hTimeText;
    public TextMeshProUGUI hScoreText;
    private float timer;
    private float scored;
    private float hTime;
    private float hScore;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hTime = PlayerPrefs.GetFloat("HighTime", 0f);
        hScore = PlayerPrefs.GetFloat("HighScore",0f);
        hTimeText.text = "Highest Time: " + hTime.ToString("F3");
        hScoreText.text = "Highest Score: " + hScore;
    }

    // Update is called once per frame
    void Update()
    {
        timer+= Time.deltaTime;
        time.text="Time: "+timer.ToString("F3");
        if(timer>hTime) {
            hTime=timer;
            PlayerPrefs.SetFloat("HighTime", hTime);
            hTimeText.text="Highest Time: "+hTime.ToString("F3");
        }
        if(scored>hScore) {
            hScore=scored;
            PlayerPrefs.SetFloat("HighScore", hScore);
            hScoreText.text="Highest Score: "+hScore;

        }
        
        
    }

    public void AddScore(int amount){
        scored+=amount;
        score.text = "Score: "+scored;

    }

    public void RestartGame(){
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
