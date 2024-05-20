using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;


public class Menu : MonoBehaviour
{
    void Start(){
        
    }
    public void Main()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
    public void PlayGame(){
        SceneManager.LoadScene(1);
    }
    public void Quit(){
        Application.Quit();
    }
    
}