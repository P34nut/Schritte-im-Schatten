using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using RenderHeads.Media.AVProVideo;
using I2.Loc;


public class VideoController : MonoBehaviour {

    //Xml
    public static VideoController Instance;

    private Content_xml xml;
    private LayoutCheck layout;

    private CursorSettings cursorSettings;

    public int prevId;
    public float answerTime = 5f;
    public int next;
    public float time;
    public float duration;
    public int id = 0;
    private int selected_option;
    public int currentSceneIndex;
    public bool meta=false;
    public bool finishedOpening = false;
    public bool useFade;

    //Plugin
    private string[] videofiles;
    private MediaPlayer[] player;
    public DisplayUGUI mediaDisplay;
    public SubtitlesUGUI subtitlesUI;
    public AudioOutput audioOutput;
    //public GameObject srt;

    //private AudiFade audioFade;
    private Inventar inventory;
    private InventoryElena elenaInventory;
    private InventoryRene reneInventory;
    //public bool testBool;
    //public bool boolTest;

    public void setKey()
    {
        layout.setKey();
    }

    public void didPhantom()
    {
        layout.didPhantom();
    }

    private void Awake()
    {
        Instance = this;
        inventory = GameObject.Find("Inventory").GetComponent<Inventar>();
        elenaInventory = inventory.gameObject.GetComponent<InventoryElena>();
        reneInventory = inventory.gameObject.GetComponent<InventoryRene>();

    }

    void Start () {

        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        elenaInventory.updateInventory(currentSceneIndex);
        reneInventory.updateInventory(currentSceneIndex);

        Save();

        xml = this.GetComponent<Content_xml>();
        layout = this.GetComponent<LayoutCheck>();

        cursorSettings = this.GetComponent<CursorSettings>();

        
        //audioFade = GameObject.Find("BGMusic").GetComponent<AudiFade>();

        videofiles = new string[xml.getVidCount()];
        player = new MediaPlayer[xml.getVidCount()];

        
        AddVideo(0);
        AddVideos(); 
        RunDialogue();


    }

    public void Save()
    {
        SaveLoadManager.SaveGameState();
    }

    public void OnEvent(MediaPlayer mp, MediaPlayerEvent.EventType et, ErrorCode errorCode)
    {
        switch (et)
        {
            case MediaPlayerEvent.EventType.ReadyToPlay:
                break;
            case MediaPlayerEvent.EventType.Started:
                break;
            case MediaPlayerEvent.EventType.FirstFrameReady:
                break;
            case MediaPlayerEvent.EventType.MetaDataReady:
                getDuration();
                meta = true;
                break;
            case MediaPlayerEvent.EventType.FinishedPlaying:
                meta = false;
                break;
        }
    }

    void Update()
    {
        //Debug.Log(xml.getVidCount());

#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (player[id].DisplayDebugGUI)
            {
                player[id].SetDebugGuiEnabled(false);
                Cursor.visible = false;
            }
            else
            {
                player[id].SetDebugGuiEnabled(true);
                Cursor.visible = true;
            }
        }
#endif

        if (meta)
        {
            time = player[id].Control.GetCurrentTimeMs() / 1000f;
        }
        
        
    }

    public void getDuration()
    {
        if(player[id]!=null && player[id].Info != null)
        {
           duration = player[id].Info.GetDurationMs()/1000f;
        }
    }

    //Erstes Video wird geladen und direkt gestartet
    public void AddVideo(int i)
    {
        videofiles[i] = xml.getVidName(i);
        GameObject newMediaPlayerGameObject = new GameObject("MediaPlayer" + "  " + i.ToString() + "  " + xml.getVidName(i));
        MediaPlayer newplayer = newMediaPlayerGameObject.AddComponent<MediaPlayer>();
        player[i] = newplayer;

        player[i].m_VideoPath = videofiles[i];
        player[i].m_AutoStart = true;
        ///////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////
        player[i].PlatformOptionsWindows.useHardwareDecoding = false;      //HardwareDecode
        ////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////
        player[i].PlatformOptionsWindows.videoApi = Windows.VideoApi.MediaFoundation;
        player[i].PlatformOptionsWindows.useUnityAudio = true;

        player[i].Events.AddListener(OnEvent);
    }

    //Übrige Videos werden in Array geladen
    public void AddVideos()
    {
        for (int i = 1; i < xml.getVidCount(); i++)
        {
            videofiles[i] = xml.getVidName(i);
            GameObject newMediaPlayerGameObject = new GameObject("MediaPlayer" + "  " + i.ToString() + "  " + xml.getVidName(i));
            MediaPlayer newplayer = newMediaPlayerGameObject.AddComponent<MediaPlayer>();
            player[i] = newplayer;

            player[i].m_VideoPath = videofiles[i];
            player[i].m_AutoOpen = false;
            player[i].m_AutoStart = false;
            player[i].m_Loop = xml.getLoop(i);
            ///////////////////////////////////////////////////////////////////////////////////
            //////////////////////////////////////////////////////////////////////////////////
            player[i].PlatformOptionsWindows.useHardwareDecoding = false;      //HardwareDecode
            ////////////////////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////////////////////
            player[i].PlatformOptionsWindows.videoApi = Windows.VideoApi.MediaFoundation;
            player[i].PlatformOptionsWindows.useUnityAudio = true;

            player[i].Events.AddListener(OnEvent);
        }
    }
     
    //Starte Coroutine
    public void RunDialogue()
    {
        StartCoroutine(run());
    }

    public void SetSelectedOption(int x)
    {

         selected_option = x;
 
    }



    //Coroutine - Hier wird alles gesteuert
    public IEnumerator run()
    {

        while (id != -1) {
            cursorSettings.setDefault();
            //player[id].Events.AddListener(OnEvent);
            meta = false;
            Debug.Log("-----------------");
            
            

            if (xml.getState(id) == 0 && !xml.getLoop(id))
            {
                Cursor.visible = false;
               
            }
            else
            {
                if (currentSceneIndex == 13 || currentSceneIndex == 1)
                {
                    Cursor.visible = false;
                }
                else
                {
                    Cursor.visible = true;
                }
                
            }
            finishedOpening = false;
            //openVideos(id); //video öffnen
            display_video(id);
            if (currentSceneIndex != 9)
            {
                StartCoroutine(openVideos(id));
            }
            else
            {
                finishedOpening = true;
            }
            prevId = id;
            while (!meta || duration==0 || !finishedOpening)
            {
                yield return new WaitForEndOfFrame();
                getDuration();
                //Debug.Log("meta "+meta);
            }
         

            //finishedOpening = false;

            if (xml.getState(id) != 1)
            {
                //yield return new WaitForSeconds(0.5f);
                layout.checkLayout();
                
            }

            Debug.Log("dur "+duration);

            if (xml.getState(id) == 1)
            {
                yield return new WaitUntil(() => player[id].Control.GetCurrentTimeMs() / 1000 >= duration - answerTime);
                layout.checkLayout(); //Button Controller
                
            }

            //Debug.Log("ID " +id);
            //Debug.Log("next ID" + " " + next);
            

            if (xml.getLoop(id) || (currentSceneIndex == 10 && id==19))
            {
                selected_option = -1;
                while(selected_option == -1)
                {
                    yield return new WaitForSeconds(0.25f);
                }
            }
            else
            {
                selected_option = next;
                while (player[id].Control.IsFinished() == false)
                {

                    yield return new WaitForSeconds(0.25f);

                }
            }

            layout.disableWindow();
            player[id].SetDebugGuiEnabled(false);
            subtitlesUI.gameObject.GetComponent<Text>().text = "";
            id = selected_option; 
            Close(id); //Schließe unbenutzte Videos
        }


        //SceneManager.LoadScene(yourscene+1);
        //StartCoroutine(audioFade.FadeAudioOut(audioFade.gameObject.GetComponent<AudioSource>()));
        LoadingScreenManager.LoadScene(currentSceneIndex+1);
        useFade = false;
    }



    public void pauseVideo()
    {
        mediaDisplay._mediaPlayer.Control.Pause();
    }

    public void unpauseVideo()
    {
        mediaDisplay._mediaPlayer.Play();
    }

    //nur Debug
    public void showOpened()
    {
        for (int i = 0; i < xml.getVidCount(); i++)
        {
            if (player[i].VideoOpened && i !=id)
            {
                Debug.Log("open:" + player[i]);
            }
        }
    }

    //Video anzeigen
    private void display_video(int i)
    {
        
        //mediaDisplay._mediaPlayer = player[i];
        //mediaDisplay._scaleMode = ScaleMode.StretchToFill;

        if (useFade)
        {
            StartCoroutine(LoadWithFade(i));
        }
        else
        {
            mediaDisplay._mediaPlayer = player[i];
            SubtitleControl();
            //subtitlesUI.ChangeMediaPlayer(player[i]);
            audioOutput.ChangeMediaPlayer(player[i]);
            mediaDisplay._mediaPlayer.Play();
        }
        

    
    }

    public void SubtitleControl()
    {
        if (xml.getSubtitle(id) && Einstellungen.Instance.currentSubtitle)
        {
            if (LocalizationManager.CurrentLanguageCode == "de")
            {
                mediaDisplay._mediaPlayer.EnableSubtitles(MediaPlayer.FileLocation.RelativeToStreamingAssetsFolder, "Subtitles/German/" + xml.getName(id) + ".srt");
            }
            else if (LocalizationManager.CurrentLanguageCode == "en")
            {
                mediaDisplay._mediaPlayer.EnableSubtitles(MediaPlayer.FileLocation.RelativeToStreamingAssetsFolder, "Subtitles/English/" + xml.getName(id) + ".srt");
            }
            else
            {
                Debug.LogError("Language has no Subtitels");
            }
            subtitlesUI._text = subtitlesUI.GetComponent<Text>();
            subtitlesUI.ChangeMediaPlayer(player[id]);
        }
        else if (xml.getSubtitle(id) && !Einstellungen.Instance.currentSubtitle)
        {
            
            mediaDisplay._mediaPlayer.DisableSubtitles();
            subtitlesUI._text.text = "";
        }
    }

    public IEnumerator LoadWithFade(int i)
    {
        const float FadeDuration = 0.25f;
        float fade = FadeDuration;

        // Fade down
        while (fade > 0f && Application.isPlaying)
        {
            fade -= Time.deltaTime;
            fade = Mathf.Clamp(fade, 0f, FadeDuration);

            mediaDisplay.color = new Color(1f, 1f, 1f, fade / FadeDuration);

            yield return null;
        }

        // Wait 3 frames for display object to update
        yield return new WaitForSeconds(0.2f);

        mediaDisplay._mediaPlayer = player[i];
        //mediaDisplay._mediaPlayer.Play();

        // Wait 3 frames for display object to update
        yield return new WaitForSeconds(0.2f);
        

        while (fade < FadeDuration && Application.isPlaying)
        {
            fade += Time.deltaTime;
            fade = Mathf.Clamp(fade, 0f, FadeDuration);

            mediaDisplay.color = new Color(1f, 1f, 1f, fade / FadeDuration);


            yield return null;
        }

        SubtitleControl();
        //subtitlesUI.ChangeMediaPlayer(player[i]);
        audioOutput.ChangeMediaPlayer(player[i]);
        mediaDisplay._mediaPlayer.Play();
    }

    //Schließe Videos
    public void Close(int id)
        {
        Debug.Log(prevId + "PREV");
        for (int i = 0; i < xml.getVidCount(); i++)
        {
            
            if(i == id || i == prevId)
            {

            }
            else if (player[i].VideoOpened)
            {
                player[i].CloseVideo();
                Debug.Log("closed" + player[i]);
            }
        
        }
    
    }

    //Videos öffnen
    IEnumerator openVideos(int id)
    {
        Debug.Log("OPEN" + id);
        for (int i = 0; i < xml.getOptionCount(id); i++)
        {
            int n = xml.getBlock(id).option[i].nextVideoID;
            if (n != -1)
            {
                Debug.Log("n: " + n + ", prevID: " + prevId);
                if (n!=prevId)
                {
                    player[n].OpenVideoFromFile(MediaPlayer.FileLocation.RelativeToStreamingAssetsFolder, player[n].m_VideoPath, false);
                    Debug.Log(player[n]);
                }
                else
                {
                    meta = true;
                }
                
            }
            else
            {
                meta = true;
                yield return new WaitUntil(() => player[id].Control.GetCurrentTimeMs() / 1000 >= duration - answerTime);
                //StartCoroutine(audioFade.FadeAudioOut(audioFade.gameObject.GetComponent<AudioSource>()));
            }
            //yield return new WaitForSecondsRealtime(0.5f);

        }
        finishedOpening = true;
    }

    //public void openVideos(int id)
    //{
    //    for (int i = 0; i < xml.getOptionCount(id); i++)
    //    {
    //        int n = xml.getBlock(id).option[i].nextVideoID;
    //        if (n != -1)
    //        {
    //            player[n].OpenVideoFromFile(MediaPlayer.FileLocation.RelativeToStreamingAssetsFolder, player[n].m_VideoPath, false);
    //            Debug.Log(player[n]);
    //        }
    //        else
    //        {
    //            meta = true;
    //        }

    //    }

    //}

    public void openSpecificVideo(int n)
    {
        player[n].OpenVideoFromFile(MediaPlayer.FileLocation.RelativeToStreamingAssetsFolder, player[n].m_VideoPath, false);
        //Debug.Log("opened specific");
    }
    


}
