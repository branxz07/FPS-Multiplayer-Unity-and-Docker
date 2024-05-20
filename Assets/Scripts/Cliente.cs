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
public class Cliente : MonoBehaviour
{
    const int PORT = 5555;
    const int MAX_BUFFER = 1024;
    const int TIMEOUT = 5;

    public GameObject menu;
    public GameObject auth;
    public GameObject play;
    public GameObject[] error;
    public GameObject Username;
    public GameObject Password;
    public GameObject LgIn;
    public GameObject snIn;
    public GameObject lgOut;
    private bool login=false;
    public TMP_InputField  User;
    public TMP_InputField  Pass;
    public GameObject seingMal;
    public GameObject seingBien;
    public GameObject AboutUS;
    public GameObject HowTP;
    public TMP_InputField  RegUser;
    public TMP_InputField  RegPass;
    public TMP_InputField  RegPassVal;
    public bool conected=true;
    private GameObject sameObjectName;
    private bool p=false;
    private string serverIpAddress = "10.7.26.28";
    private float tiempoInicio;
    private string nameuser="SinUsuario";
    public string FianlUser;
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    private void Start()
    {
        DontDestroyOnLoad(gameObject); // Asegúrate de que este objeto no se destruya al cambiar de escena
        tiempoInicio = Time.time;


    }
    public GameObject inGame;
    public GameObject yo;
    private void Update(){
        inGame = GameObject.Find("Sapwns");
        RoomManager.instance.ChangeNickName(FianlUser);
        Debug.Log(FianlUser);
        if (inGame.activeSelf)
        {
            yo.SetActive(false);
        }
        
        float tiempoTranscurrido = Time.time - tiempoInicio;
        if (tiempoTranscurrido >= 5f)
        {
            TryConnectToServer(serverIpAddress);
            tiempoInicio = Time.time;
            string act= Log();
            int ans=SendRequestToServer(serverIpAddress,act);
            checkAnswerServer(ans);
        }
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (p==true)
        {
            sameObjectName = GameObject.Find("Canvas");
            Destroy(sameObjectName);
        }
        if (currentSceneIndex != 0 && conected)
        {
            p=true;
            this.gameObject.name="ServerConection";
            error[1].SetActive(false);
            error[0].SetActive(false);
            error[2].SetActive(false);
            error[3].SetActive(false);;
            auth.SetActive(false);
            play.SetActive(false);
            Username.SetActive(false);
            Password.SetActive(false);
            menu.SetActive(false);
        }
        // Verificar si la escena actual no es la escena 0
        if (currentSceneIndex != 0 && !conected)
        {
            // Cargar la escena 0
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene(0);
            menu.SetActive(true);
            error[1].SetActive(true);
            error[0].SetActive(false);
            error[2].SetActive(false);
            error[3].SetActive(false);
            auth.SetActive(false);
            play.SetActive(false);
            Username.SetActive(false);
            Password.SetActive(false);
            HowTP.SetActive(false);
            AboutUS.SetActive(false);

        }
        if (!conected)
        {
            error[1].SetActive(true);
            error[0].SetActive(false);
            error[2].SetActive(false);
            error[3].SetActive(false);
            auth.SetActive(false);
            play.SetActive(false);
            Username.SetActive(false);
            Password.SetActive(false);
        }
        else
        {
            error[1].SetActive(false);
           
        }
    }

    public GameObject[] UIs;
    public void LogOut()
    {
        
        
        lgOut.SetActive(false);
        auth.SetActive(true);
        play.SetActive(false);
        Username.SetActive(true);
        Password.SetActive(true);
        User.text="";
        Pass.text="";
        login=false;
    }

    public void SignIn()
    {
        error[5].SetActive(false);
        error[4].SetActive(false);
        error[2].SetActive(false);
        error[3].SetActive(false);
        if (RegUser.text=="" || RegPass.text=="" || RegPassVal.text=="")
        {
            error[5].SetActive(true);
        
        }else
        {
            //debug.Log("Comprobando SignIn inicio");
            Availavility();
            //debug.Log("Comprobando SignIn fin");
            RegUser.text="";
            RegPass.text="";
            RegPassVal.text="";
        }
    }

    public void SignInChange()
    {
        error[0].SetActive(false);
        RegUser.text="";
        RegPass.text="";
        RegPassVal.text="";
        snIn.SetActive(true);
        LgIn.SetActive(false);
    }

    public void LogInChange()
    {
        error[5].SetActive(false);
        error[4].SetActive(false);
        error[2].SetActive(false);
        error[3].SetActive(false);
        User.text="";
        Pass.text="";
        snIn.SetActive(false);
        LgIn.SetActive(true);
    }

    public string gt;
    public void LogIn()
    {
        if (User.text=="" || Pass.text=="")
        {
            error[0].SetActive(true);
        }
        else
        {
            FianlUser = User.text;
            error[0].SetActive(false);
            string instruction= $"LogIn {User.text} {Pass.text}";
            //debug.Log($"Mando al server: {instruction}");
            int ans=SendRequestToServer(serverIpAddress,instruction);
            checkAnswerServer(ans);
            if(login){
                lgOut.SetActive(true);
                auth.SetActive(false);
                play.SetActive(true);
                Username.SetActive(false);
                Password.SetActive(false);
                nameuser=User.text;
                gt = User.text;
                
                //debug.Log($"El usuario es: {nameuser}");
            }
            else{
                auth.SetActive(true);
                play.SetActive(false);
                Username.SetActive(true);
                Password.SetActive(true);
                nameuser="";
            }
            User.text="";
            Pass.text="";
        }
    }

    private void Availavility()
    {
        seingMal.SetActive(false);
        seingBien.SetActive(true);
        string inputUsername = RegUser.text;
        string inputPassword = RegPass.text;
        string inputPasswordVal = RegPassVal.text;
        error[5].SetActive(false);
        error[4].SetActive(false);
        error[3].SetActive(false);
        error[2].SetActive(false);
        if (inputPassword==inputPasswordVal)
        {
            error[3].SetActive(false);
            string instruction="SignIn " + inputUsername + " " + inputPassword;
            int ans=SendRequestToServer(serverIpAddress,instruction);
            //debug.Log($"Mande al server fin {ans}");
            checkAnswerServer(ans);
        }else
        {
            RegUser.text="";
            RegPass.text="";
            RegPassVal.text="";
            error[5].SetActive(false);
            error[4].SetActive(false);
            error[2].SetActive(false);
            error[3].SetActive(true);
        }
    }
    public void TryConnectToServer(string ipAddress)
    {
        TcpClient client = null;
        try
        {
            client = new TcpClient();
            client.SendTimeout = TIMEOUT * 1000;
            client.ReceiveTimeout = TIMEOUT * 1000;
            IPEndPoint remoteEP = new IPEndPoint(IPAddress.Parse(ipAddress), PORT);
            client.Connect(remoteEP);
            if (client.Connected)
            {
                conected=true;
                //debug.Log("Conectado al server");
            }
            else
            {
                conected=false;
            }
        }
        catch (Exception ex)
        {
            conected=false;
            Debug.Log($"Error al conectar con el servidor: {ex.Message}");
        }
        finally
        {
            if (client != null)
            {
                client.Close();
            }
        }
    }

    public int SendRequestToServer(string ip, string request)
    {
        TcpClient client = null;
        NetworkStream stream = null;
        try
        {
            client = new TcpClient();
            client.SendTimeout = TIMEOUT * 1000;
            client.ReceiveTimeout = TIMEOUT * 1000;
            IPAddress ipAddress = IPAddress.Parse(ip);
            IPEndPoint remoteEP = new IPEndPoint(ipAddress, PORT);
            client.Connect(remoteEP);
            if (client.Connected)
            {
                stream = client.GetStream();
                byte[] buffer = System.Text.Encoding.ASCII.GetBytes(request);
                //debug.Log($"Mande al server: {request}");
                stream.Write(buffer, 0, buffer.Length);
                byte[] responseBuffer = new byte[MAX_BUFFER];
                int bytesRead = stream.Read(responseBuffer, 0, responseBuffer.Length);
                string response = System.Text.Encoding.ASCII.GetString(responseBuffer, 0, bytesRead);
                //debug.Log($"Obtuve de server: {response}");
                int result;
                if (!string.IsNullOrEmpty(response))
                {
                    int startIndex = response.IndexOf('\'') + 1;
                    int endIndex = response.IndexOf('\'', startIndex);
                    if (endIndex > startIndex)
                    {
                        string numberString = response.Substring(startIndex, endIndex - startIndex);
                        if (int.TryParse(numberString, out result))
                        {
                            return result;
                        }
                    }
                }
                return 0;
            }
            else
            {
                return 33; 
            }
        }
        catch (Exception ex)
        {
            Debug.Log($"Error: {ex.Message}");
            return 33;
        }
        finally
        {
            if (stream != null)
            {
                stream.Close();
            }
            if (client != null)
            {
                client.Close();
            }
        }
    }
    public void checkAnswerServer(int ans){
        //debug.Log($"{ans}");
        if(ans==-1 || ans==0){
            //debug.Log($"Soy {ans}");
            login = false;
        }else if(ans==76){ //nombre en uso
            //debug.Log("Soy 76");
            RegUser.text="";
            RegPass.text="";
            RegPassVal.text="";
            error[2].SetActive(true);
            login = false; 
        }else if(ans==87){ //contraseña vunerable (alguien ya la uso)
            //debug.Log("Soy 87");
            login = false; 
            RegUser.text="";
            RegPass.text="";
            RegPassVal.text="";
            error[5].SetActive(true);
        }else if(ans==5){
            //debug.Log("Soy 5");
            error[0].SetActive(false);
            error[5].SetActive(false);
            error[4].SetActive(true);
            error[3].SetActive(false);
            error[2].SetActive(false);
            login = true;
        }else if(ans==33){
            //debug.Log("Soy 33");
            error[1].SetActive(true);
            error[0].SetActive(false);
            error[2].SetActive(false);
            error[3].SetActive(false);
            auth.SetActive(false);
            play.SetActive(false);
            Username.SetActive(false);
            Password.SetActive(false);
        }else{
            login = false;
            User.text="";
            Pass.text="";
            RegUser.text="";
            RegPass.text="";
            RegPassVal.text="";
            error[0].SetActive(false);
        }
    }
    private string GetIPAddress()
    {
        NetworkInterface[] interfaces = NetworkInterface.GetAllNetworkInterfaces();
        foreach (NetworkInterface iface in interfaces)
        {
            if (iface.OperationalStatus == OperationalStatus.Up && (iface.NetworkInterfaceType == NetworkInterfaceType.Ethernet || iface.NetworkInterfaceType == NetworkInterfaceType.Wireless80211))
            {
                foreach (UnicastIPAddressInformation ip in iface.GetIPProperties().UnicastAddresses)
                {
                    if (ip.Address.AddressFamily == AddressFamily.InterNetwork && !IPAddress.IsLoopback(ip.Address))
                    {
                        return ip.Address.ToString();
                    }
                }
            }
        }
        return "V.A.C.I.O";
    }
    private string GetCurrentDateTime()
    {
        DateTime now = DateTime.Now;
        return string.Format("{0:D2}:{1:D2}:{2:D2}-{3:D2}-{4:D2}-{5:D4}", 
            now.Hour, now.Minute, now.Second, now.Day, now.Month, now.Year);
    }
    public string Log()
    {
        string ipAddress = GetIPAddress();
        string dateTime = GetCurrentDateTime();
        //debug.Log($"{dateTime}");
        if (nameuser == ""){
            nameuser="SinUsuario";
        }
        string logMessage = "Activity " + dateTime + " " + ipAddress + " " + nameuser;
        return logMessage;
    }
}




/*
error[0]= contraseñas o nombre mal
error[1]= problemas con server
error[2]= nombre en uso
error[3]= contraseñas no coinciden
error[4]= SignIn correcto
error[5]= Algo esta mal
*/