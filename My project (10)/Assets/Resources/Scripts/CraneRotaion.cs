using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraneRotaion : MonoBehaviour
{
    // Start is called before the first frame update
    float speed=1;
    public float maxPoint;
    public float minPoint;
    void Start()
    {
        speed = -1; 
    }

    // Update is called once per frame
    void Update()
    {
        //var y= UnityEditor.TransformUtils.GetInspectorRotation(gameObject.transform).y;
        //if (y >= maxPoint)
        //    speed = -1 ;
        //if (y <= minPoint)
        //    speed= 1;
        //transform.Rotate(0, speed * Time.deltaTime, 0);      
    }

}
