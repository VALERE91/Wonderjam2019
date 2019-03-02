using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextOverHead : MonoBehaviour
{
    public Text m_textOverHead;
    public Transform m_transform;
    public Transform m_textOverTransform;

    // Start is called before the first frame update
    void Start()
    {
        m_textOverHead = GetComponentInParent<Text>();

        if (m_textOverHead == null)
        {
            Debug.LogError("Text must exist");
            return;
        }

        m_transform = transform;
        m_textOverTransform = m_textOverHead.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LateUpdate()
    {
        if(m_textOverTransform == null)
        {
            return;
        }

        Vector3 screenPos = Camera.main.WorldToScreenPoint(m_transform.position);
        // add a tiny bit of height?
        screenPos.y += 2; // adjust as you see fit.
        m_textOverTransform.position = screenPos;
    }
}
