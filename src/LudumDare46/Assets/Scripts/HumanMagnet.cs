using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanMagnet : MonoBehaviour
{
    public bool interactHealthy = true;
    public bool interactInfected= true;
    public float attractCycle = 2f; //Check every X-Sec
    public float range = 2f;

    void Start()
    {
        InvokeRepeating("AttractHumans", 0, attractCycle);  //delay, repeat every
    }

    void AttractHumans()
    {
        List<HumanProperties> allHumans = InfectionManager.Instance.getAllHumans();
        foreach (var human in allHumans)
        {
            if(
                human.status == HealthStatusEnum.healthy && interactHealthy ||      //check if healty
                human.status == HealthStatusEnum.infected && interactInfected       //check if infected
            ){
                //check if human is in range
                if(Vector2.Distance(transform.position,human.transform.position) <= range){
                    HumanMovement move = human.GetComponent<HumanMovement>();
                    move.setTarget(transform);
                }
            }    
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1f, 0, 0.0f);
        Gizmos.DrawWireSphere(transform.position,range);
    }
}
