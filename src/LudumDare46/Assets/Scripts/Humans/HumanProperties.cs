using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanProperties : MonoBehaviour
{

    public HealthStatusEnum status;
    public SexEnum sex;
    public int age;

    public GameObject source;
    private Virus myVirus;

    private void Awake()
    {
        myVirus = GetComponent<Virus>();
    }

    private void Start()
    {
        if (status == HealthStatusEnum.healthy)
        {
            gameObject.tag = "Human";
            myVirus.setVirusActive(false);
        }
        else if (status == HealthStatusEnum.infected)
        {
            Infect();
        }
    }

    public void Infect()
    {
        status = HealthStatusEnum.infected;
        InfectionManager.Instance.AddNewInfected(this);
        gameObject.tag = "Infected";
        myVirus.setVirusActive(true);

        //WIP
        GetComponent<SpriteRenderer>().color = Color.green;
        GetComponent<HumanMovement>().GotInfected();
    }

    //public bool RollIfVirusSpread()
    //{
    //    if (status == HealthStatusEnum.infected)
    //    {
    //        return true;
    //    }

    //    if (Random.Range(0f, 1f) <= percentSpread)
    //    {
    //        Infect();
    //        return true;
    //    }
    //    else
    //    {
    //        Debug.Log("LUCKY!!!");
    //        return false;
    //    }

    //}
    public void hadContact(GameObject s)
    {
        source = s;
        Infect();
    }
}
