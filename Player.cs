using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{   
    [SerializeField]
    private float moveSpeed;
    
    [SerializeField]
    private GameObject[] weapons; // SerializeField 를 통해 Weapon에 접근함 
    private int weaponIndex = 0; 
    [SerializeField]
    private Transform shootTransform; // shootTransform에서 값을 받음 

    [SerializeField]
    private float shootInterval = 0.05f; 

    private float lastShotTime = 0f; 

    // Update is called once per frame
    void Update()
    {
        /*float horizontalInput = Input.GetAxisRaw("Horizontal"); 
        Vector3 moveTo = new Vector3(horizontalInput, 0f, 0f); 
        transform.position += moveTo * moveSpeed * Time.deltaTime;*/

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); 
        float toX = Mathf.Clamp(mousePos.x, -2.35f, 2.35f); 
        transform.position = new Vector3(toX, transform.position.y, transform.position.z);

        if (GameManager.instance.isGameOver == false){
            Shoot();}
    }

    void Shoot(){ // Object Original / Vector / Quaterion Rotation) 
        if (Time.time - lastShotTime > shootInterval){
        Instantiate(weapons[weaponIndex], shootTransform.position, Quaternion.identity);
        lastShotTime = Time.time;}
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Boss"){
            GameManager.instance.SetGameOver(); 
            Destroy(gameObject); 
        }
        else if (other.gameObject.tag == "Coin"){
            GameManager.instance.IncreaseCoin(); // single turn 을 사용하면 바로 메소드가 호출 가능하다 
            Destroy(other.gameObject); 
        }
    }

    public void Upgrade(){
        weaponIndex++; 
        if (weaponIndex >= weapons.Length){
            weaponIndex = weapons.Length - 1; 
        }
    }
}
