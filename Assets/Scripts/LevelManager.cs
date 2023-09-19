using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager main;

    public Transform[] path; // enemy path

    public Transform startPoint; // start point

    private void Awake()
    {
        main = this;
    }
}
