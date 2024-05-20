using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class RoomList : MonoBehaviourPunCallbacks
{
    public static RoomList Instance;

    public GameObject rooomManagerObject;
    public RoomManager roomManager;

    [Header("UI")]
    public Transform roomListParent; 
    public GameObject roomListItemPrefab;     
    public GameObject roomListItemPrefabPass; 
    public GameObject putPass; 
    private List<RoomInfo> cachedRoomList = new List<RoomInfo>();

    public void ChangeRoomToCreateName(string _roomname){
        roomManager.roomNameToJoin = _roomname;
    }
    public void ChangeRoomToPassword(string _password){
        Debug.Log(_password);
        roomManager.roomPassword = _password;
    }

    private void Awake(){
        Instance = this;
        photonView = GetComponent<PhotonView>(); // Assuming the PhotonView is attached to the same GameObject as RoomList
    }

    IEnumerator Start()
    {
        roomManager.roomPassword="";
        PhotonNetwork.EnableCloseConnection = true;
        if (PhotonNetwork.InRoom){
            PhotonNetwork.LeaveRoom();
            PhotonNetwork.Disconnect();
        }
        yield return new WaitUntil(() => !PhotonNetwork.IsConnected);
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster(){
        base.OnConnectedToMaster();
        PhotonNetwork.JoinLobby();
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList){
        if (cachedRoomList.Count <= 0){
            cachedRoomList = roomList;
        } else {
            foreach (var room in roomList){
                for (int i = 0; i < cachedRoomList.Count; i++){
                    if (cachedRoomList[i].Name == room.Name){
                        List<RoomInfo> newList = cachedRoomList;
                        if (room.RemovedFromList){
                            newList.Remove(newList[i]);
                        } else {
                            newList[1] = room;
                        }
                        cachedRoomList = newList;
                    }
                }
            }
        }
        UpdateUI();
    }
    public PhotonView photonView;



void UpdateUI()
{
    // Destruir los elementos previos
    foreach (Transform roomItem in roomListParent)
    {
        Destroy(roomItem.gameObject);
    }

    // Iterar a través de cachedRoomList
    foreach (var room in cachedRoomList)
    {
        GameObject roomItem; // Inicializar la variable aquí

        // Iterar a través de las claves de CustomProperties
        foreach (var key in room.CustomProperties.Keys)
        {
            if (key.ToString() == "password")
            {
                roomItem = Instantiate(roomListItemPrefabPass, roomListParent);
                roomItem.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = room.Name;
                roomItem.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = room.PlayerCount + "/6";
                roomItem.GetComponent<RoomItemButton>().RoomName = room.Name;
                break;
            }else
            {
                Debug.Log("No tiene Keys");
                roomItem = Instantiate(roomListItemPrefab, roomListParent);
                roomItem.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = room.Name;
                roomItem.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = room.PlayerCount + "/6";
                roomItem.GetComponent<RoomItemButton>().RoomName = room.Name;
            }
        }
       

        // Asignar valores a los textos de los prefabs
        

        // // Imprimir el valor de la propiedad "password" si existe
        // if (room.CustomProperties.ContainsKey("password"))
        // {
        //     var passwordValue = room.CustomProperties["password"];
        //     Debug.Log("Password value: " + passwordValue);

        //     // Activar putPass si no es null
        //     if (putPass != null)
        //     {
        //         Debug.Log("Activating putPass");
        //         putPass.SetActive(true);
        //     }
        //     else
        //     {
        //         Debug.LogError("putPass is not assigned.");
        //     }
        // }
    }
}


    public void JoinRoomByName(string _name){
        roomManager.roomNameToJoin = _name;
        rooomManagerObject.SetActive(true);
        gameObject.SetActive(false);
    }
    public void JoinRoomByNameCoded(string _name, string _password){
        foreach (var room in cachedRoomList)
        {
            GameObject roomItem; // Inicializar la variable aquí

            if (room.Name == _name)
            {
                // Iterar a través de las claves de CustomProperties
                foreach (var key in room.CustomProperties.Keys)
                {
                    // Verificar si la clave es "password" y si la contraseña coincide
                    if (key.ToString() == "password" && _password == room.CustomProperties[key].ToString())
                    {
                        roomManager.roomNameToJoin = _name;
                        rooomManagerObject.SetActive(true);
                        gameObject.SetActive(false);
                        break;
                    }
                    else
                    {
                        Debug.Log("Contraseña Incorrecta");
                    }
                }
            }
        }
    }
    
    // public void KickPlayerByName(string playerName)
    // {
       
    //     if (PhotonNetwork.LocalPlayer.IsMasterClient)
    //     {
    //         photonView.RPC("KickPlayer", RpcTarget.MasterClient, playerName);
    //     }
    // }

}
