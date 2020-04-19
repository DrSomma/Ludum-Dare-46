using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleMovement : MonoBehaviour
{
    public float speed = 2f;
    public GameObject drop;

    private float dropPoint;
    private Vector2 startPos;

    private static float offset = 0.2f;
    private bool hasDropt;
    private Vector2 target;

    private GameObject dropObj;
    private Vector2 dropTarget;
    public float dropSpeed = 1.3f;
    public float dropTravelDis = 5f;


    public void SetTarget(Vector2 tar)
    {
        startPos = transform.position;
        float disToTar = Vector2.Distance(transform.position, tar);
        dropPoint = disToTar * Random.Range(0.10f, 0.90f);
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
            if (disToTar <= dropPoint + offset && disToTar >= dropPoint - offset) { 
                DropObject();
            }
        }
    }

    void DropObject()
    {
        Debug.Log("DROP!!!");

        hasDropt = true;
        dropObj = Instantiate(drop);
        dropObj.transform.position = transform.position;

        //for size animation
        dropObj.transform.localScale = Vector2.one * 0.5f;

        //WIP TODO: ITEM etc quick fix
        HumanMagnet magnet = dropObj.GetComponent<HumanMagnet>();
        magnet.enabled = false;

        //target pos for drop
        dropTarget = new Vector2(transform.position.x, transform.position.y - dropTravelDis);

        //start drop movement
        StartCoroutine(MoveDropObject(dropObj, dropTarget, dropSpeed));
    }

    public IEnumerator MoveDropObject(GameObject drop, Vector2 posTo, float speed)
    {
        float elapsedTime = 0;
        Vector3 startingScale = drop.transform.localScale;
        Vector3 startPos = drop.transform.position;
        Vector2 scaleTo = Vector2.one;
        while (elapsedTime < speed)
        {
            drop.transform.localScale = Vector3.Lerp(startingScale, scaleTo, (elapsedTime / speed));
            drop.transform.position = Vector3.Lerp(startPos, posTo, (elapsedTime / speed));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        drop.transform.localScale = scaleTo;
        drop.transform.position = posTo;

        //WIP TODO: ITEM etc quick fix
        HumanMagnet magnet = dropObj.GetComponent<HumanMagnet>();
        magnet.enabled = true;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0, 1f, 0.0f);
        Gizmos.DrawSphere(new Vector2(startPos.x + dropPoint,startPos.y), offset);
        Gizmos.DrawLine(startPos, target);
    }

}
