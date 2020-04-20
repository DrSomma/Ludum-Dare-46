using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfectionManager : MonoBehaviour
{
    public static InfectionManager Instance;

    public GameObject gravePrefab;
    public GameObject linePrefab;

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
    }

    public float losingPercent = 0.5f;

    public List<HumanProperties> infectedHumans;
    public List<HumanProperties> allHumans;
    public bool isGameOver = false;

    private int losingCnt = 999999;

    private void Start() {
        infectedHumans = new List<HumanProperties>();
    }

    public void setAllHumans(List<HumanProperties> all){
        allHumans = all;
        losingCnt = (int) (allHumans.Count * losingPercent);
    }

    public void addHuman(HumanProperties newHuman){
        allHumans.Add(newHuman);
    }

    public void AddNewInfected(HumanProperties human){
        if (isGameOver)
            return;
        
        //check if already  counted
        if(infectedHumans.Contains(human))
            return;

        Debug.Log("New!");
        infectedHumans.Add(human);

        CheckLost();
    }

    public void CheckLost(){
        if(getInfectedCount() >= losingCnt){
            isGameOver = true;
            Debug.Log("GAME OVER! " + getInfectedCount() + " " + losingCnt);

            Time.timeScale = 0;

            foreach (HumanProperties human in allHumans)
            {
                Vector2 pos = human.transform.position;

                //Draw line
                if(human.source != null) {
                    GameObject lineObj = Instantiate(linePrefab);
                    GameOverLine line = lineObj.GetComponent<GameOverLine>();
                    line.DrawLine(pos, human.source.transform.position);
                }

                human.gameObject.SetActive(false);
                GameObject grave = Instantiate(gravePrefab);
                gravePrefab.transform.position = pos;
            }
        }
    }

    public int getInfectedCount(){
        return infectedHumans.Count;
    }

    public int getInfectedLosingCount(){
        return losingCnt;
    }

    public List<HumanProperties> getAllHumans(){
        return allHumans;
    }


}
