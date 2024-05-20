using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun; 
using TMPro;
public class PPlayerSetup : MonoBehaviour
{
    public Move move;
    public GameObject camera;
    public string nickname;
    public TextMeshPro nametag;
    public Transform TPweaponHolder;

    public void IsLocalPLayer()
    {
        TPweaponHolder.gameObject.SetActive(false);

        move.enabled = true;
        camera.SetActive(true);
    }

    [PunRPC]
    public void SetTPweapon (int _weaponIndex){
        foreach (Transform  _weapon in TPweaponHolder)
        {
            _weapon.gameObject.SetActive(false);
        }
        TPweaponHolder.GetChild(_weaponIndex).gameObject.SetActive(true);
    }



    [PunRPC]
    public void SetNickname(string _name){
        nickname = _name;

        nametag.text = nickname;
    }
}
