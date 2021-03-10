using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MouseQTE : MonoBehaviour
{
    private Vector3 mouseDownPos;

    private VideoController vidcont;
    private Content_xml xml;

    public GameObject prefabImage;
    private GameObject image;
    private GameObject checkImage;

    public Sprite[] pfeil;
    public Sprite haken;
    public Sprite kreuz;
    public bool clicked;
    public bool allowed;
    public float quickTime=1.5f;


    void Start()
    {
        Cursor.visible = false;
        vidcont = this.GetComponent<VideoController>();
        xml = this.GetComponent<Content_xml>();
        
        prefabImage = Instantiate<GameObject>(prefabImage);

        image = GameObject.Find("Pfeil_Image");
        checkImage = GameObject.Find("CheckImage");

        prefabImage.SetActive(false);
        
    }

    void Update()
    {
        //Debug.Log(clicked);


            checkMouse();
        


    }

    public void display()
    {
        Cursor.visible = false;
        //allowed = true;
        image.SetActive(false);
        checkImage.SetActive(false);
        
        StartCoroutine(show());
        changeImagePosition();
       
    }

    public void changeImagePosition()
    {
        switch (vidcont.id)
        {
            case 0:
                image.transform.localPosition = new Vector2(350, 250);
                checkImage.transform.localPosition = new Vector2(350, 250);
                break;
            case 3:
                image.transform.localPosition = new Vector2(430, -320);
                checkImage.transform.localPosition = new Vector2(430, -320);
                break;
            case 6:
                image.transform.localPosition = new Vector2(0, 0);
                checkImage.transform.localPosition = new Vector2(0, 0);
                break;
        }
        
    }

    public void checkNext()
    {
        int id = vidcont.id;
        int rand = Random.Range(0, xml.getBlock(id).option.Count); //Random welche Dialogoption gewählt wird wenn man nichts klickt
        vidcont.next = xml.getBlock(id).option[rand].nextVideoID;
    }

    public IEnumerator show()
    {

        image.SetActive(false);
        checkImage.SetActive(false);
        prefabImage.SetActive(false);
        
        if (xml.getState(vidcont.id)==-1)
        {
            switch (vidcont.id)
            {
                case 0:
                    image.GetComponent<Image>().sprite = pfeil[0];
                    break;
                case 3:
                    image.GetComponent<Image>().sprite = pfeil[1];
                    break;
                case 6:
                    image.GetComponent<Image>().sprite = pfeil[2];
                    break;
            }

            yield return new WaitForSeconds(1f);
            prefabImage.SetActive(true);
            yield return new WaitWhile(() => vidcont.time <= vidcont.duration - 2f - quickTime);
            Cursor.visible = true;
            yield return new WaitWhile(() => vidcont.time <= vidcont.duration - quickTime);
            allowed = true;
            image.SetActive(true);
            yield return new WaitForSeconds(quickTime);

            if (clicked)
            {
                vidcont.SetSelectedOption(xml.getBlock(vidcont.id).option[0].nextVideoID);
                allowed = false;
            }
            else

            {
                vidcont.SetSelectedOption(xml.getBlock(vidcont.id).option[1].nextVideoID);
                allowed = false;
            }

        }
        else
        {
            clicked = false;
        }
    }

    public void checkMouse()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mouseDownPos = Input.mousePosition;
            
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (allowed)
            {
                switch (vidcont.id)
                {
                    case 0:
                        if (Input.mousePosition.y > mouseDownPos.y && Input.mousePosition.x > mouseDownPos.x && (Input.mousePosition.y - mouseDownPos.y) > 50 && (Input.mousePosition.x - mouseDownPos.x) > 50)
                        {
                            clicked = true;
                            allowed = false;
                            image.SetActive(false);
                            checkImage.GetComponent<Image>().sprite = haken;
                            checkImage.SetActive(true);
                        }
                        else
                        {
                            clicked = false;
                            allowed = false;
                            image.SetActive(false);
                            checkImage.GetComponent<Image>().sprite = kreuz;
                            checkImage.SetActive(true);
                        }
                        break;
                    case 3:
                        if (Input.mousePosition.y < mouseDownPos.y && (mouseDownPos.y - Input.mousePosition.y) > 50)
                        {
                            clicked = true;
                            allowed = false;
                            image.SetActive(false);
                            checkImage.GetComponent<Image>().sprite = haken;
                            checkImage.SetActive(true);
                        }
                        else
                        {
                            clicked = false;
                            allowed = false;
                            image.SetActive(false);
                            checkImage.GetComponent<Image>().sprite = kreuz;
                            checkImage.SetActive(true);
                        }
                        break;
                    case 6:
                        if (Input.mousePosition.y > mouseDownPos.y && (Input.mousePosition.y - mouseDownPos.y) > 50)
                        {
                            clicked = true;
                            allowed = false;
                            image.SetActive(false);
                            checkImage.GetComponent<Image>().sprite = haken;
                            checkImage.SetActive(true);
                        }
                        else
                        {
                            clicked = false;
                            allowed = false;
                            image.SetActive(false);
                            checkImage.GetComponent<Image>().sprite = kreuz;
                            checkImage.SetActive(true);
                        }
                        break;

                }
            }
            
        }
    }
}

