using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public float maxRangeX;
    public float maxRangeY;

    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1f, 0, 0.0f);
        Gizmos.DrawWireCube(transform.position, new Vector2(maxRangeX, maxRangeY));
    }

    public HumanProperties SpawnObject(GameObject preFab)
    {
        Vector2 pos = new Vector2(Random.Range(-maxRangeX / 2, maxRangeX / 2) + transform.position.x, Random.Range(-maxRangeY / 2, maxRangeY / 2) + transform.position.y);
        GameObject h = GameObject.Instantiate(preFab);
        h.transform.position = pos;
        HumanProperties probs = h.GetComponent<HumanProperties>();

        return probs;
    }
}
