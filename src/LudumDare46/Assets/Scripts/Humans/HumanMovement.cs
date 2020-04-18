using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanMovement : MonoBehaviour
{
    [Header("config")]
    public float normalSpeed = 0.5f;
    public float attractSpeed = 2f;
    public float lockFreeWillTime = 2f;
    public GameObject Highlighter;
    public GameObject DirectionArrow;

    private Vector2 dir;
    private Vector2 dragStart;
    private HumanProperties myProbs;

    public bool currentlySelected; // Bin ich selektiert?
    private bool inMovement; // Bin ich gerade in einer Bewegeung? - Maus wurde geklickt

    private Transform attractTarget;
    private bool hasFreeWill = true;
    private float speed;

    void Start()
    {
        speed = normalSpeed;

        //Calc init move dir
        calcDir(Random.Range(0, 360));

        myProbs = GetComponent<HumanProperties>();

        Highlighter.SetActive(false);
        DirectionArrow.SetActive(false);
    }

    private void calcDir(float angle)
    {
        dir = (Vector2)(Quaternion.Euler(0, 0, angle) * Vector2.up);
    }

    void Update()
    {
        if (hasFreeWill && attractTarget != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, attractTarget.position, Time.deltaTime * attractSpeed);
        }
        else
        {
            transform.Translate(dir * speed * Time.deltaTime);
        }

        if(inMovement)
        {
            Vector2 dragEnd = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            //Vector2 targetDir = dragEnd - dragStart;
            //float angle = Vector2.Angle(dragEnd, dragStart);
            //Debug.Log(angle);
            //angle = angle.normalized;// * speed;

            Transform directionArrowTransform = DirectionArrow.GetComponent<Transform>();
            directionArrowTransform.rotation = Quaternion.Euler(0,0,AngleBetweenVector2(dragEnd, dragStart));
        }
    }

    private float AngleBetweenVector2(Vector2 vec1, Vector2 vec2)
    {
        Vector2 difference = vec2 - vec1;
        float sign = (vec2.y < vec1.y) ? -1.0f : 1.0f;
        return Vector2.Angle(Vector2.right, difference) * sign;
    }

    public void setTarget(Transform target)
    {
        attractTarget = target;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Check for border. Go in diff dir if on a wall 
        if (other.gameObject.CompareTag("WorldBorder"))
        {
            dir = -dir;
        }
        //else if (other.gameObject.CompareTag("Infected"))
        //{
        //    if (myProbs.RollIfVirusSpread() && currentlySelected)
        //    {
        //        OnMouseUp();
        //    }
        //}
    }

    //TODO: bessere Erkennung! Es sollte reichen wenn man in die Nähe klickt
    private void OnMouseDown()
    {
        if (currentlySelected && myProbs.status != HealthStatusEnum.infected)
        {
            inMovement = true;
            DirectionArrow.SetActive(true);
            dragStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        //if (myProbs.status == HealthStatusEnum.infected)
        //    return;

        //dragStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnMouseOver()
    {
        if (myProbs.status != HealthStatusEnum.infected && MovementManager.Instance.TrySelectNewCharacter(myProbs))
        {
            Highlighter.SetActive(true);
            currentlySelected = true;
        }
    }

    private void OnMouseExit()
    {
        if (currentlySelected && !inMovement)
        {
            MovementManager.Instance.DeselectCharacter();
            Highlighter.SetActive(false);
        }
    }

    private void OnMouseUp()
    {
        if (currentlySelected && myProbs.status != HealthStatusEnum.infected && inMovement)
        {
            Vector2 dragEnd = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            //do nothing if distance is to smale
            if (Vector2.Distance(dragEnd, dragStart) > 0.001f)
            {
                dir = (dragEnd - dragStart);
                dir = dir.normalized;// * speed;

                inMovement = false;
                DirectionArrow.SetActive(false);
                OnMouseExit();

                StartCoroutine("LockFreeWill");
            }
        }
        else if (currentlySelected && myProbs.status == HealthStatusEnum.infected)
        {
            inMovement = false;
            MovementManager.Instance.DeselectCharacter();
            Highlighter.SetActive(false);
            DirectionArrow.SetActive(false);
        }
    }

    public void GotInfected()
    {
        if (currentlySelected)
        {
            OnMouseUp();
        }
    }

    IEnumerable LockFreeWill()
    {
        hasFreeWill = false;
        yield return new WaitForSeconds(lockFreeWillTime);
        hasFreeWill = true;
    }
}