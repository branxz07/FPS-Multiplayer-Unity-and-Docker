using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun; // Add this line
using TMPro;
public class Health : MonoBehaviour
{
    // Start is called before the first frame update
    public int health;
    public bool IsLocalPLayer;


    [Header("UI")]
    public TextMeshProUGUI healthText;

    [PunRPC]
    public void takeDamage(int _damage){
        health -= _damage;

        healthText.text = health.ToString();
        if (health<=0)
        {
            if (IsLocalPLayer)
            {
                Debug.Log("Es local");
                RoomManager.instance.RespawnPlayer();
            }
            
            Destroy(gameObject);
        }
    }
}
