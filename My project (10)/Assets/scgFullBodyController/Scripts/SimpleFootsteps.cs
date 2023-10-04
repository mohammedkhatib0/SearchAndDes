﻿//SlapChickenGames
//2021
//Simple foot logic for detecting movement and adding sound

using UnityEngine;
using System.Collections;
using Photon.Pun;

namespace scgFullBodyController
{
    public class SimpleFootsteps : MonoBehaviour
    {
        //IMPORTANT, this script needs to be on the root transform

        public AudioClip[] soundGrass;
        public AudioClip[] soundWater;
        public AudioClip[] soundMetal;
        public AudioClip[] soundConcrete;
        public AudioClip[] soundGravel;
        public AudioSource audioSource;
        string floortag;
        public float footstepSensitivity;
        public float playbackSpeedDamping;
        public float speed = 0.0f;
        bool moving = false;
        bool toggle;
        public bool isAi;
        PhotonView PV;
        private void Awake()
        {
            PV = GetComponent<PhotonView>();
        }

        void Start()
        {
            StartCoroutine("senseSteps");
        }

        void OnCollisionEnter(Collision col)
        {
            if (col.transform.tag == "grass")
            {
                floortag = "grass";
            }
            else if (col.transform.tag == "metal")
            {
                floortag = "metal";
            }
            else if (col.transform.tag == "gravel")
            {
                floortag = "gravel";
            }
            else if (col.transform.tag == "water")
            {
                floortag = "water";
            }
            else if (col.transform.tag == "concrete")
            {
                floortag = "concrete";
            }
        }

        void OnTriggerEnter(Collider col)
        {
            if (col.transform.tag == "grass")
            {
                floortag = "grass";
            }
            else if (col.transform.tag == "metal")
            {
                floortag = "metal";
            }
            else if (col.transform.tag == "gravel")
            {
                floortag = "gravel";
            }
            else if (col.transform.tag == "water")
            {
                floortag = "water";
            }
            else if (col.transform.tag == "concrete")
            {
                floortag = "concrete";
            }
        }

        void Update()
        {
            //Sensing movement for players
            var velocity = gameObject.GetComponent<Rigidbody>().velocity;
            var localVel = transform.InverseTransformDirection(velocity);

            if (localVel.z > footstepSensitivity)
            {
                moving = true;
            }
            else if (localVel.z < (footstepSensitivity * -1))
            {
                moving = true;
            }
            else if (localVel.x > footstepSensitivity)
            {
                moving = true;
            }
            else if (localVel.x < (footstepSensitivity * -1))
            {
                moving = true;
            }
            else
            {
                moving = false;
            }

            //Different playback speed calculations (for crouching, sprinting etc.)
            speed = 1 - (gameObject.GetComponent<Rigidbody>().velocity.magnitude * playbackSpeedDamping);



        }

        IEnumerator senseSteps()
        {
            while (true)
            {

                if (gameObject.GetComponent<ThirdPersonCharacter>().m_IsGrounded && moving && !gameObject.GetComponent<ThirdPersonCharacter>().m_Sliding)
                {
                    if (floortag == "grass")
                    {
                        audioSource.clip = soundGrass[Random.Range(0, soundGrass.Length)];
                    }
                    else if (floortag == "gravel")
                    {
                        audioSource.clip = soundGravel[Random.Range(0, soundGravel.Length)];
                    }
                    else if (floortag == "water")
                    {
                        audioSource.clip = soundWater[Random.Range(0, soundWater.Length)];
                    }
                    else if (floortag == "metal")
                    {
                        audioSource.clip = soundMetal[Random.Range(0, soundMetal.Length)];
                    }
                    else if (floortag == "concrete")
                    {
                        audioSource.clip = soundConcrete[Random.Range(0, soundConcrete.Length)];
                    }
                    else
                    {
                        yield return 0;
                    }
                    PV.RPC("PlaySound", RpcTarget.All);
                    yield return new WaitForSeconds(speed);
                }
                else
                {
                    yield return 0;
                }
            }
        }
        [PunRPC]
        void PlaySound()
        {
            if (audioSource.clip != null)
                audioSource.PlayOneShot(audioSource.clip);
        }
    }
}