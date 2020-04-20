using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteAction : Collectible
{
    private bool wasCollected = false;

    public float offsetY = 0.4f;

    public override void doPickUp(GameObject human)
    {
        if (wasCollected)
            return;
        wasCollected = true;

        //TODO PlaySound

        StartCoroutine(doAction(human));
    }

    IEnumerator doAction(GameObject human)
    {
        HumanMovement move = human.GetComponent<HumanMovement>();
        move.SetSpeed(0);

        transform.position = new Vector2(human.transform.position.x, human.transform.position.y+offsetY);


        yield return new WaitForSeconds(1f);
        move.SetSpeed(move.normalSpeed);
        Destroy(gameObject);
    }
}
