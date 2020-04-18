using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject human;
    public float maxRange;
    public float startInfected = 0.03f;
    public int count = 100;

    // Start is called before the first frame update
    void Start()
    {
        List<HumanProperties> allHumans = new List<HumanProperties>();

        for (int i = 0; i < count; i++)
        {
            Vector2 pos = new Vector2(Random.Range(-maxRange,maxRange)+transform.position.x,Random.Range(-maxRange,maxRange)+transform.position.y);

            GameObject h = GameObject.Instantiate(human);
            h.transform.position = pos;
            HumanProperties probs = h.GetComponent<HumanProperties>();
            SetRndProbs(probs);
            allHumans.Add(probs);
        }

        //failsave - need min 1 infected
        if(InfectionManager.Instance.infectedHumans.Count == 0){
            allHumans[0].Infect();      //infect first in list
        }

        InfectionManager.Instance.allHumans = allHumans;
    }

    void SetRndProbs(HumanProperties probs){
        //TODO: macht nicht viel Sinn oder? Sprit muss angepasst werden
        probs.sex = RandomInt(0,1)==0 ? SexEnum.male : SexEnum.female;
        probs.age = Random.Range(5,99);

        if(Random.Range(0f,1f)<=startInfected){
            probs.Infect();
        }else{
            probs.status = HealthStatusEnum.healthy;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1f, 0, 0.0f);
        Gizmos.DrawWireSphere(transform.position,maxRange);
    }

    private int RandomInt(int start, int end){
        return (int) Random.Range(start,end);
    }

}
