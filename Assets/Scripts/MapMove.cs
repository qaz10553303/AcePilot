using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMove : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector2 mapPosition;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mapPosition.x = GetComponent<RectTransform>().anchoredPosition.x;
        mapPosition.y = GetComponent<RectTransform>().anchoredPosition.y;
        Debug.Log(GetComponent<RectTransform>().anchoredPosition.y);
        transform.Translate(Vector2.down, Space.World);
        if (mapPosition.y <= -Screen.height)
        {
            GetComponent<RectTransform>().anchoredPosition = new Vector2(mapPosition.x, mapPosition.y + 2 * Screen.height);
        }
    }
}
