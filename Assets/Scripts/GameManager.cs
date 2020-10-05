using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Sprite mapSprite;
    public GameObject map1,map2;
    void Start()
    {
        map1.GetComponent<Image>().sprite = mapSprite;
        map1.GetComponent<RectTransform>().sizeDelta=new Vector2(Screen.safeArea.width,Screen.height);
        map1.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        map2.GetComponent<Image>().sprite = mapSprite;
        map2.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.safeArea.width, Screen.height);
        map2.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, Screen.height);

        

    }

    // Update is called once per frame
    void Update()
    {

    }
}
