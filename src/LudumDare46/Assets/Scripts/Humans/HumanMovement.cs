using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanMovement : MonoBehaviour
{
    [Header("config")]
    public float speed = 1.0f;
    public GameObject Highlighter;

    private Vector2 dir;
    private Vector2 dragStart;
    private bool currentlySelected; // Bin ich selektiert?
    private bool inMovement; // Bin ich gerade in einer Bewegeung? - Maus wurde geklickt
    private HumanProperties myProbs;

    void Start()
    {
        //Calc init move dir
        calcDir(Random.Range(0, 360));
        Highlighter.SetActive(false);

        myProbs = GetComponent<HumanProperties>();
    }

    private void calcDir(float angle)
    {
        dir = (Vector2)(Quaternion.Euler(0, 0, angle) * Vector2.up);
        dir = dir * speed;
    }

    void Update()
    {
        transform.Translate(dir * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Check for border. Go in diff dir if on a wall 
        if (other.gameObject.CompareTag("WorldBorder"))
        {
            dir = -dir;
        }
        else if (other.gameObject.CompareTag("Infected"))
        {
            if (myProbs.RollIfVirusSpread() && currentlySelected)
            {
                OnMouseUp();
            }
        }
    }

    //TODO: bessere Erkennung! Es sollte reichen wenn man in die Nähe klickt
    private void OnMouseDown()
    {
        if (currentlySelected && myProbs.status != HealthStatusEnum.infected)
        {
            inMovement = true;
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
            dir = (dragEnd - dragStart);
            dir = dir.normalized * speed;

            inMovement = false;
            OnMouseExit();
        } else if (currentlySelected && myProbs.status == HealthStatusEnum.infected)
        {
            inMovement = false;
            MovementManager.Instance.DeselectCharacter();
            Highlighter.SetActive(false);
        }
    }

}