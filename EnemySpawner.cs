using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] enemies; // Enenmies 정보를 리스트로 feed 함 

    [SerializeField]
    private GameObject boss;  

    private float[] arrPosX = {-2.2f, -1.1f, 0f, 1.1f, 2.2f}; // 적이 생성되는 5가지 위치

     [SerializeField]
    private float spawnInterval = 1.5f; 

    void Start()
    {
       StartEnemyRoutine(); 
    }

    // 코루틴 (메소드 안에서 시간을 정의할 수 있음 (몇 초 후에 이 동작을 해줘))

    void StartEnemyRoutine(){
        StartCoroutine("EnemyRoutine"); 
    }

    public void StopEnemyRoutine(){
        StopCoroutine("EnemyRoutine");
    }

    IEnumerator EnemyRoutine(){
        yield return new WaitForSeconds(3f); // 다음 동작 하기 전에 대기 (3초 대기 후 밑 라인 실행)
        
        float moveSpeed = 5f; 
        int enemyIndex = 0; 
        int spawnCount = 0; 

        while (true){
            foreach (float posX in arrPosX) // 배열의 element 에 직접 접근함 
            {
            SpawnEnemy(posX, enemyIndex, moveSpeed);
            }
            spawnCount++; 

            if (spawnCount % 10 ==0){ //10, 20, 30 ... 
                enemyIndex ++; 
                moveSpeed += 2; // 떨어지는 속도를 빠르게 하기 위해서 
            }
            
            if (enemyIndex >= enemies.Length){
                SpawnBoss(); 
                enemyIndex = 0; 
                moveSpeed = 5f; // 초기화 
            }

            yield return new WaitForSeconds (spawnInterval); 
        }
    }

    void SpawnEnemy(float posX, int index, float moveSpeed){
        Vector3 spawnPos = new Vector3(posX, transform.position.y, transform.position.z); 

         if (Random.Range(0,5) == 0){ // Random 숫자를 뽑아서 index 숫자를 증가시킴 
            index ++; 
        }

        if (index >= enemies.Length){ // 방어코드 (난이도 6 이상의 Object 가 생성되지 않도록)
            index = enemies.Length-1; 
        } 

        GameObject enemyObject = Instantiate(enemies[index], spawnPos, Quaternion.identity); 
        Enemy enemy = enemyObject.GetComponent <Enemy>(); 
        enemy.SetMoveSpeed(moveSpeed); // Enemy 에 있는 이동 속도를 바꾸어주는 작업 
    }

    void SpawnBoss(){
        Instantiate(boss, transform.position, Quaternion.identity); 
    }
}
