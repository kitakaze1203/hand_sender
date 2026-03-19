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
    private int motor1 = 0;
    private int motor2 = 0;
    private int motor3 = 0;
    private int motor4 = 0;
    private int motor5 = 0;
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
        Quaternion q = baseRotation*Input.gyro.attitude;
        Quaternion corQ = new Quaternion(q.x, q.z, q.y, -q.w);

        transform.localRotation = corQ;

        string message = $"{corQ.x},{corQ.y},{corQ.z},{corQ.w},{motor1},{motor2},{motor3},{motor4},{motor5}";
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

    public void OnMotor1() => motor1 = 1;
    public void OffMotor1() => motor1 = 0;
    public void RevMotor1() => motor1 = -1;
    public void OnMotor2() => motor2 = 1;
    public void OffMotor2() => motor2 = 0;
    public void RevMotor2() => motor2 = -1;
    public void OnMotor3() => motor3 = 1;
    public void OffMotor3() => motor3 = 0;
    public void OnMotor4() => motor4 = 1;
    public void OffMotor4() => motor4 = 0;
    public void OnMotor5() => motor5 = 1;
    public void OffMotor5() => motor5 = 0;
}
