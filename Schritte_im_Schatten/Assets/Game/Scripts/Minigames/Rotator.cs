using UnityEngine;

public class Rotator : MonoBehaviour {

    public static Rotator Instance;
    public bool isPaused;
    public float speed = 100f;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        SetRandomSpeed();
    }

    // Update is called once per frame
    void Update () {

            transform.Rotate(0f, 0f, speed * Time.deltaTime);
        
	}

    public void SetRandomSpeed()
    {
        int index = Random.Range(0, 9);

        switch (index)
        {
            case 0:
                speed = -100;
                break;
            case 1:
                speed = -75;
                break;
            case 2:
                speed = -50;
                break;
            case 3:
                speed = 100;
                break;
            case 4:
                speed = 75;
                break;
            case 5:
                speed = 50;
                break;
            case 6:
                speed = 62.5f;
                break;
            case 7:
                speed = -62.5f;
                break;
            case 8:
                speed = -87.5f;
                break;
            case 9:
                speed = 87.5f;
                break;
        }

    }

}
