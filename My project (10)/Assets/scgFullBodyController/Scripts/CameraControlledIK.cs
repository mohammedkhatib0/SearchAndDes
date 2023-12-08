//SlapChickenGames
//2021
//Camera spine controller

using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace scgFullBodyController
{
    public class CameraControlledIK : MonoBehaviour
    {
        public Transform spineToOrientate;
        public GameObject PlayerManager;

        // Update is called once per frame
        void LateUpdate()
        {
            spineToOrientate.rotation = transform.rotation;
        }
    }
}
