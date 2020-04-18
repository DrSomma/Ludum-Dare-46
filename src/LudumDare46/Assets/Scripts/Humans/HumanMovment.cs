using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanMovment : MonoBehaviour
{
    [Header("config")]
    public float speed = 1.0f;

    private Vector2 dir;
    private Vector2 dragStart;
    private HumanProperties myProbs;

    void Start()
    {
        //Calc init move dir
        calcDir(Random.Range(0,360));

        myProbs = GetComponent<HumanProperties>();
    }

    private void calcDir(float angle){
        dir = (Vector2)(Quaternion.Euler(0,0,angle) * Vector2.up);
        dir = dir * speed;
    }

    void Update() {
        transform.Translate(dir * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        //Check for border. Go in diff dir if on a wall 
        if (other.gameObject.CompareTag("WorldBorder"))
        {
            dir = -dir;
        }
    }
    
    //TODO: bessere Erkennung! Es sollte reichen wenn man in die Nähe klickt
   private void OnMouseDown() {
       if(myProbs.status == HealthStatusEnum.infected)
            return;
            
       dragStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
   }

   private void OnMouseUp() {
        if(myProbs.status == HealthStatusEnum.infected)
            return;
        
        Vector2 dragEnd = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        dir = (dragEnd - dragStart);
        dir = dir.normalized * speed;        
   }

}