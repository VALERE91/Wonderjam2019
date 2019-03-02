using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePlayerMeshController : MonoBehaviour
{
    //private GameObject parentPlayerGameObject;

    // Start is called before the first frame update
    void Start()
    {
        //parentPlayerGameObject = this.transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");
        float angle = Mathf.Atan2(v, h) * Mathf.Rad2Deg;

        Quaternion rotation = Quaternion.Euler(0, angle, 0);
        transform.rotation = rotation;

        //Debug.Log(angle + " VERT: " + v + " HORZ: " + h);
    }
}
