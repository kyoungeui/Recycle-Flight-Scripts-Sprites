using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 
using UnityEngine.SceneManagement; 

public class GameManager : MonoBehaviour
{
    // single turn 을 위한 객체 

    public static GameManager instance = null; 

    [SerializeField]
    private TextMeshProUGUI text; 

    [SerializeField]
    private GameObject gameOverPanel; 

    private int coin = 0; 

    [HideInInspector] // isGameOver 라는 변수는 public 이지만 inspector 에서 보여지지는 않는다 
    public bool isGameOver = false; // Game Over 를 판단하는 불리언 

    // start 이전에 불려짐 
    void Awake(){
        if (instance == null){
            instance = this; // GameManager 를 집어넣음 (single turn 이라는 디자인)
        }
    }

    public void IncreaseCoin(){
        coin ++; 
        text.SetText(coin.ToString()); 

        if (coin % 30 == 0){
            Player player = FindObjectOfType<Player>(); 
            if (player != null){
                player.Upgrade(); 
            }
        }
    }

    public void SetGameOver(){
        isGameOver = true; 
        EnemySpawner enemySpawner = FindObjectOfType< EnemySpawner>(); 
        if (enemySpawner != null){ // 오류 방지 if 문 
            enemySpawner.StopEnemyRoutine(); 
        }
        
        Invoke("ShowGameOverPanel", 1f); // 1초 후에 함수를 실행시킴 
    }

    void ShowGameOverPanel(){
    gameOverPanel.SetActive(true);}

    public void PlayAgain(){
        SceneManager.LoadScene("SampleScene"); // 괄호 안에 다시 불러오고 싶은 Scene 을 불러온다 
    }
}
