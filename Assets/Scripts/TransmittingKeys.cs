using System.Diagnostics;
using WindowsInput;
using WindowsInput.Native;
using UnityEngine;

public class TransmittingKeys : MonoBehaviour
{
    //InputSimulator simulator;

    private void Start()
    {
        //simulator = new InputSimulator();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.K))
        {
            //simulator.KeyPress(VirtualKeyCode.VK_K);
            //simulator.Keyboard.KeyPress(VirtualKeyCode.VK_0);
            Process p = Process.GetProcessById(15184);
            p.StandardInput.Write("Hello");
        }
    }
}
