using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public List<GameObject> humans;
    public float startInfected = 0.03f;
    public int count = 100;
    public List<SpawnPoint> spawnPoints;

    // Start is called before the first frame update
    void Start()
    {
        List<HumanProperties> allHumans = new List<HumanProperties>();

        for (int i = 0; i < count; i++)
        {
            GameObject preFab = humans[RandomInt(0, humans.Count)];
            SpawnPoint point = spawnPoints[RandomInt(0, spawnPoints.Count)];
            HumanProperties probs = point.SpawnObject(preFab);

            SetRndProbs(probs);
            allHumans.Add(probs);
        }

        //failsave - need min 1 infected
        InfectionManager.Instance.setAllHumans(allHumans);
        if (InfectionManager.Instance.infectedHumans.Count == 0){
            allHumans[0].Infect();      //infect first in list
        }
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

    private int RandomInt(int start, int end){
        return (int) Random.Range(start,end);
    }

}
