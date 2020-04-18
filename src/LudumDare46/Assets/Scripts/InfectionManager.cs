using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfectionManager : MonoBehaviour
{
    public static InfectionManager Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public List<HumanProperties> infectedHumans;
    public List<HumanProperties> allHumans;

    private void Start() {
        infectedHumans = new List<HumanProperties>();
    }

    public void AddNewInfected(HumanProperties human){
        //check if already  counted
        if(infectedHumans.Contains(human))
            return;

        Debug.Log("New!");
        infectedHumans.Add(human);
    }
}
