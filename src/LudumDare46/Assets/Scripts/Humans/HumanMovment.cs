using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanMovment : MonoBehaviour
{
    [Header("config")]
    public float normalSpeed = 0.5f;
    public float attractSpeed = 2f;
    public float lockFreeWillTime = 2f;
    

    private Vector2 dir;
    private Vector2 dragStart;
    private HumanProperties myProbs;
    private Transform attractTarget;
    private bool hasFreeWill = true;
    private float speed;

    void Start()
    {
        speed = normalSpeed;

        //Calc init move dir
        calcDir(Random.Range(0,360));

        myProbs = GetComponent<HumanProperties>();
    }

    private void calcDir(float angle){
        dir = (Vector2)(Quaternion.Euler(0,0,angle) * Vector2.up);
    }

    void Update() {
        if(hasFreeWill && attractTarget != null){
            transform.position = Vector2.MoveTowards(transform.position,attractTarget.position,Time.deltaTime*attractSpeed);
        }else{
            transform.Translate(dir * speed * Time.deltaTime);    
        }
    }

    public void setTarget(Transform target){
        attractTarget = target;
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

        //do nothing if distance is to smale
        if (Vector2.Distance(dragEnd, dragStart) <= 0.001f)
            return;

        dir = (dragEnd - dragStart);
        dir = dir.normalized;
        Debug.Log(dir);
        StartCoroutine("LockFreeWill");
   }

   IEnumerable LockFreeWill(){
       hasFreeWill = false;
       yield return new WaitForSeconds(lockFreeWillTime);
       hasFreeWill = true;
   }

}