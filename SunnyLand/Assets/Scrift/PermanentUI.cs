using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PermanentUI : MonoBehaviour
{
    // Start is called before the first frame update
    public int cherries = 0;
    public int diamonds = 0;


    public TextMeshProUGUI cherryText;
    public TextMeshProUGUI diamondText;

    public static PermanentUI perm;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        //singleton
        if (!perm)
        {
            perm = this;
        }
        else
            Destroy(gameObject);
    }
    public void Reset()
    {
        cherries = 0;
        cherryText.text = cherries.ToString();
        diamonds = 0;
        diamondText.text = diamonds.ToString();
    }

}
