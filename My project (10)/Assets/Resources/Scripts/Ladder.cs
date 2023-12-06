
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform chController;
    bool inside;
    public float speedUpDown=3.2f;
    public scgFullBodyController.ThirdPersonUserControl FPSInput;
    public scgFullBodyController.ThirdPersonCharacter FPS1Input;
    public float x;
    void Start()
    {
        FPSInput = GetComponent<scgFullBodyController.ThirdPersonUserControl>();
        FPS1Input = GetComponent<scgFullBodyController.ThirdPersonCharacter>();
        inside = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ladder")
        {
            FPSInput.enabled = false;
            FPS1Input.enabled = true;
            inside = !inside;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Ladder")
        {
            FPSInput.enabled = true;
            FPS1Input.enabled = true;
            inside = !inside;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (inside && Input.GetKey("w"))
            chController.transform.position += Vector3.up / speedUpDown;
        if(inside&&Input.GetKey("s"))
            chController.transform.position += Vector3.down / speedUpDown;
    }
}
