using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleMovement : MonoBehaviour
{
    public float speed = 2f;
    public GameObject drop;

    private float dropDis;
    private Vector2 startPos;

    private static float offset = 0.2f;
    private bool hasDropt;
    private Vector2 target;


    public void SetTarget(Vector2 tar)
    {
        startPos = transform.position;
        float disToTar = Vector2.Distance(transform.position, tar);
        dropDis = disToTar * Random.Range(0.10f, 0.90f);
        target = tar;
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
            return;

        transform.position = Vector2.MoveTowards(transform.position, target, Time.deltaTime * speed);
        float disToTar = Vector2.Distance(transform.position, target);
        if(disToTar <= 0.2f){
            Destroy(gameObject);
        }else if(!hasDropt)
        {
            if (disToTar <= dropDis + offset && disToTar >= dropDis - offset) { 
                DropObject();
            }
        }
    }

    void DropObject()
    {
        Debug.Log("DROP!!!");

        hasDropt = true;
        GameObject obj = Instantiate(drop);
        obj.transform.position = transform.position;

        //WIP TODO: ITEM etc quick fix
        HumanMagnet magnet = obj.GetComponent<HumanMagnet>();
        magnet.enabled = false;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0, 1f, 0.0f);
        Gizmos.DrawSphere(new Vector2(startPos.x + dropDis,startPos.y), offset);
        Gizmos.DrawLine(startPos, target);
    }

}
