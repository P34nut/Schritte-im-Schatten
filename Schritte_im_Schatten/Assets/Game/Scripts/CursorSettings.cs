using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CursorSettings : MonoBehaviour {

    public Texture2D menuCursor;
    public Texture2D defaultCursor;
    public Texture2D takeCursor;
    public Texture2D lookCursor;
    public Texture2D QTECursor;
    public Texture2D leftRightCursor;
    public Texture2D walkCursor;
    public Texture2D leftCursor;
    public Texture2D rightCursor;
    public Texture2D backCursor;
    public Texture2D vorCursor;

    public Vector2 hotSpot;

    private void Awake()
    {
        hotSpot = new Vector2(22.5f, 22.5f);
    }

    // Use this for initialization
    void Start () {
        
        Cursor.lockState = CursorLockMode.Confined;

        if(SceneManager.GetActiveScene().buildIndex == 0)
        {
            setMenu();
        }
        else
        {
            setDefault();
        }
        
    }

    public void setMenu()
    {
        Cursor.SetCursor(menuCursor, Vector2.zero, CursorMode.ForceSoftware);
    }

    public void setWalk()
    {
        Cursor.SetCursor(walkCursor, hotSpot, CursorMode.ForceSoftware);
    }

    public void setLook()
    {
        Cursor.SetCursor(lookCursor, hotSpot, CursorMode.ForceSoftware);
    }

    public void setTake()
    {
        Cursor.SetCursor(takeCursor, hotSpot, CursorMode.ForceSoftware);
    }

    public void setQTE()
    {
        Cursor.SetCursor(QTECursor, hotSpot, CursorMode.ForceSoftware);
    }

    public void setLeftRight(Image image)
    {

        if (image.sprite != null)
        {
            if (image.sprite.name == "visi_1x1")
            {
                setTake();
            }
            else
            {
                Cursor.SetCursor(leftRightCursor, hotSpot, CursorMode.ForceSoftware);
            }
            
        }
       
    }

    public void setDefault()
    {
        Cursor.SetCursor(defaultCursor, hotSpot, CursorMode.ForceSoftware);
    }

    public void setLinks()
    {
        Cursor.SetCursor(leftCursor, hotSpot, CursorMode.ForceSoftware);
    }

    public void setRechts()
    {
        Cursor.SetCursor(rightCursor, hotSpot, CursorMode.ForceSoftware);
    }

    public void setVor()
    {
        Cursor.SetCursor(vorCursor, hotSpot, CursorMode.ForceSoftware);
    }

    public void setBack()
    {
        Cursor.SetCursor(backCursor, hotSpot, CursorMode.ForceSoftware);
    }
}
