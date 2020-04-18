using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusCloud : MonoBehaviour
{
    public float deleteAfter = 2f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("DeleteObj");
    }

    IEnumerator DeleteObj(){
        yield return new WaitForSeconds(deleteAfter);
        Destroy(this.gameObject);
    }
}
