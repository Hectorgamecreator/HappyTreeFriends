using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class ArduinoReader : MonoBehaviour
{

    SerialPort stream;

    public PuzzleManager puzzleManager;
   

    public string passcode; //Delcare passcode variable

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

                if (passcode == "1234")
                {
                    puzzleManager.OnPuzzleSolved();
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
        if(stream != null)
        {
            stream.Close();
        }
       
    }

   
    
}
