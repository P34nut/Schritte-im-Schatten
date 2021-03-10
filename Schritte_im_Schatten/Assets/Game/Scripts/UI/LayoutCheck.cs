using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LayoutCheck : MonoBehaviour {

    private MouseQTE one;
    private UI_S2 two;
    private UI_S3 three;
    private UI_S4 four;
    private UI_S5 five;
    private UI_S6 six;
    private UI_S7 seven;
    private UI_S8 eight;
    private UI_S9 nine;
    private UI_S10 ten;
    private UI_S11 eleven;
    private UI_S12 twelve;
    private UI_S13 thirdteen;



	// Use this for initialization
	void Start () {

        one = this.GetComponent<MouseQTE>();
        two = this.GetComponent<UI_S2>();
        three = this.GetComponent<UI_S3>();
        four = this.GetComponent<UI_S4>();
        five = this.GetComponent<UI_S5>();
        six = this.GetComponent<UI_S6>();
        seven = this.GetComponent<UI_S7>();
        eight = this.GetComponent<UI_S8>();
        nine = this.GetComponent<UI_S9>();
        ten = this.GetComponent<UI_S10>();
        eleven = this.GetComponent<UI_S11>();
        twelve = this.GetComponent<UI_S12>();
        thirdteen = this.GetComponent<UI_S13>();
    }
	
	// Update is called once per frame
	void Update () {
        
    }

    public void checkLayout()
    {

        switch (SceneManager.GetActiveScene().buildIndex)
        {
            case 1:         //S1
                one.display();
                one.checkNext();
                break;
            case 2:         //S2
                two.display();
                break;
            case 3:         //S3
                three.display();
                break;
            case 4:         //S4
                four.display();
                break;
            case 5:         //S5
                five.display();
                break;
            case 6:         //S6
                six.display();
                break;
            case 7:         //S7
                seven.display();
                break;
            case 8:         //S8
                eight.display();
                break;
            case 9:         //S9
                nine.display();
                break;
            case 10:         //S10
                ten.display();
                break;
            case 11:         //S11
                eleven.display();
                break;
            case 12:         //S12
                twelve.display();
                break;
            case 13:         //S13
                thirdteen.display();
                break;
        }
    }

    public void disableWindow()
    {
        switch (SceneManager.GetActiveScene().buildIndex)
        {
            case 1:         //S1
                break;
            case 2:         //S2
                two.disable();
                break;
            case 3:         //S3
                three.disable();
                break;
            case 4:         //S4
                four.disable();
                break;
            case 5:         //S5
                five.disable();
                break;
            case 6:         //S6
                six.disable();
                break;
            case 7:         //S7
                seven.disable();
                break;
            case 8:         //S8
                eight.disable();
                break;
            case 9:         //S9
                nine.disable();
                break;
            case 10:         //S10
                ten.disable();
                break;
            case 11:         //S11
                eleven.disable();
                break;
            case 12:         //S12
                twelve.disable();
                break;
            case 13:         //S13
                thirdteen.disable();
                break;
        }
    }

    public void setKey()
    {
        six.key = true;
    }

    public void didPhantom()
    {
        eight.LIndex += 1;
    }

}
