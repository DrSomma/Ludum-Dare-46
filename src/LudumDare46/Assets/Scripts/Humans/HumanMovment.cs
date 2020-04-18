using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanMovment : MonoBehaviour
{
    [Header("config")]
    public float speed = 1.0f;

    private Rigidbody2D rigidbody;
    private Vector2 dir;

    private Vector2 dragStart;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        //Give rnd force
        calcDir(Random.Range(0,360));

    }

    private void calcDir(float angle){
        dir = (Vector2)(Quaternion.Euler(0,0,angle) * Vector2.up);
        dir = dir * speed;
    }

    void Update() {
        transform.Translate(dir * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("WorldBorder"))
        {
            dir = -dir;
        }
    }

    //TODO: bessere Erkennung! Es sollte reichen wenn man in die Nähe klickt
   private void OnMouseDown() {
       dragStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
   }

   private void OnMouseUp() {
        Vector2 dragEnd = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        dir = (dragEnd - dragStart);
        dir = dir.normalized;        
   }

}