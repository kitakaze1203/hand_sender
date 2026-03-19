using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetIP : MonoBehaviour
{
    public void ReIP()
    {
        SceneChanger.remoteIp = null;
        SceneManager.LoadScene("setIP");
    }
}
