//SlapChickenGames
//2021
//Camera controller for x and y movement

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


namespace scgFullBodyController
{
    public class CameraController : MonoBehaviour
    {
        public float Sensitivity = 10f;
        public float minPitch = -30f;
        public float maxPitch = 60f;
        public Transform parent;
        public Transform boneParent;

        public Transform spineToOrientate;

        private float pitch = 0f;

        public GameObject PlayerManager;
        [HideInInspector] public float yaw = 0f;
        [HideInInspector] public float relativeYaw = 0f;
        private void Awake()
        {
        }
        void OnEnable()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        void LateUpdate()
        {
            //if (!PV.IsMine)
            //    return;
            CameraRotate();
           
            transform.position = boneParent.position;

            spineToOrientate.rotation = transform.rotation;
        }

        void CameraRotate()
        {
            //Get input to turn the cam view
            relativeYaw = Input.GetAxis("Mouse X") * Sensitivity;
            pitch -= Input.GetAxis("Mouse Y") * Sensitivity;
            yaw += Input.GetAxis("Mouse X") * Sensitivity;
            pitch = Mathf.Clamp(pitch, minPitch, maxPitch);
            transform.eulerAngles = new Vector3(pitch, yaw, 0f);
        }
    }
}