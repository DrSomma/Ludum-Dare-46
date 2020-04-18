using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Virus : MonoBehaviour
{
    public bool isActiv = false;

    public float range = 2f;
    public float percentSpread = 0.30f;
    public float infectionCycle = 2f; //Check every X-Sec

    // Start is called before the first frame update
    void Start()
    {
        setVirusActive(isActiv);
    }

    public void setVirusActive(bool status){
        isActiv = status;
        if(status){
            InvokeRepeating("InfectHumans", infectionCycle, infectionCycle);  //delay, repeat every
        }else{
            CancelInvoke();
        }
    }

    // Update is called once per frame
    void InfectHumans()
    {
        List<HumanProperties> allHumans = InfectionManager.Instance.getAllHumans();
        foreach (var human in allHumans)
        {
            
            if(human.status == HealthStatusEnum.healthy){
                if(Vector2.Distance(transform.position,human.transform.position) <= range){
                    if(Random.Range(0f,1f)<=percentSpread){
                        human.hadContact(gameObject);
                    }
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
