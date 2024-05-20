using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Net.NetworkInformation;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.IO;
public class ClienteUDP : MonoBehaviour
{
    const int PORT = 6666;
    const int BUFFER_SIZE = 1024;
    const int TIMEOUT = 5;
    public bool conectedUDP=true;
    public Text txt1;
    public Text txt2;
    public Text txt3;
    public Text txt4;
    public Text txt5;
    public Text txt6;
    private string serverIpAddress = "192.168.0.4";
    private string filename = "HowToPlay.txt";
    private float tiempoInicio;
    

    
    private void Start()
    {
        tiempoInicio = Time.time;
        txt1.text="Servicio no disponible";
        txt2.text="Servicio no disponible";
        txt3.text="Servicio no disponible";
        txt4.text="Servicio no disponible";
        txt5.text="Servicio no disponible";
        txt6.text="Servicio no disponible";
    }
    private void Update(){
        float tiempoTranscurrido = Time.time - tiempoInicio;
        if (tiempoTranscurrido >= 5f)
        {   
            int conect=TryConnectToServer(serverIpAddress);
            if(conect==1){
                TryConnectToServer(serverIpAddress, filename);
                FileInstruction(filename);
            }else{
                txt1.text="Servicio no disponible";
                txt2.text="Servicio no disponible";
                txt3.text="Servicio no disponible";
                txt4.text="Servicio no disponible";
                txt5.text="Servicio no disponible";
                txt6.text="Servicio no disponible";
            }
            tiempoInicio = Time.time;
        }
    }

    public void TryConnectToServer(string ip, string filename)
    {
        try
        {
            Socket sockfd = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            IPEndPoint servaddr = new IPEndPoint(IPAddress.Parse(ip), PORT);
            sockfd.SendTo(System.Text.Encoding.ASCII.GetBytes(filename), servaddr);
            FileStream file = new FileStream(filename, FileMode.Create);
            byte[] buffer = new byte[BUFFER_SIZE];
            int bytesReceived;
            do
            {
                bytesReceived = sockfd.Receive(buffer);
                if (bytesReceived == 0)
                    break;
                string data = System.Text.Encoding.ASCII.GetString(buffer, 0, bytesReceived);
                if (data == "FIN")
                {
                    break;
                }
                file.Write(buffer, 0, bytesReceived);
            } while (true);
            Debug.Log("Archivo recibido del servidor.");
            file.Close();
            sockfd.Close();
        }
        catch (Exception ex)
        {
            Debug.Log($"Error al conectar con el servidor: {ex.Message}");
        }
    }

    public int TryConnectToServer(string ip)
    {
        try
        {
            Socket sockfd = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            IPEndPoint servaddr = new IPEndPoint(IPAddress.Parse(ip), PORT);
            sockfd.Connect(servaddr);
            sockfd.Close();
            return 1; // Conexión exitosa

        }
        catch (Exception ex)
        {
            Debug.Log($"Error al conectar con el servidor: {ex.Message}");
            txt1.text="Servicio no disponible";
            txt2.text="Servicio no disponible";
            txt3.text="Servicio no disponible";
            txt4.text="Servicio no disponible";
            txt5.text="Servicio no disponible";
            txt6.text="Servicio no disponible";
            return 0; // Error al conectar
        }
    }

    public void PrintFileContent(string filename)
    {
        try
        {
            using (StreamReader reader = new StreamReader(filename))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    Debug.Log(line);
                }
            }
        }
        catch (Exception ex)
        {
            Debug.Log($"Error al leer el archivo: {ex.Message}");
            txt1.text="Servicio no disponible";
            txt2.text="Servicio no disponible";
            txt3.text="Servicio no disponible";
            txt4.text="Servicio no disponible";
            txt5.text="Servicio no disponible";
            txt6.text="Servicio no disponible";
        }
    }

    public void FileInstruction(string filename)
    {
        try
        {
            using (StreamReader reader = new StreamReader(filename))
            {   
                txt1.text=reader.ReadLine();
                txt2.text=reader.ReadLine();
                txt3.text=reader.ReadLine();
                txt4.text=reader.ReadLine();
                txt5.text=reader.ReadLine();
                txt6.text=reader.ReadLine(); 
            }
        }
        catch (Exception ex)
        {
            Debug.Log($"Error al leer el archivo: {ex.Message}");
            txt1.text="Servicio no disponible";
            txt2.text="Servicio no disponible";
            txt3.text="Servicio no disponible";
            txt4.text="Servicio no disponible";
            txt5.text="Servicio no disponible";
            txt6.text="Servicio no disponible";
        }
    }
}

