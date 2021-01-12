using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class OneSnowflake : MonoBehaviour
{
    public Transform geometry;
    private float directionX;
    private float directionY;
    private float rotationSpeed;
    private bool isfading;

    void Start()
    {
        float sizeFactor = Random.Range(60, 200);
        GetComponent<RectTransform>().sizeDelta = new Vector2(sizeFactor, sizeFactor);

        geometry = GameObject.Find("Geometry").transform;
        GetComponent<RectTransform>().anchoredPosition = geometry.GetChild(Random.Range(0, Music_Visualization.instance.barAmount)).GetComponent<RectTransform>().anchoredPosition;
        directionX = GetComponent<RectTransform>().anchoredPosition.x;
        directionY =  GetComponent<RectTransform>().anchoredPosition.y;

        rotationSpeed = Random.Range(.05f, .17f);
        if (Random.Range(-100, 100) < 0)
            rotationSpeed = -rotationSpeed;

        GetComponent<Image>().DOFade(Random.Range(.2f, .6f), 3f);
    }

    void FixedUpdate()
    {
        GetComponent<RectTransform>().anchoredPosition = new Vector2(GetComponent<RectTransform>().anchoredPosition.x + directionX / 200, 
            GetComponent<RectTransform>().anchoredPosition.y + directionY / 200);
        GetComponent<RectTransform>().Rotate(0, 0, rotationSpeed);

        if (!isfading && (GetComponent<RectTransform>().anchoredPosition.x > 750 || GetComponent<RectTransform>().anchoredPosition.x < -750
            || GetComponent<RectTransform>().anchoredPosition.y > 450 || GetComponent<RectTransform>().anchoredPosition.y < -450))
        {
            isfading = true;
            GetComponent<Image>().DOComplete();
            GetComponent<Image>().DOFade(0, 2.3f);
            
            Destroy(gameObject, 3f);
        }
            
    }


}
