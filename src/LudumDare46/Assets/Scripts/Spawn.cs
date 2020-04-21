using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public List<GameObject> humans;
    public int startInfected = 2;
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
        if (InfectionManager.Instance.infectedHumans.Count <= startInfected)
        {
            for (int i = 0; i < startInfected; i++)
            {
                allHumans[i].Infect();      //infect
            }
        }
    }

    void SetRndProbs(HumanProperties probs){
        //TODO: macht nicht viel Sinn oder? Sprit muss angepasst werden
        probs.sex = RandomInt(0,1)==0 ? SexEnum.male : SexEnum.female;
        probs.age = Random.Range(5,99);
        probs.status = HealthStatusEnum.healthy;
    }

    private int RandomInt(int start, int end){
        return (int) Random.Range(start,end);
    }

}
