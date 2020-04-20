using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteAction : Collectible
{
    public override void doPickUp()
    {
        Destroy(gameObject);
    }
}
