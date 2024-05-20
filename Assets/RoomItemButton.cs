using UnityEngine;
using TMPro;

public class RoomItemButton : MonoBehaviour
{
    public string RoomName;
    public string RoomCode;
    public activatendeactivatefast lobby;
    public GameObject Lobby;
    public void OnJoinRoomButtonClick(){
        RoomList.Instance.JoinRoomByName(RoomName);
    }
    public void OnJoinRoomButtonClickCode(){
        lobby = Lobby.gameObject.GetComponent<activatendeactivatefast>();
        RoomName =  lobby.RoomNameSelect;
        RoomCode = lobby.RoomPassSelect;

        Debug.Log("The room name n pass sended : " + RoomName + "  1  " + RoomCode);

        RoomList.Instance.JoinRoomByNameCoded(RoomName,RoomCode);
    }
    public void OnKickPlayerButtonClick(TextMeshProUGUI playerNameText){
        string playerName = playerNameText.text;
        RoomManager.instance.KickPlayer(playerName);
    }
}
