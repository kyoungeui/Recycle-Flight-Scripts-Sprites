using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 10f; 
    // Update is called once per frame
   
    public float damage = 1f; 

     void Start()
   {
        Destroy(gameObject, 1f); // 1초 뒤에 gameObject 를 없앤다는 예약문 
   }

    void Update()
    {
        transform.position += Vector3.up * moveSpeed * Time.deltaTime;
    }
}
