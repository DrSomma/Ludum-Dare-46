using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject human;
    public float maxRange;
    public int count = 100;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < count; i++)
        {
            Vector2 pos = new Vector2(Random.Range(-maxRange,maxRange)+transform.position.x,Random.Range(-maxRange,maxRange)+transform.position.y);

            GameObject h = GameObject.Instantiate(human);
            h.transform.position = pos;
            SetRndProbs(h);
        }
        
    }

    void SetRndProbs(GameObject h){
        HumanProperties probs = h.GetComponent<HumanProperties>();
        //TODO: Wirklich RND machen
        probs.sex = SexEnum.male;
        probs.status = HealthStatusEnum.healthy;
        probs.age = Random.Range(5,99);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1f, 0, 0.0f);
        Gizmos.DrawWireSphere(transform.position,maxRange);
    }

}
