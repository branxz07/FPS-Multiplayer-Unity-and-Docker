using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class activatendeactivatefast : MonoBehaviour
{
    public static activatendeactivatefast Instance;
    // Start is called before the first frame update
    public string RoomNameSelect;
    public string RoomPassSelect;
    public GameObject passField;
    public TMP_InputField password;

    void Start()
    {
        
    }
    public bool press = false;
    // Update is called once per frame
    void Update()
    {
        if (press)
        {
            passField.SetActive(true);
        }
        //StartCoroutine(ActivatePasswordField());
        RoomPassSelect=password.text;
    }

    private IEnumerator ActivatePasswordField()
    {
        yield return new WaitForSeconds(.5f);
        print("byebye");
       
    }
}
