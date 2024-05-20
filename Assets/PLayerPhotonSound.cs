using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class PLayerPhotonSound : MonoBehaviour
{
    public AudioSource footStepsSource;
    public AudioClip footstepsSFX;


    public AudioSource gunShootSource;
    public AudioClip[] allGunShootSFX;

    public AudioSource gunReloadSource;
    public AudioClip[] allGunReloadSFX;

    public void PlayFootstaps(){
        GetComponent<PhotonView>().RPC("PlayFootstaps_RPC", RpcTarget.All);
    }


    [PunRPC]
    public void PlayFootstaps_RPC(){
        footStepsSource.clip = footstepsSFX;

        //Pitch Volume
        footStepsSource.pitch = UnityEngine.Random.Range(0.7f,1.2f);
        footStepsSource.volume = UnityEngine.Random.Range(0.2f,0.35f);

        footStepsSource.Play();
    }
    public void PlayShootSFX(int index){
        GetComponent<PhotonView>().RPC("PlayShootSFX_RPC", RpcTarget.All, index);
    }
    [PunRPC]
    public void PlayShootSFX_RPC(int index){
        gunShootSource.clip = allGunShootSFX[index];

        //Pitch Volume
        gunShootSource.pitch = UnityEngine.Random.Range(0.7f,1.2f);
        gunShootSource.volume = UnityEngine.Random.Range(0.2f,0.35f);

        gunShootSource.Play();
    }


    public void PlayReloadSFX(int index){
        GetComponent<PhotonView>().RPC("PlayReloadSFX_RPC", RpcTarget.All, index);
    }
    [PunRPC]
    public void PlayReloadSFX_RPC(int index){
        gunReloadSource.clip = allGunReloadSFX[index];

        gunReloadSource.Play();
    }


}
