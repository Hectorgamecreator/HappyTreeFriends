
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class JoyStickController : MonoBehaviour
{
    SerialPort stream = new SerialPort("/dev/cu.usbmodem14201", 9600);

    float joystickX = 0.0f;
    float joystickY = 0.0f;

    public float speed = 1f;
    public GameManager gameManager; // Reference to the GameManager script

    private float prevJoystickX = 0.0f;
    private float prevJoystickY = 0.0f;

    void Start()
    {
        stream.Open(); // Open the serial port
    }

    void Update()
    {
        if (stream.IsOpen)
        {
            string data = stream.ReadLine(); // Read the data from the Arduino
            string[] values = data.Split(','); // Split the data into individual values

            if (values.Length == 2) // Make sure there are two values
            {

                // Parse the values and assign them to the joystickX and joystickY variables
                joystickX = float.Parse(values[0]) / 1023.0f;
                joystickY = float.Parse(values[1]) / 1023.0f;

                // Smooth the joystick input
                joystickX = Mathf.Lerp(prevJoystickX, joystickX, 0.1f);
                joystickY = Mathf.Lerp(prevJoystickY, joystickY, 0.1f);


                Debug.Log("Joystick X: " + joystickX);
                Debug.Log("Joystick Y: " + joystickY);
            }

            // Map joystick input to Shift function calls in the GameManager script
            if (Mathf.Abs(joystickX) > 0.1f || Mathf.Abs(joystickY) > 0.1f)
            {
                Vector2 dir = Vector2.zero;

                if (Mathf.Abs(joystickX) > Mathf.Abs(joystickY))
                {
                    if (joystickX < 0)
                    {
                        dir = Vector2.left;
                    }
                    else
                    {
                        dir = Vector2.right;
                    }
                }
                else
                {
                    if (joystickY < 0)
                    {
                        dir = Vector2.down;
                    }
                    else
                    {
                        dir = Vector2.up;
                    }
                }

                gameManager.Shift(dir * speed * Time.deltaTime);
            }
        }

        void OnApplicationQuit()
        {
            if (stream.IsOpen)
            {
                stream.Close(); // Close the serial port when the application quits
            }
        }
    }
}