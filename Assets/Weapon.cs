using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun; // Add this line
using TMPro;
using Photon.Pun.UtilityScripts;
public class Weapon : MonoBehaviour
{
    
    public int damage;
    public  Camera camera;
    public float fireRate;

[Header("VFX")]
public GameObject hitVFX;
    
    [Header("Ammo")]
    public int mag = 5;
    public int ammo = 30;
    public int magAmmo = 30;
    private float nextFire;


    [Header("SFX")]
    public int shootSFXIndex = 0;
    public PLayerPhotonSound pLayerPhotonSound;

    [Header("UI")]
    public TextMeshProUGUI magText;
    public TextMeshProUGUI ammoText;


    [Header("Animation")]
    public Animation animation;
    public AnimationClip reload;


    [Header("RecoilSettings")]
    // [Range(0,1)]
    // public float recoilPercent = 0.3f;
    [Range(0,2)]
    public float recorPercent = 0.7f;
    [Space]
    public float recoilUP = 1f;
    public float recoilBack = 0f;

    private Vector3 originalPosition;
    private Vector3 recoilVelocity = Vector3.zero;


    private float recoilLenght;
    private float recoverLenght;


    private bool recoiling;
    private bool recovering;


    
    void Start(){
        
        magText.text = mag.ToString();
        ammoText.text = ammo + "/" + magAmmo;

        originalPosition = transform.localPosition;

        recoilLenght = 0;
        recoverLenght = 1/fireRate * recorPercent;
    }
    // Update is called once per frame
    void Update()
    {
        if (nextFire > 0)
        {
            nextFire -= Time.deltaTime;
        }
        if(Input.GetButton("Fire1") && nextFire <=0  && ammo > 0 && animation.isPlaying == false){
            nextFire = 1 / fireRate;
            ammo--;

            magText.text = mag.ToString();
            ammoText.text = ammo + "/" + magAmmo;
            Fire();
        }

        if (Input.GetKeyDown(KeyCode.R) && ammo < magAmmo && mag>0)
        {
            Reload();
        }


        if (recoiling)
        {
            Recoil();
        }
        if (recovering)
        {
            Recover();
        }
    }

void Reload(){
    pLayerPhotonSound.PlayReloadSFX(shootSFXIndex);
    animation.Play(reload.name);
    if (mag > 0)
    {
        mag--;
        ammo = magAmmo;
    }
    magText.text = mag.ToString();
    ammoText.text = ammo + "/" + magAmmo;

}

    void Fire(){

    recoiling= true;
    recovering = false;


    pLayerPhotonSound.PlayShootSFX(shootSFXIndex);

        Ray ray = new Ray(camera.transform.position, camera.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray.origin, ray.direction, out hit, 100f))
        {
            
            PhotonNetwork.Instantiate(hitVFX.name , hit.point, Quaternion.identity);

            if (hit.transform.gameObject.GetComponent<Health>())
            {
                
                if (damage > hit.transform.gameObject.GetComponent<Health>().health)
                {
                    //Kill
                    PhotonNetwork.LocalPlayer.AddScore(1);
                }
                
                hit.transform.gameObject.GetComponent<PhotonView>().RPC("takeDamage",RpcTarget.All, damage);
            }
        }
    }


    void Recoil(){
        Vector3 finalPos = new Vector3(originalPosition.x,originalPosition.y+recoilUP, originalPosition.z - recoilBack);
        
        
        transform.localPosition = Vector3.SmoothDamp(transform.localPosition, finalPos, ref recoilVelocity, recoilLenght);


        if (transform.localPosition == finalPos)
        {
            recoiling = false;
            recovering = true;
        }
    
    
    }
    void Recover(){
        Vector3 finalPos = originalPosition;
        
        
        transform.localPosition = Vector3.SmoothDamp(transform.localPosition, finalPos, ref recoilVelocity, recoverLenght);


        if (transform.localPosition == finalPos)
        {
            recoiling = false;
            recovering = false;
        }
    
    
    }

}
