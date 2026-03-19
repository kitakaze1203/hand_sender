using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.Text;
using TMPro;
using UnityEngine.SceneManagement;

public class Sender : MonoBehaviour
{
    public int port = 20000;
    private UdpClient udpClient;
    private Quaternion baseRotation = Quaternion.identity;
    [SerializeField] TextMeshProUGUI sending_IP;
    private int[] motor = new int[5];
    private bool zflag = false;
    // Start is called before the first frame update

    void Start()
    {
        udpClient = new UdpClient();
        baseRotation = Quaternion.Inverse(Input.gyro.attitude);
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }


    // Update is called once per frame
    void Update()
    {
        if (string.IsNullOrEmpty(SceneChanger.remoteIp))
        {
            sending_IP.text = "not send";
            return;
        }
        sending_IP.text = $"{SceneChanger.remoteIp} に送信中...";
        if (zflag)
        {
            baseRotation = Quaternion.Inverse(Input.gyro.attitude);
        }
        Quaternion q = baseRotation * Input.gyro.attitude;
        Quaternion corQ = new Quaternion(q.x, q.z, q.y, -q.w);

        transform.localRotation = corQ;

        string message = $"{corQ.x},{corQ.y},{corQ.z},{corQ.w},{motor[0]},{motor[1]},{motor[2]},{motor[3]},{motor[4]}";
        byte[] data = Encoding.UTF8.GetBytes(message);
        try
        {
            udpClient.Send(data, data.Length, SceneChanger.remoteIp, port);
        }
        catch { /* 送信エラー無視 */ }

    }
    void OnApplicationQuit() => udpClient.Close();
    void OnSceneUnloaded(Scene scene)
    {
        byte[] data = Encoding.UTF8.GetBytes("0.0,0.0,0.0,0.0,0,0,0,0,0");
        udpClient.Send(data, data.Length, SceneChanger.remoteIp, port);
        udpClient.Close();
    }
    public void OnMotor1() => motor[0] = 1;
    public void OffMotor1() => motor[0] = 0;
    public void RevMotor1() => motor[0] = -1;
    public void OnMotor2() => motor[1] = 1;
    public void OffMotor2() => motor[1] = 0;
    public void RevMotor2() => motor[1] = -1;
    public void OnMotor3() => motor[2] = 1;
    public void OffMotor3() => motor[2] = 0;
    public void OnMotor4() => motor[3] = 1;
    public void OffMotor4() => motor[3] = 0;
    public void OnMotor5() => motor[4] = 1;
    public void OffMotor5() => motor[4] = 0;
    public void OnResetZ() => zflag = true;
    public void OffResetZ() => zflag = false;
}