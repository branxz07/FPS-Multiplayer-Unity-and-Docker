using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class RoomManager : MonoBehaviourPunCallbacks
{
    public static RoomManager instance;
    public GameObject player;
    [Space]
    public Transform[] spawn;
    [Space]
    public GameObject roomCam;
    [Space]
    public GameObject nameUI;
    public GameObject connectingUI;
    private string nickname = "Anonymous";
    public string roomNameToJoin = "test";
    public string roomPassword = "";

    private List<Player> playersInRoom = new List<Player>();

    void Awake(){
        instance = this;
    }

    public void ChangeNickName(string _name){
        nickname = _name;
    }

    public void JoinRoomBtnPress(){
        Debug.Log("Connecting...");
        Debug.Log(roomPassword);
        RoomOptions roomOptions = new RoomOptions { MaxPlayers = 6 };
        if (roomPassword != "")
        {
            roomOptions.CustomRoomProperties = new ExitGames.Client.Photon.Hashtable() { { "password", roomPassword } };
            roomOptions.CustomRoomPropertiesForLobby = new string[] { "password" };
            Debug.Log(roomNameToJoin +"  Nueva pass: " + roomPassword);
        }else
        {
            roomOptions.CustomRoomProperties = new ExitGames.Client.Photon.Hashtable() { { "NoPassword", roomPassword } };
            roomOptions.CustomRoomPropertiesForLobby = new string[] { "NoPassword" };
            Debug.Log(roomNameToJoin +"  No pass: " + roomPassword);
        }
        
        PhotonNetwork.JoinOrCreateRoom(roomNameToJoin,  roomOptions, TypedLobby.Default);
        

        nameUI.SetActive(false);
        connectingUI.SetActive(true);
    }

    public override void OnJoinedRoom(){
        base.OnJoinedRoom();
        Debug.Log("We're connected and in a room now");
        roomCam.SetActive(false);
        playersInRoom.Add(PhotonNetwork.LocalPlayer);
        RespawnPlayer();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer){
        base.OnPlayerEnteredRoom(newPlayer);
        playersInRoom.Add(newPlayer);
        Debug.Log(newPlayer.NickName + " has joined the room.");
    }

    public override void OnPlayerLeftRoom(Player otherPlayer){
        base.OnPlayerLeftRoom(otherPlayer);
        playersInRoom.Remove(otherPlayer);
        Debug.Log(otherPlayer.NickName + " has left the room.");
    }

    public void RespawnPlayer(){
        Transform spawns = spawn[UnityEngine.Random.Range(0, spawn.Length)];
        GameObject _player = PhotonNetwork.Instantiate(player.name, spawns.position, Quaternion.identity);
        _player.GetComponent<PPlayerSetup>().IsLocalPLayer();
        _player.GetComponent<Health>().IsLocalPLayer = true;
        _player.GetComponent<PhotonView>().RPC("SetNickname", RpcTarget.AllBuffered, nickname);
        PhotonNetwork.LocalPlayer.NickName = nickname;
    }

[PunRPC]
public void KickPlayer(string playerNameToKick)
{
    Debug.Log("Trying to kick " + playerNameToKick);
    if (PhotonNetwork.LocalPlayer.IsMasterClient)
    {
        Player playerToKick = null;
        foreach (Player player in PhotonNetwork.PlayerList)
        {
            if (player.NickName == playerNameToKick)
            {
                playerToKick = player;
                break;
            }
        }

        if (playerToKick != null)
        {
            //Lobby.SetActive(true);

            
            // Expulsar al jugador de la sala
            PhotonNetwork.CloseConnection(playerToKick);
            Debug.Log("Player kicked: " + playerNameToKick);

        }
        else
        {
            Debug.LogWarning("Player " + playerNameToKick + " not found in the room.");
        }
    }
    else
    {
        Debug.LogWarning("Only the MasterClient can kick players from the room.");
    }
    
}
   

}
