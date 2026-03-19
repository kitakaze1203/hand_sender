using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SceneChanger : MonoBehaviour
{
    // Start is called before the first frame update
    public static string remoteIp;
    public TMP_InputField ipInputField;
    public void SendStart()
    {
        if (ipInputField != null)
        {
            Input.gyro.enabled = true;
            remoteIp = ipInputField.text;
            Debug.Log("IPƒZƒbƒg");
            SceneManager.LoadScene("sending");
        }
    }
}
