using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Testeos : MonoBehaviourPunCallbacks // Añadir MonoBehaviourPunCallbacks para acceder a los eventos de Photon
{
    public GameObject Lobby;

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("holaaa");
        base.OnDisconnected(cause);

        // Activa el objeto deseado
        Lobby.SetActive(true);
    }
}
