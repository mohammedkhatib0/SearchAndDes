﻿//SlapChickenGames
//2021
//Manager for weapon inventory and switching

using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HashTable = ExitGames.Client.Photon.Hashtable;

namespace scgFullBodyController
{
    public class GunManager : MonoBehaviourPunCallbacks
    {
        public GameObject[] weapons;
        public Animator anim;
        public OffsetRotation oRot;
        public float swapTime;
        int index = 0;
        PhotonView PV;
        private void Awake()
        {
            PV = GetComponent<PhotonView>();
        }
        void Start()
        {
            //Initialize each weapon and set state to swapping automatically so gun controller knows to setup weapon positions
            foreach (GameObject weapon in weapons)
            {
                weapon.GetComponent<GunController>().swapping = true;
            }
            /*
            The invoke timing here is based off the time it takes for the swap animation to complete + transition time,
            this is so that the weapon aiming position is based off where its first position is out of the swap anim
            */
            Invoke("setSwappedWeaponPositions", .567f + .25f);
        }

        void Update()
        {
            if (!PV.IsMine)
                return;
            //To add more weapons, just copy one of these blocks of code, add an else if, and change the keybind to the next one up ex., 
            //Aplha4, then set index to the corresponding key value such as 4
            if (Input.GetKeyDown(KeyCode.Alpha1) && index != 0)
            {
                if (!weapons[index].GetComponent<GunController>().firing && !weapons[index].GetComponent<GunController>().swapping
                    && !weapons[index].GetComponent<GunController>().aiming && weapons[index].GetComponent<GunController>().aimFinished
                    && !weapons[index].GetComponent<GunController>().reloading && !weapons[index].GetComponent<GunController>().cycling)
                {
                    index = 0;
                    Invoke("swapWeapons", swapTime);
                    foreach (GameObject weapon in weapons)
                    {
                        weapon.GetComponent<GunController>().swapping = true;
                    }
                    anim.SetBool("putaway", true);
                }
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2) && index != 1 && weapons.Length > 1)
            {
                if (!weapons[index].GetComponent<GunController>().firing && !weapons[index].GetComponent<GunController>().swapping
                    && !weapons[index].GetComponent<GunController>().aiming && weapons[index].GetComponent<GunController>().aimFinished
                    && !weapons[index].GetComponent<GunController>().reloading && !weapons[index].GetComponent<GunController>().cycling)
                {
                    {
                        index = 1;
                        Invoke("swapWeapons", swapTime);
                        foreach (GameObject weapon in weapons)
                        {
                            weapon.GetComponent<GunController>().swapping = true;
                        }
                        anim.SetBool("putaway", true);
                    }
                }
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3) && index != 2 && weapons.Length > 2)
            {
                if (!weapons[index].GetComponent<GunController>().firing && !weapons[index].GetComponent<GunController>().swapping
                    && !weapons[index].GetComponent<GunController>().aiming && weapons[index].GetComponent<GunController>().aimFinished
                    && !weapons[index].GetComponent<GunController>().reloading && !weapons[index].GetComponent<GunController>().cycling)
                {
                    {
                        index = 2;
                        Invoke("swapWeapons", swapTime);
                        foreach (GameObject weapon in weapons)
                        {
                            weapon.GetComponent<GunController>().swapping = true;
                        }
                        anim.SetBool("putaway", true);
                    }
                }
            }
        }
        void swapWeapons()
        {
            if (PV.IsMine)
            {
                //Set every other weapon except the one we want to swap to at index to false
                for (int i = 0; i < weapons.Length; i++)
            {
                if (i != index)
                {
                    weapons[i].SetActive(false);
                }
            }

            //Set desired weapon to active
            EquipItem(index);
            Invoke("setSwappedWeaponPositions", .567f + .25f);

            //Initliaze the correct spine rotation on the spine bone's orientation script
            if (weapons[index].GetComponent<GunController>().Weapon == GunController.WeaponTypes.Rifle)
            {
                oRot.rifle = true;
                oRot.pistol = false;
            }
            else if (weapons[index].GetComponent<GunController>().Weapon == GunController.WeaponTypes.Pistol)
            {
                oRot.rifle = false;
                oRot.pistol = true;
            }
            anim.SetBool("putaway", false);
           
                ExitGames.Client.Photon.Hashtable hash = new ExitGames.Client.Photon.Hashtable();
                hash.Add("itemIndex", index);
                PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
            }
        }
        public override void OnPlayerPropertiesUpdate(Player targetPlayer, HashTable changedProps)
        {
            if (!PV.IsMine && targetPlayer == PV.Owner)
            {
                EquipItem((int)changedProps["itemIndex"]);
            }
        }
        void setSwappedWeaponPositions()
        {
            //Initialize the correct original aim position if it is the first time swapping
            if (!weapons[index].GetComponent<GunController>().aimPosSet)
            {
                weapons[index].GetComponent<GunController>().initiliazeOrigPositions();
                weapons[index].GetComponent<GunController>().aimPosSet = true;
            }
            foreach (GameObject weapon in weapons)
            {
                weapon.GetComponent<GunController>().swapping = false;
            }
        }
        void EquipItem(int item)
        {   
           
            for (int i = 0; i < weapons.Length; i++)
            {
                if (weapons[i].gameObject != null)
                    weapons[i].SetActive(false);
            }
            if (weapons[item].gameObject!=null)
            weapons[item].SetActive(true);
        }
    }
}
