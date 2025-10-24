using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    private static UiManager instance;
    public static UiManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<UiManager>();
            }

            return instance;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
