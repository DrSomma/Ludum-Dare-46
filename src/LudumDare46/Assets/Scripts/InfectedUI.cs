using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InfectedUI : MonoBehaviour
{
    public TextMeshProUGUI counter;
    public string pattern = "Infected\n{0}/{1}";

    // Update is called once per frame
    void Update()
    {
        counter.SetText(string.Format(pattern, InfectionManager.Instance.getInfectedCount(), InfectionManager.Instance.getInfectedLosingCount()));
    }
}
