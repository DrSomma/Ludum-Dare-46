using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusCloud : MonoBehaviour
{
    public float deleteAfter = 2f;

    private Animator animator;
    private Virus virus;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        virus = GetComponent<Virus>();
        StartCoroutine("DeleteObj");
    }

    IEnumerator DeleteObj(){

        yield return new WaitForSeconds(deleteAfter);
        virus.setVirusActive(false);
        animator.SetTrigger("fadeOut");
        yield return new WaitForSeconds(1);
        Destroy(this.gameObject);
    }
}
