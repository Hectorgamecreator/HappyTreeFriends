using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class ArduinoReader : MonoBehaviour
{

    SerialPort port = new SerialPort("/dev/cu.usbmodem1301", 9600);


    // Start is called before the first frame update
    void Start()
    {
        port.Open();
    }

    // Update is called once per frame
    void Update()
    {
        if(port.IsOpen)
        {
            string data = port.ReadLine();// read a line of data from the serial port
            Debug.Log(data);// output the data to the Unity console
        }
    }

    private void OnApplicationQuit()
    {
        port.Close(); // close the serial port when the application is quit.
    }
}
