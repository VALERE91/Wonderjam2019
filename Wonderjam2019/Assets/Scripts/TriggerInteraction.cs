using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerInteraction : MonoBehaviour
{
    public string AllowTagInterect = "Player";
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other) {
        if(other.tag == AllowTagInterect)
        {
            Debug.Log("USER YOU CAN INTERACT WITH MEEEEEE");
        }
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log("Good BYYYYYYYYYYYYYYYYYYYE");
    }
}
