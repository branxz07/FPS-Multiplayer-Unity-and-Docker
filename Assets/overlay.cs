using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class overlay : MonoBehaviour
{
    public GameObject Lobby;
    public activatendeactivatefast lobby;
    public TextMeshProUGUI roomNameValue;
    void Start()
    {
      
    }
    void Update()
    {
       Lobby = GameObject.Find("LobbyINFO");
       lobby = Lobby.gameObject.GetComponent<activatendeactivatefast>();
    }
    
    public void ButonClcick(){
        lobby.press = true;
        lobby.RoomNameSelect = roomNameValue.text;
    }
}
