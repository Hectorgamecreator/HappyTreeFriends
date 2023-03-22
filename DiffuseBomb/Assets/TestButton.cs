using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestButton : MonoBehaviour
{
    [SerializeField]
    ProgressBar bar;

    public void TestClick()
    {
        bar.UpdateProgress(0.1f);
    }
}
