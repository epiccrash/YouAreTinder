﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class TimelineUI : MonoBehaviour
{
    // TODO: grab information from the singleton
    // TODO: different colors for events based on if they're good or not
    public GameObject eventMarker;
    public GameObject lineRenderer;
    public GameObject textBox;
    public GameObject characters;
    public Image p1Sprite;
    public Image p2Sprite;
    public TextMeshProUGUI exitText;
    public Image background;
    public GameObject fadeOut;
    public Color startBgColor;
    public Color goodEventColor;
    public GameObject goodEventParticles;
    public Sprite goodEventMarker;
    public Color badEventColor;
    public Sprite badEventMarker;

    public float spaceBetweenEvents;
    public float spaceBetweenText;
    public float pauseBetweenEvents;
    public float lineDrawTime;
    public float renderEventTime;
    public float jitterTime;

    private string[] events;
    private bool[] eventOutcomes;
    private float nextXPos;
    private float yPos;

    private RectTransform lineRT;
    private RectTransform charactersRT;
    private bool fastForward = false;
    private bool canExit = false;

    private float drawIter = 0.01f;
    private int fadeIn = 100;
    private int textMoveInY = 15;
    private float shakes = 10;
    private float shakeSpeed = 0.1f;
    private float pulseAmount = 1.2f;
    private float pulseSpeed = 20;

    private IEnumerator ActivateExit()
    {
        if(eventOutcomes[eventOutcomes.Length - 1]) GameState.Instance.currentPoints += 1;
        if(GameState.Instance.currentPoints >= GameState.Instance.pointsToWin) {

            // Set fade out so it renders over everything else
            fadeOut.SetActive(true);
            fadeOut.transform.SetSiblingIndex(this.transform.childCount - 1);
            Image fadeOutImg = fadeOut.GetComponent<Image>();
            
            yield return new WaitForSeconds(pauseBetweenEvents / 2);

            // Fade to pink
            for(int j = 0; j < fadeIn; j++) {
                float frac = (float)j / (float)fadeIn;
                float newAlpha = Mathf.Lerp(0, 1.0f, frac);
                fadeOutImg.color = new Color(fadeOutImg.color.r, fadeOutImg.color.g, fadeOutImg.color.b, newAlpha);
                
                yield return new WaitForSeconds(drawIter);
            }

            yield return new WaitForSeconds(pauseBetweenEvents);
            GameState.Instance.currentPoints = 0;
            SceneManager.LoadScene(3);
        } else {
            canExit = true;

            // Fade text in + bring it down
            for(int j = 0; j < fadeIn; j++) {
                float frac = (float)j / (float)fadeIn;
                float newAlpha = Mathf.Lerp(0, 1.0f, frac);
                exitText.faceColor = new Color(exitText.faceColor.r, exitText.faceColor.g, exitText.faceColor.b, newAlpha);
                
                yield return new WaitForSeconds(drawIter);
            }
        }
        
    }

    private IEnumerator ChangeBackground(bool good, bool final)
    {
        // Find colors to deal with
        Color tintWith = goodEventColor;
        if(!good) {
            tintWith = badEventColor;
        }

        Color ogColor = background.color;
        Color newColor = new Color(tintWith.r * 0.3f + ogColor.r * 0.7f, tintWith.g * 0.3f + ogColor.g * 0.7f,
                                    tintWith.b * 0.3f + ogColor.b * 0.7f, 1.0f);
        if(final) {
            newColor = new Color(tintWith.r, tintWith.g, tintWith.b, 1.0f);
        }

        // Transition colors
        float colorF = 1.0f / (float)fadeIn;
        for(int i = 0; i < fadeIn; i++) {
            background.color = Color.Lerp(ogColor, newColor, colorF);
            colorF += 1.0f / (float)fadeIn;
            
            yield return new WaitForSeconds(drawIter);
        }
    }

    private IEnumerator ShakeEventMarker(GameObject marker) {
        RectTransform markerRT = marker.GetComponent<RectTransform>();
        int ogPos = (int)markerRT.localPosition.x;
        int numShakes = (int)Random.Range(shakes / 2, shakes);
        int shakeAmt = (int)(shakeSpeed / drawIter);

        // Shake!!!
        float frac = 1.0f / (float)shakeAmt;
        for(int j = 0; j < numShakes; j++) {
            // Go back and forth
            int shakeDist = (int)Mathf.Pow(-1.0f, j) * (int)Random.Range(0, shakes);
            int curr = (int)markerRT.localPosition.x;
            int newP = curr + shakeDist;

            for(int i = 0; i < shakeAmt; i++) {
                int lerp = (int)Mathf.Lerp(curr, newP, frac);
                markerRT.localPosition = new Vector3(lerp, markerRT.localPosition.y, markerRT.localPosition.z);

                frac += 1.0f / (float)shakeAmt;
                yield return new WaitForEndOfFrame();
            }
        }

        // Reset position
        int currPos = (int)markerRT.localPosition.x;
        frac = 1.0f / (float)shakeAmt;
        for(int k = 0; k < shakeAmt; k++) {
                int lerp = (int)Mathf.Lerp(currPos, ogPos, frac);
                markerRT.localPosition = new Vector3(lerp, markerRT.localPosition.y, markerRT.localPosition.z);

                frac += 1.0f / (float)shakeAmt;
                yield return new WaitForEndOfFrame();
        }
    }

    private IEnumerator PulseEventMarker(GameObject marker) {
        RectTransform markerRT = marker.GetComponent<RectTransform>();

        // Pulse out
        float frac = 1.0f / (float)pulseSpeed;
        for(int k = 0; k < pulseSpeed; k++) {
            float lerp = Mathf.Lerp(1.0f, pulseAmount, frac);
            markerRT.localScale = new Vector3(lerp, lerp, 1);
            // Debug.Log(markerRT.localScale.x);

            frac += 1.0f / (float)pulseSpeed;
            yield return new WaitForEndOfFrame();
        }

        // Pulse back in
        float currScale = markerRT.localScale.x;
        frac = 1.0f / (float)pulseSpeed;
        for(int k = 0; k < pulseSpeed; k++) {
            float lerp = Mathf.Lerp(currScale, 1.0f, frac);
            markerRT.localScale = new Vector3(lerp, lerp, 1);

            frac += 1.0f / (float)pulseSpeed;
            yield return new WaitForEndOfFrame();
        }
    }

    private IEnumerator PlayTimeline()
    {
        nextXPos = lineRenderer.GetComponent<RectTransform>().localPosition.x;
        yPos = lineRenderer.GetComponent<RectTransform>().localPosition.y;
        p1Sprite.sprite = MatchmakingState.Instance.profile1;
        p2Sprite.sprite = MatchmakingState.Instance.profile2;
        lineRT = lineRenderer.GetComponent<RectTransform>();
        charactersRT = characters.GetComponent<RectTransform>();

        // Fade background color in
        Color ogColor = background.color;
        float colorF = 1.0f / (float)fadeIn;
        for(int i = 0; i < fadeIn; i++) {
            background.color = Color.Lerp(ogColor, startBgColor, colorF);
            colorF += 1.0f / (float)fadeIn;

            yield return new WaitForSeconds(drawIter);
        }

        yield return new WaitForSeconds(pauseBetweenEvents);
        
        characters.SetActive(true);
        if(!fastForward) yield return new WaitForSeconds(pauseBetweenEvents);
        
        int lineIter = (int)(lineDrawTime / drawIter);
        for(int i = 0; i < events.Length; i++) {
            fastForward = false;
            // Render event marker
            GameObject nEventMarker = Instantiate (eventMarker);
            nEventMarker.transform.parent = gameObject.transform;
            nEventMarker.SetActive(true);
            RectTransform nEventMarkerRT = nEventMarker.GetComponent<RectTransform>();
            nEventMarkerRT.localPosition = new Vector3(nextXPos, yPos, 0);
            nEventMarkerRT.localScale = new Vector3(1, 1, 1);
            if(eventOutcomes[i]) {
                nEventMarker.GetComponent<Image>().sprite = goodEventMarker;
                StartCoroutine(PulseEventMarker(nEventMarker));
                GameObject particles = Instantiate(goodEventParticles);
                particles.transform.position = new Vector3(nEventMarker.transform.position.x, nEventMarker.transform.position.y, 0.1f);
            } else {
                nEventMarker.GetComponent<Image>().sprite = badEventMarker;
                StartCoroutine(ShakeEventMarker(nEventMarker));
            }

            if(!fastForward) yield return new WaitForSeconds(renderEventTime);

            // Render text
            GameObject nTextBox = Instantiate (textBox);
            nTextBox.transform.parent = gameObject.transform;
            nTextBox.SetActive(true);
            RectTransform nTextBoxRT = nTextBox.GetComponent<RectTransform>();
            nTextBoxRT.localPosition = new Vector3(nextXPos, yPos - spaceBetweenText + textMoveInY, 0);
            nTextBoxRT.localScale = new Vector3(1, 1, 1);
            TextMeshProUGUI nTextBoxTMP = nTextBox.GetComponent<TextMeshProUGUI>();
            nTextBoxTMP.text = events[i];
            nTextBoxTMP.faceColor = new Color(nTextBoxTMP.faceColor.r, nTextBoxTMP.faceColor.g, nTextBoxTMP.faceColor.b, 0);

            StartCoroutine(ChangeBackground(eventOutcomes[i], (i == events.Length - 1)));

            // Fade text in + bring it down
            for(int j = 0; j < fadeIn; j++) {
                float frac = (float)j / (float)fadeIn;
                float newAlpha = Mathf.Lerp(0, 1.0f, frac);
                nTextBoxTMP.faceColor = new Color(nTextBoxTMP.faceColor.r, nTextBoxTMP.faceColor.g, nTextBoxTMP.faceColor.b, newAlpha);
                float newYPos = Mathf.Lerp(nTextBoxRT.localPosition.y, yPos - spaceBetweenText, frac);
                nTextBoxRT.localPosition = new Vector3(nextXPos, newYPos, 0);

                if(!fastForward) yield return new WaitForSeconds(drawIter);
            }

            // Wait
            if(!fastForward) yield return new WaitForSeconds(pauseBetweenEvents + Random.Range(0.0f, jitterTime));
            nextXPos += spaceBetweenEvents;
            
            // Lengthen line, move people's images
            if(i < events.Length - 1) {
                float currLineWidth = lineRT.sizeDelta.x;
                float newLineWidth = currLineWidth + spaceBetweenEvents;
                float lineXOffset = spaceBetweenEvents / (2 * lineIter);
                for(int j = 0; j < lineIter; j++) {
                    // Adjust width
                    float frac = (float)j / (float)lineIter;
                    float newWidth = Mathf.Lerp(currLineWidth, newLineWidth, frac);
                    lineRT.sizeDelta = new Vector2 (newWidth, lineRT.sizeDelta.y);

                    // Adjust position
                    lineRT.localPosition = new Vector3(lineRT.localPosition.x + lineXOffset, yPos, 0);
                    charactersRT.localPosition = new Vector3(charactersRT.localPosition.x + spaceBetweenEvents / lineIter, charactersRT.localPosition.y, 0);
                    if(!fastForward) yield return new WaitForSeconds(drawIter);
                }
            }
        }

        StartCoroutine(ActivateExit());
    }

    // Start is called before the first frame update
    private void Start()
    {
        eventMarker.SetActive(false);
        textBox.SetActive(false);
        characters.SetActive(false);
        fadeOut.SetActive(false);
        exitText.faceColor = new Color(exitText.faceColor.r, exitText.faceColor.g, exitText.faceColor.b, 0);
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0)) {
            if(canExit) {
                SceneManager.LoadScene(1);
            }
            fastForward = true;
        }
    }

    public void TimelineAnimation(string[] e, bool[] eOutcomes) {
        events = e;
        eventOutcomes = eOutcomes;
        StartCoroutine(PlayTimeline());
    }
}
