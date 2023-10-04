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
        PhotonView PV;
        private void Awake()
        {
            PV = PlayerManager.GetComponent<PhotonView>();
        }

        // Update is called once per frame
        void LateUpdate()
        {
            if (!PV.IsMine)
                return;
            spineToOrientate.rotation = transform.rotation;
        }
    }
}
