using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PromptDisplay : MonoBehaviour
{
    public GameObject Prompt;

    public List<Image> Images;

    public Sprite DPadLeft;
    public Sprite DPadRight;
    public Sprite DPadUp;
    public Sprite DPadDown;

    public Sprite A;
    public Sprite B;
    public Sprite X;
    public Sprite Y;

    private List<KeyType> m_PromptList;
    private List<KeyType> m_TargetPrompt = null;

    private bool m_LockHorizontal = false;
    private bool m_LockVertical = false;

    public enum KeyType
    {
        Up,
        Down,
        Left,
        Right,
        A,
        B,
        X,
        Y
    }

    public void SetTargetPrompts(List<KeyType> types)
    {
        m_TargetPrompt = types;
        Prompt.SetActive(true);
    }

    void Start()
    {
        Prompt.SetActive(false);
        m_PromptList = new List<KeyType>();
        m_TargetPrompt = new List<KeyType>() { KeyType.Left, KeyType.Right, KeyType.Up, KeyType.Down};
    }
    
    void Update()
    {
        if (Prompt.activeSelf)
        {
            float horizontalDPad = Input.GetAxis("JAM_DPAD_HOR_1");
            float verticalDPad = Input.GetAxis("JAM_DPAD_VERT_1");

            if (horizontalDPad == 0.0f)
            {
                m_LockHorizontal = false;
            }

            if (verticalDPad == 0.0f)
            {
                m_LockVertical = false;
            }

            if (horizontalDPad > 0.1f && !m_LockHorizontal)
            {
                m_PromptList.Add(KeyType.Right);
                m_LockHorizontal = true;
            }
            else if (horizontalDPad < -0.1f && !m_LockHorizontal)
            {
                m_PromptList.Add(KeyType.Left);
                m_LockHorizontal = true;
            }
            else if (verticalDPad > 0.1f && !m_LockVertical)
            {
                m_PromptList.Add(KeyType.Up);
                m_LockVertical = true;
            }
            else if (verticalDPad < -0.1f && !m_LockVertical)
            {
                m_PromptList.Add(KeyType.Down);
                m_LockVertical = true;
            }
            else if (Input.GetButtonDown("JAM_A_1"))
            {
                m_PromptList.Add(KeyType.A);
            }
            else if (Input.GetButtonDown("JAM_B_1"))
            {
                m_PromptList.Add(KeyType.B);
            }
            else if (Input.GetButtonDown("JAM_X_1"))
            {
                m_PromptList.Add(KeyType.X);
            }
            else if (Input.GetButtonDown("JAM_Y_1"))
            {
                m_PromptList.Add(KeyType.Y);
            }

            if (m_PromptList.SequenceEqual(m_TargetPrompt))
            {
                m_PromptList.Clear();
            }

            if (m_PromptList.Count > 7)
            {
                m_PromptList.Clear();
            }

            for (int i = 0; i < 7; i++)
            {
                if (i >= m_PromptList.Count)
                {
                    Images[i].gameObject.SetActive(false);
                }
                else
                {
                    Images[i].gameObject.SetActive(true);

                    switch (m_PromptList[i])
                    {
                        case KeyType.Up:
                            Images[i].sprite = DPadUp;
                            break;

                        case KeyType.Down:
                            Images[i].sprite = DPadDown;
                            break;

                        case KeyType.Left:
                            Images[i].sprite = DPadLeft;
                            break;

                        case KeyType.Right:
                            Images[i].sprite = DPadRight;
                            break;

                        case KeyType.A:
                            Images[i].sprite = A;
                            break;

                        case KeyType.B:
                            Images[i].sprite = B;
                            break;

                        case KeyType.X:
                            Images[i].sprite = X;
                            break;

                        case KeyType.Y:
                            Images[i].sprite = Y;
                            break;
                    }
                }
            }
        }
    }
}