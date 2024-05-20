using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using TMPro;
public class leaderboard : MonoBehaviour
{
    public GameObject Winner;
    public GameObject Winnner;
    public TextMeshProUGUI Winnnner;
    public GameObject playersHolder;
    [Header("Options")]
    public float refreshRate = 1f;
    [Header("UI")]
    public GameObject[] slots;
    [Space]
    public TextMeshProUGUI[] scoreTexts;
    public TextMeshProUGUI[] nameText;


    private void Start(){
        InvokeRepeating(nameof(Refresh), 1f,refreshRate);
    }
    public void Refresh(){
        foreach (var slot in slots)
        {
            slot.SetActive(false);

        }

        var sortedPlayerList = (from player in PhotonNetwork.PlayerList orderby player.GetScore() descending select player).ToList();

        int i = 0;
        foreach (var player in sortedPlayerList)
        {
            slots[i].SetActive(true);
            if (player.NickName=="")
            {
                player.NickName="Anonimus";
            }
            
            nameText[i].text = player.NickName;
            scoreTexts[i].text = player.GetScore().ToString();
            if (scoreTexts[i].text == "20")
            {
                Winner.SetActive(true);
                Winnnner.text = nameText[i].text + " won !!!";
                Time.timeScale = 0;
                break;
            }
            i++;
        }
    

    }

private bool canToggle = true;

    private void Update(){
        if (Winner.activeSelf)
        {
            Winnner = GameObject.FindWithTag("roomList");
            Winnnner = Winnner.GetComponent<TextMeshProUGUI>();
        }
        playersHolder.SetActive(Input.GetKey(KeyCode.Tab));
        
    }
}
