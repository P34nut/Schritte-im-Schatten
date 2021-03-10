using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class passwort : MonoBehaviour {

    private string pass = "Hallo";
    public Texture2D test;
    public Texture2D test2;

    // Use this for initialization
    void Start () {

        Cursor.SetCursor(test, Vector2.zero, CursorMode.ForceSoftware);
        //Cursor.visible = false;

    }


    // Update is called once per frame
    void Update() {

        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    if (Time.timeScale == 1)
        //    {
        //        Time.timeScale = 0;
        //    }
        //    else if (Time.timeScale == 0)
        //    {
        //        Debug.Log("high");
        //        Time.timeScale = 1;
        //    }
        //}
    }

    public void changeCursorSet()
    {
        Cursor.SetCursor(test2,Vector2.zero,CursorMode.ForceSoftware);
    }

    public void exitCursor()
    {
        Cursor.SetCursor(test, Vector2.zero, CursorMode.ForceSoftware);
    }

    //public void check()
    //{
    //    if( this.GetComponent<InputField>().text == pass)
    //    {
    //        Debug.Log("richtig");
    //    }
    //    else
    //    {
    //        Debug.Log("falsch");
    //    }
    //}
}
