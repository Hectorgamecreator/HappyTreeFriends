using System.Collections;
using UnityEngine;
using System.IO.Ports;
using System.Globalization;

public class TestConnection : MonoBehaviour
{
    SerialPort data_stream = new SerialPort("/dev/cu.usbmodem14301", 9600); 
    public string recievedstring;
    public GameObject test_data;
    public Rigidbody rb;
    public float sensitivity = 0.01f;

    public string[] datas;

    void Start()
    {
        data_stream.Open(); //Initate the Serial stream

    }

    void Update()
    {
        recievedstring = data_stream.ReadLine();
        string[] datas = recievedstring.Split(','); //split the data between ','

        Debug.Log("Received data: " + recievedstring); // print received data
        Debug.Log("Split data: " + string.Join(",", datas)); // print split data


        if (datas.Length >= 3)
        {
            if (float.TryParse(datas[0], NumberStyles.Float, CultureInfo.InvariantCulture, out float forceZ))
            {
                rb.AddForce(0, 0, forceZ * sensitivity * Time.deltaTime, ForceMode.VelocityChange);
            }
            else
            {
                Debug.LogError("Unable to parse force Z: " + datas[0]);
            }

            if (float.TryParse(datas[1], NumberStyles.Float, CultureInfo.InvariantCulture, out float forceX))
            {
                rb.AddForce(forceX * sensitivity * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
            }
            else
            {
                Debug.LogError("Unable to parse force X: " + datas[1]);
            }

            if (float.TryParse(datas[2], NumberStyles.Float, CultureInfo.InvariantCulture, out float rotationY))
            {
                transform.Rotate(0, rotationY, 0);
            }
            else
            {
                Debug.LogError("Unable to parse rotation Y: " + datas[2]);
            }
        }
        else
        {
            Debug.LogWarning("Invalid number of data points received: " + datas.Length);
        }
    }
}
    
