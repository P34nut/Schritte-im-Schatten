using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonFilter : MonoBehaviour {

    public enum Tag
    {
        Alltag, Gesundheit, Kriminalität, Lifestyle, Menschen, Natur, Politik,
        Sport, Unterhaltung, Wirtschaft
    }
    public int tag;
    public int gelöst;
    public int Datum;
    public int Entfernung;
    public int ermittlung;
    public int last;
    public int aufgearbeitet;
    public int unterhaltung;
    public bool isActive;

}
