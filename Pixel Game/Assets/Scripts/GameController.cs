using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameController : MonoBehaviour
{   
    public int lastScore;
    public int totalScore;
    public int sceneLoadedInt;
    public Text scoreText;

    public GameObject gameOver;

    public static GameController instance;
    // Start is called before the first frame update
    void Start()
    {
        //Pegar cena atual
        Scene sceneLoaded = SceneManager.GetActiveScene();
        sceneLoadedInt = sceneLoaded.buildIndex;
        //Se for primeira cena, deletar score salvo 
        if(sceneLoadedInt == 0){
            PlayerPrefs.DeleteKey("LastScore");
            if(PlayerPrefs.HasKey("LastScore")){
                lastScore = PlayerPrefs.GetInt("LastScore");
            }
        //Senao tranferir score salvo pro score atual    
        }else{
            lastScore = PlayerPrefs.GetInt("LastScore");
            totalScore = lastScore;
            UpdateScoreText();
        }


        /* 
        */
        instance = this;
    }
    
    //Salvar score atual e mostrar na tela
    public void UpdateScoreText()
    {
        PlayerPrefs.SetInt("LastScore", totalScore);
        Debug.Log( PlayerPrefs.GetInt("LastScore"));
        scoreText.text = totalScore.ToString();
    }

    public void ShowGameOver()
    {
        gameOver.SetActive(true);
    }

    public void RestartGame(string lvlName)
    {
        SceneManager.LoadScene(lvlName);
    }
}
