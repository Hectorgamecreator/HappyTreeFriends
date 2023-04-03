using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class ArduinoReader : MonoBehaviour
{

    SerialPort stream;

    public GameManager gameManager;
    int Message;

    public string passcode; //Delcare passcode variable

    void Start()
    {
        stream = new SerialPort("/dev/cu.usbmodem14301");
        stream.ReadTimeout = 50;
        stream.Open();
    }

    void Update()
    {

        if (stream.IsOpen)
        {
            try
            {
                Message = stream.ReadByte();
                Debug.Log(Message);

                if (Message == 0421)
                {
                    ///gameManager.OnPuzzleSolved();
                }
            }

            catch (System.Exception)
            {
                //Ignore timeouts and other exceptions
            }
        }


    }

    private void OnDestroy()
    {
        if (stream != null)
        {
            stream.Close();
        }

    }



}