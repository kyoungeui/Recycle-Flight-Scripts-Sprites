using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private GameObject coin; 

    [SerializeField]
    float moveSpeed = 10f;

    private float minY = -7f; 

    [SerializeField]
    private float hp = 1f; 


    public void SetMoveSpeed(float moveSpeed){
        this.moveSpeed = moveSpeed; // this 가 없는 moveSpeed 는 float moveSpeed
        // this.moveSpeed 는 위에 있는 무브스피드 
    }

    void Update()
    {
        transform.position += Vector3.down * moveSpeed * Time.deltaTime;
        if (transform.position.y < minY){
            Destroy(gameObject); 
        }
    }

    private void OnTriggerEnter2D (Collider2D other) {
        //Debug.Log("Collision detected");
        
            Weapon weapon = other.gameObject.GetComponent<Weapon>(); // Declare 할 때 쓰는 문법 
            hp -= weapon.damage; 
            if (hp <=0){
                if (gameObject.tag == "Boss"){
                    GameManager.instance.SetGameOver(); 
                }

                Destroy(gameObject); 
                Instantiate(coin, transform.position, Quaternion.identity); 
            }
            Destroy(other.gameObject); 

    } }
