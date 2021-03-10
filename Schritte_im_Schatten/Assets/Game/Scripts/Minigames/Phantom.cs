using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using I2.Loc;

public class Phantom : MonoBehaviour {

    private VideoController vidcont;
    private InventoryRene inventoryRene;

    public AudioClip[] audioClip;
    public AudioClip drucken;
    //private AudioSource audioSource;
    //private AudioSource music;
    private BGMusic bgm_script;

    public GameObject Bild;
    public Image Augen;
    public Image Mund;
    public Image Bart;
    public Image Haar;
    public Image Nase;

    public Sprite[] augen;
    public Sprite[] mund;
    public Sprite[] bart;
    public Sprite[] haar;
    public Sprite[] nase;

    public Text Augen_text;
    public Text Bart_text;
    public Text Mund_text;
    public Text Haar_text;
    public Text Nase_text;

    private int augen_index = 0;
    private int bart_index = 0;
    private int haar_index = 0;
    private int mund_index = 0;
    private int nase_index = 0;

   private bool haarBool = false;
   private bool augenBool = false;
   private bool naseBool = false;
   private bool bartBool = false;
   private bool mundBool = false;

    public Image[] buttons;
    

    private void Awake()
    {
        inventoryRene = GameObject.Find("Inventory").GetComponent<InventoryRene>();
    }

    // Use this for initialization
    void Start () {

        vidcont = Camera.main.GetComponent<VideoController>();
        //audioSource = Haar_text.GetComponent<AudioSource>();
        bgm_script = GameObject.Find("BGMusic").GetComponent<BGMusic>();
        //music = GameObject.Find("AudioSource").GetComponent<AudioSource>();

        Augen_text.text = ScriptLocalization.Texte.Augen + " " + (augen_index + 1);
        Bart_text.text = ScriptLocalization.Texte.Bart + " " + (bart_index + 1);
        Mund_text.text = ScriptLocalization.Texte.Mund + " " + (mund_index + 1);
        Haar_text.text = ScriptLocalization.Texte.Haar + " " + (haar_index + 1);
        Nase_text.text = ScriptLocalization.Texte.Nase + " " + (nase_index + 1);

        Augen.sprite = augen[0];
        Bart.sprite = bart[0];
        Nase.sprite = nase[0];
        Mund.sprite = mund[0];
        Haar.sprite = haar[0];
	}

    public void onTranslateAuge()
    {
        Localize.MainTranslation = ScriptLocalization.Texte.Augen + " " + (augen_index + 1);
    }

    public void onTranslateBart()
    {
        Localize.MainTranslation = ScriptLocalization.Texte.Bart + " " + (bart_index + 1);
    }

    public void onTranslateNase()
    {
        Localize.MainTranslation = ScriptLocalization.Texte.Nase + " " + (nase_index + 1);
    }

    public void onTranslateMund()
    {
        Localize.MainTranslation = ScriptLocalization.Texte.Mund + " " + (mund_index + 1);
    }

    public void onTranslateHaar()
    {
        Localize.MainTranslation = ScriptLocalization.Texte.Haar + " " + (haar_index + 1);
    }

    public void changePicAugen(int button_index)
    {
      
        //Debug.Log(augen_index);
        if (augen_index == 0)
        {
            switch (button_index)
            {
                case 1:
                    Augen.sprite = augen[4];
                    augen_index = 4;
                    Augen_text.text = ScriptLocalization.Texte.Augen + " " + (augen_index+1);
                    break;
                case 2:
                    Augen.sprite = augen[1];
                    augen_index = 1;
                    Augen_text.text = ScriptLocalization.Texte.Augen + " " + (augen_index + 1);
                    break;
            }
        }
        else if (augen_index == 1)
        {
            switch (button_index)
            {
                case 1:
                    Augen.sprite = augen[0];
                    augen_index = 0;
                    Augen_text.text = ScriptLocalization.Texte.Augen + " " + (augen_index + 1);
                    break;
                case 2:
                    Augen.sprite = augen[2];
                    augen_index = 2;
                    Augen_text.text = ScriptLocalization.Texte.Augen + " " + (augen_index + 1);
                    break;
            }
        }
        else if(augen_index == 2)
        {
            switch (button_index)
            {
                case 1:
                    Augen.sprite = augen[1];
                    augen_index = 1;
                    Augen_text.text = ScriptLocalization.Texte.Augen + " " + (augen_index + 1);
                    break;
                case 2:
                    Augen.sprite = augen[3];
                    augen_index = 3;
                    Augen_text.text = ScriptLocalization.Texte.Augen + " " + (augen_index + 1);
                    break;
            }
        }
        else if (augen_index == 3)
        {
            switch (button_index)
            {
                case 1:
                    Augen.sprite = augen[2];
                    augen_index = 2;
                    Augen_text.text = ScriptLocalization.Texte.Augen + " " + (augen_index + 1);
                    break;
                case 2:
                    Augen.sprite = augen[4];
                    augen_index = 4;
                    Augen_text.text = ScriptLocalization.Texte.Augen + " " + (augen_index + 1);
                    break;
            }
        }
        else if (augen_index == 4)
        {
            switch (button_index)
            {
                case 1:
                    Augen.sprite = augen[3];
                    augen_index = 3;
                    Augen_text.text = ScriptLocalization.Texte.Augen + " " + (augen_index + 1);
                    break;
                case 2:
                    Augen.sprite = augen[0];
                    augen_index = 0;
                    Augen_text.text = ScriptLocalization.Texte.Augen + " " + (augen_index + 1);
                    break;
            }
        }
    }

    public void changePicMund(int button_index)
    {

        //Debug.Log(mund_index);
        if (mund_index == 0)
        {
            switch (button_index)
            {
                case 1:
                    Mund.sprite = mund[4];
                    mund_index = 4;
                    Mund_text.text = ScriptLocalization.Texte.Mund + " " + (mund_index + 1);
                    break;
                case 2:
                    Mund.sprite = mund[1];
                    mund_index = 1;
                    Mund_text.text = ScriptLocalization.Texte.Mund + " " + (mund_index + 1);
                    break;
            }
        }
        else if (mund_index == 1)
        {
            switch (button_index)
            {
                case 1:
                    Mund.sprite = mund[0];
                    mund_index = 0;
                    Mund_text.text = ScriptLocalization.Texte.Mund + " " + (mund_index + 1);
                    break;
                case 2:
                    Mund.sprite = mund[2];
                    mund_index = 2;
                    Mund_text.text = ScriptLocalization.Texte.Mund + " " + (mund_index + 1);
                    break;
            }
        }
        else if (mund_index == 2)
        {
            switch (button_index)
            {
                case 1:
                    Mund.sprite = mund[1];
                    mund_index = 1;
                    Mund_text.text = ScriptLocalization.Texte.Mund + " " + (mund_index + 1);
                    break;
                case 2:
                    Mund.sprite = mund[3];
                    mund_index = 3;
                    Mund_text.text = ScriptLocalization.Texte.Mund + " " + (mund_index + 1);
                    break;
            }
        }
        else if (mund_index == 3)
        {
            switch (button_index)
            {
                case 1:
                    Mund.sprite = mund[2];
                    mund_index = 2;
                    Mund_text.text = ScriptLocalization.Texte.Mund + " " + (mund_index + 1);
                    break;
                case 2:
                    Mund.sprite = mund[4];
                    mund_index = 4;
                    Mund_text.text = ScriptLocalization.Texte.Mund + " " + (mund_index + 1);
                    break;
            }
        }
        else
        {
            switch (button_index)
            {
                case 1:
                    Mund.sprite = mund[3];
                    mund_index = 3;
                    Mund_text.text = ScriptLocalization.Texte.Mund + " " + (mund_index + 1);
                    break;
                case 2:
                    Mund.sprite = mund[0];
                    mund_index = 0;
                    Mund_text.text = ScriptLocalization.Texte.Mund + " " + (mund_index + 1);
                    break;
            }
        }

    }

    public void changePicBart(int button_index)
    {

        //Debug.Log(bart_index);
        if (bart_index == 0)
        {
            switch (button_index)
            {
                case 1:
                    Bart.sprite = bart[3];
                    bart_index = 3;
                    Bart_text.text = ScriptLocalization.Texte.Bart + " " + (bart_index + 1);
                    break;
                case 2:
                    Bart.sprite = bart[1];
                    bart_index = 1;
                    Bart_text.text = ScriptLocalization.Texte.Bart + " " + (bart_index + 1);
                    break;
            }
        }
        else if (bart_index == 1)
        {
            switch (button_index)
            {
                case 1:
                    Bart.sprite = bart[0];
                    bart_index = 0;
                    Bart_text.text = ScriptLocalization.Texte.Bart + " " + (bart_index + 1);
                    break;
                case 2:
                    Bart.sprite = bart[2];
                    bart_index= 2;
                    Bart_text.text = ScriptLocalization.Texte.Bart + " " + (bart_index + 1);
                    break;
            }
        }
        else if (bart_index == 2)
        {
            switch (button_index)
            {
                case 1:
                    Bart.sprite = bart[1];
                    bart_index = 1;
                    Bart_text.text = ScriptLocalization.Texte.Bart + " " + (bart_index + 1);
                    break;
                case 2:
                    Bart.sprite = bart[3];
                    bart_index = 3;
                    Bart_text.text = ScriptLocalization.Texte.Bart + " " + (bart_index + 1);
                    break;
            }
        }
        else
        {
            switch (button_index)
            {
                case 1:
                    Bart.sprite = bart[2];
                    bart_index = 2;
                    Bart_text.text = ScriptLocalization.Texte.Bart + " " + (bart_index + 1);
                    break;
                case 2:
                    Bart.sprite = bart[0];
                    bart_index = 0;
                    Bart_text.text = ScriptLocalization.Texte.Bart + " " + (bart_index + 1);
                    break;
            }
        }

    }

    public void changePicHaar(int button_index)
    {

        //Debug.Log(haar_index);
        if (haar_index == 0)
        {
            switch (button_index)
            {
                case 1:
                    Haar.sprite = haar[4];
                    haar_index = 4;
                    Haar_text.text = ScriptLocalization.Texte.Haar + " " + (haar_index + 1);
                    break;
                case 2:
                    Haar.sprite = haar[1];
                    haar_index = 1;
                    Haar_text.text = ScriptLocalization.Texte.Haar + " " + (haar_index + 1);
                    break;
            }
        }
        else if (haar_index == 1)
        {
            switch (button_index)
            {
                case 1:
                    Haar.sprite = haar[0];
                    haar_index = 0;
                    Haar_text.text = ScriptLocalization.Texte.Haar + " " + (haar_index + 1);
                    break;
                case 2:
                    Haar.sprite = haar[2];
                    haar_index = 2;
                    Haar_text.text = ScriptLocalization.Texte.Haar + " " + (haar_index + 1);
                    break;
            }
        }
        else if (haar_index == 2)
        {
            switch (button_index)
            {
                case 1:
                    Haar.sprite = haar[1];
                    haar_index = 1;
                    Haar_text.text = ScriptLocalization.Texte.Haar + " " + (haar_index + 1);
                    break;
                case 2:
                    Haar.sprite = haar[3];
                    haar_index = 3;
                    Haar_text.text = ScriptLocalization.Texte.Haar + " " + (haar_index + 1);
                    break;
            }
        }
        else if (haar_index == 3)
        {
            switch (button_index)
            {
                case 1:
                    Haar.sprite = haar[2];
                    haar_index = 2;
                    Haar_text.text = ScriptLocalization.Texte.Haar + " " + (haar_index + 1);
                    break;
                case 2:
                    Haar.sprite = haar[4];
                    haar_index = 4;
                    Haar_text.text = ScriptLocalization.Texte.Haar + " " + (haar_index + 1);
                    break;
            }
        }
        else
        {
            switch (button_index)
            {
                case 1:
                    Haar.sprite = haar[3];
                    haar_index = 3;
                    Haar_text.text = ScriptLocalization.Texte.Haar + " " + (haar_index + 1);
                    break;
                case 2:
                    Haar.sprite = haar[0];
                    haar_index = 0;
                    Haar_text.text = ScriptLocalization.Texte.Haar + " " + (haar_index + 1);
                    break;
            }
        }

    }

    public void changePicNase(int button_index)
    {

        //Debug.Log(nase_index);
        if (nase_index == 0)
        {
            switch (button_index)
            {
                case 1:
                    Nase.sprite = nase[3];
                    nase_index = 3;
                    Nase_text.text = ScriptLocalization.Texte.Nase + " " + (nase_index + 1);
                    break;
                case 2:
                    Nase.sprite = nase[1];
                    nase_index = 1;
                    Nase_text.text = ScriptLocalization.Texte.Nase + " " + (nase_index + 1);
                    break;
            }
        }
        else if (nase_index == 1)
        {
            switch (button_index)
            {
                case 1:
                    Nase.sprite = nase[0];
                    nase_index = 0;
                    Nase_text.text = ScriptLocalization.Texte.Nase + " " + (nase_index + 1);
                    break;
                case 2:
                    Nase.sprite = nase[2];
                    nase_index = 2;
                    Nase_text.text = ScriptLocalization.Texte.Nase + " " + (nase_index + 1);
                    break;
            }
        }
        else if(nase_index == 2)
        {
            switch (button_index)
            {
                case 1:
                    Nase.sprite = nase[1];
                    nase_index = 1;
                    Nase_text.text = ScriptLocalization.Texte.Nase + " " + (nase_index + 1);
                    break;
                case 2:
                    Nase.sprite = nase[3];
                    nase_index = 3;
                    Nase_text.text = ScriptLocalization.Texte.Nase + " " + (nase_index + 1);
                    break;
            }
        }
        else if (nase_index == 3)
        {
            switch (button_index)
            {
                case 1:
                    Nase.sprite = nase[2];
                    nase_index = 2;
                    Nase_text.text = ScriptLocalization.Texte.Nase + " " + (nase_index + 1);
                    break;
                case 2:
                    Nase.sprite = nase[0];
                    nase_index = 0;
                    Nase_text.text = ScriptLocalization.Texte.Nase + " " + (nase_index + 1);
                    break;
            }
        }

    }

    public void checkDruck()
    {
        StartCoroutine(Drucker());
    }

    IEnumerator Drucker()
    {
        if (haar_index == 2 && mund_index == 3 && nase_index == 0 && augen_index == 4 && bart_index == 1)
        {


            if (!AudioManger.Instance.audioSource.isPlaying)
            {
                Cursor.visible = false;
                //Bild.GetComponent<AudioSource>().Play();
                AudioManger.Instance.setAudio(8,true);
                AudioManger.Instance.setAudioDelayed(9, 1.5f, false);
                StartCoroutine(bgm_script.SwitchTrack(32));
                yield return new WaitWhile(() => AudioManger.Instance.audioSource.isPlaying);
                yield return new WaitWhile(() => AudioManger.Instance.audioSourceDelay.isPlaying);

                inventoryRene.setImage(3);
                vidcont.SetSelectedOption(30);
                Cursor.visible = true;
            }
            
                
        }
        else
        {
            Cursor.visible = false;
            AudioManger.Instance.setAudio(10, true);
            checkCorrect();
            yield return new WaitWhile(() => AudioManger.Instance.audioSource.isPlaying);
            Cursor.visible = true;
        }
        StopCoroutine(Drucker());
    }

    void checkCorrect()
    {
        if (haar_index == 2)
        {
            buttons[0].enabled = false;
            buttons[1].enabled = false;
        }

        if (mund_index == 3)
        {
            buttons[8].enabled = false;
            buttons[9].enabled = false;
        }

        if (nase_index == 0)
        {
            buttons[4].enabled = false;
            buttons[5].enabled = false;
        }

        if (augen_index == 4)
        {
            buttons[2].enabled = false;
            buttons[3].enabled = false;
        }

        if (bart_index == 1)
        {
            buttons[6].enabled = false;
            buttons[7].enabled = false;
        }
    }

    public void startQuestion(int audioIndex)
    {
        //StartCoroutine(questions(audioIndex));

        AudioManger.Instance.setAudio(audioIndex, true);


    }
}
