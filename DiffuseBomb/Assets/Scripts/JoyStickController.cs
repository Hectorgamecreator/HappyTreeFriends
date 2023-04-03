using System.Collections;
using UnityEngine;
using System.IO.Ports;

public class JoyStickController : MonoBehaviour
{
    public Rigidbody rb;

    public float forwardForce = 4000f;
    public float sidewaysForce = 100f;
    public int CMD;

    public SerialPort sp = new SerialPort("/dev/cu.usbmodem141kk01", 9600);

    // Start is called before the first frame update
    void Start()
    {
        sp.Open();
        sp.ReadTimeout = 1;
    }

    void Update()
    {
        if (sp.IsOpen)
        {
            try
            {
                ReadCom();
                Move();
            }
            catch (System.Exception)
            {

            }
        }
        else
        {
            Move();
        }
    }

    // Update is called once per frame
    void Move()
    {
        rb.AddForce(0, 0, forwardForce * Time.deltaTime);

        if (Input.GetKey("d") || CMD == 6)
        {
            rb.AddForce(sidewaysForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
        }

        if (Input.GetKey("a") || CMD == 4)
        {
            rb.AddForce(-sidewaysForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
        }

        if (Input.GetKey("w") || CMD == 8)
        {
            rb.AddForce(0, sidewaysForce * Time.deltaTime, 0, ForceMode.VelocityChange);
        }

        if (Input.GetKey("s") || CMD == 2)
        {
            rb.AddForce(0, -sidewaysForce * Time.deltaTime, 0, ForceMode.VelocityChange);
        }

       
    }

    void ReadCom()
    {
        CMD = sp.ReadByte();
    }

    void OnDestroy()
    {
        // Close the serial port when the script is destroyed  
        sp.Close();
    }
}


