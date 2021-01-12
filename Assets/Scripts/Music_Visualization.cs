using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(AudioSource))]
public class Music_Visualization : MonoBehaviour
{

    public static Music_Visualization instance;

    public GameObject barPrefab;
    public Transform Geometry;
    private float[] spectrumStart;
    private float[] spectrumCurrent;
    private float[] spectrumEnd;
    private float ratio = 0;
    private float timeStep = .1f;
    private float timeFactor;
    private float timeFactor2;

    //optional
    [HideInInspector] public int barAmount = 128;
    private int spectrumAmount;
    private int radius = 200;

    [HideInInspector] public float totVol;

    public Transform Quin;

    private void Awake()
    {
        instance = this;
        timeFactor = Time.time;
        timeFactor2 = Time.time;
    }

    private void Start()
    {
        GetComponent<AudioSource>().time = 0;

        spectrumAmount = barAmount > 1024 ? 8192 : 8 * barAmount; //spectrumAmount is 8 times of barAmount
        
        spectrumStart = new float[spectrumAmount];
        spectrumCurrent = new float[spectrumAmount];
        spectrumEnd = new float[spectrumAmount];

        for (int i = 0; i < barAmount; i++)
            Instantiate(barPrefab, Geometry);

        for (int i = 0; i < barAmount; i++)
        {
            float x = Mathf.Cos(((float)i / barAmount) * 2 * Mathf.PI) * radius;
            float y = Mathf.Sin(((float)i / barAmount) * 2 * Mathf.PI) * radius;
            Geometry.GetChild(i).GetComponent<RectTransform>().anchoredPosition = new Vector2(x, y);
            Geometry.GetChild(i).GetComponent<RectTransform>().Rotate(new Vector3(0, 0, ((float)i / barAmount) * 360));
        }
     }

    private void Update()
    {   
       //quit application
       if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if(Time.time > timeFactor)
        {
            timeFactor = Time.time + timeStep;
            ratio = 0;

            for (int i = 0; i < spectrumAmount; i++)
            {
                spectrumStart[i] = spectrumEnd[i];
            }

            totVol = 0;
            for (int i = 0; i < barAmount; i++)
                totVol += spectrumEnd[i];

            AudioListener.GetSpectrumData(spectrumEnd, 0, FFTWindow.Rectangular);
        }
        else
            ratio += Time.deltaTime / timeStep;

        
        for (int i = 0; i < barAmount; i++)
        {
            spectrumCurrent[i + barAmount / 5] = Mathf.Lerp(spectrumStart[i + barAmount / 5], spectrumEnd[i + barAmount / 5], ratio);
            Geometry.GetChild(i).GetComponent<RectTransform>().sizeDelta 
                 = new Vector2(spectrumCurrent[i + barAmount / 5] * 4000 > 5 ? spectrumCurrent[i + barAmount / 5] * 4000: 5, 4);
            
        }

        if (Time.time > timeFactor2)
        {
            timeFactor2 += .465f;
            if(totVol >2f)
                Quin.GetComponent<RectTransform>().DOPunchScale(new Vector3(.2f, .2f, .2f), .13f);
        }
    }

    IEnumerator QuitApp()
    {
        yield return new WaitForSeconds(332f);
        Application.Quit();
    }
}
