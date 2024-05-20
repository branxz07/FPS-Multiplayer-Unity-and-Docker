using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class WeaponSwitcher : MonoBehaviour
{
    public PhotonView playerSetupView;
    public Animation _animation;
    public AnimationClip draw;
    private int selectetWeapon = 0;
    // Start is called before the first frame update
    void Start()
    {
        SelectWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        int previousSelectedWeapon = selectetWeapon;

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectetWeapon = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            selectetWeapon = 1;
        }


        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (selectetWeapon >= transform.childCount - 1)
            {
                selectetWeapon = 0;
            }else
            {
                selectetWeapon += 1;
            }
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (selectetWeapon<= 0)
            {
                selectetWeapon = transform.childCount - 1;
            }else
            {
                selectetWeapon -= 1;
            }
        }


        if (previousSelectedWeapon != selectetWeapon)
        {
            SelectWeapon();
        }
    }


    void SelectWeapon(){

        playerSetupView.RPC("SetTPweapon", RpcTarget.All, selectetWeapon);
        _animation.Stop();
        _animation.Play(draw.name);

        int i = 0;
        foreach (Transform _weapon in transform)
        {
            if (i == selectetWeapon)
            {
                _weapon.gameObject.SetActive(true);
            }else
            {
                _weapon.gameObject.SetActive(false);
            }
            i++;
        }
    }
}
