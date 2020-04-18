using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementManager : MonoBehaviour
{
    public static MovementManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public HumanProperties SelectedCharacter;

    public bool TrySelectNewCharacter(HumanProperties vHumanProperties)
    {
        if (SelectedCharacter == null)
        {
            SelectedCharacter = vHumanProperties;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void DeselectCharacter()
    {
        SelectedCharacter = null;
    }



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
