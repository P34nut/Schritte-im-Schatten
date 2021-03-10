
using UnityEngine;

public class Schloss : MonoBehaviour {

    public static Schloss Instance;

    private VideoController vidcont;
    public bool isPaused;
    public int winID;
    public float speed = 10f;
    private float startY;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        vidcont = Camera.main.GetComponent<VideoController>();
        startY = transform.position.y;
    }

    // Update is called once per frame
    void Update () {

        if (!isPaused)
        {
            if (Input.GetMouseButton(0))
            {

                float currentX = transform.position.x;
                float currentY = transform.position.y;
                Vector3 target = new Vector3(currentX, currentY - 10f, 0f);
                transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
            }
        }


        

	}

    void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Fail")
        {
            transform.position = new Vector3(transform.position.x, startY, 0f);
        }
        else if(collision.tag == "Win")
        {
            vidcont.SetSelectedOption(winID);
        }
    }
}
