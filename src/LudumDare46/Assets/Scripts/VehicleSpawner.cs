using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleSpawner : MonoBehaviour
{
    public float rangeX = 20f;
    public float rangeY = 4f;
    public List<GameObject> vehicls;

    public float spawnEvery = 40f;   //Seconds
    public float startDelay = 10f;   //Seconds
         
    void Start()
    {
        InvokeRepeating("SpawnVehicle",startDelay,spawnEvery);
    }

    public void SpawnVehicle()
    {
        Vector2 startPos = new Vector2(transform.position.x,Random.Range(-rangeY / 2, rangeY / 2)+transform.position.y);
        Vector2 endPos = new Vector2(startPos.x + rangeX, startPos.y);

        GameObject preFab = vehicls[RandomInt(0, vehicls.Count)];
        GameObject obj = Instantiate(preFab);
        obj.transform.position = startPos;
        VehicleMovement vehMov = obj.GetComponent<VehicleMovement>();
        vehMov.SetTarget(endPos);
    }

    private int RandomInt(int start, int end)
    {
        return (int)Random.Range(start, end);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0, 1f, 0.0f);
        Vector2 center = new Vector2(transform.position.x+rangeX/2, transform.position.y);
        Gizmos.DrawWireCube(center, new Vector2(rangeX, rangeY));
    }
}
