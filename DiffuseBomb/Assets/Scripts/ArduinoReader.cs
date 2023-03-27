using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class ArduinoReader : MonoBehaviour
{

    SerialPort stream;

     void Start()
    {
        stream = new SerialPort("/dev/cu.usbmodem14301");
        stream.ReadTimeout = 50;
        stream.Open();
    }

     void Update()
    {
        
        if(stream.IsOpen)
        {
            try
            {
                string passcode = stream.ReadLine();
                Debug.Log(passcode);
            }

            catch (System.Exception)
            {
                //Ignore timeouts and other exceptions
            }
        }    
    }

    private void OnDestroy()
    {
        if(stream != null)
        {
            stream.Close();
        }
       
    }
}
