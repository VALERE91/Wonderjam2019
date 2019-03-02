using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrinkFountain : MonoBehaviour
{
    public GameObject ButtonPrompt;

    void Start()
    {
        ButtonPrompt.SetActive(false);
    }
    
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            ButtonPrompt.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            ButtonPrompt.SetActive(false);
        }
    }
}
