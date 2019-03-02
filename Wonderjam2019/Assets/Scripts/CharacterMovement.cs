using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMovement : MonoBehaviour
{
    public int CharacterControllerID = 0;
    public float MovementSpeed = 2.0f;
    public float MovementLimit = 2.0f;

    private CharacterController m_CharacterController;

    void Start()
    {
        m_CharacterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        float horizontal = Input.GetAxis("JAM_LTRIG_HOR_1");
        float vertical = Input.GetAxis("JAM_LTRIG_VERT_1");

        m_CharacterController.Move(new Vector3(horizontal * Time.deltaTime * MovementSpeed, 0.0f, -vertical * Time.deltaTime * MovementSpeed));

        //Debug.Log(horizontal);
        //if (horizontal != 0.0f && Mathf.Abs((transform.localPosition + (Vector3.left * horizontal * Time.deltaTime * MovementSpeed)).magnitude) <= MovementLimit)
        //{
        //    transform.position += Vector3.left * horizontal * Time.deltaTime * MovementSpeed;
        //}
    }
}
