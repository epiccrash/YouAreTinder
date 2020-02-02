using System.Collections;
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
    public GameObject p1Sprite;
    public GameObject p2Sprite;
    public TextMeshProUGUI exitText;
    public Image background;
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
    private RectTransform p1SpriteRT;
    private RectTransform p2SpriteRT;
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
        // Fade text in + bring it down
        for(int j = 0; j < fadeIn; j++) {
            float frac = (float)j / (float)fadeIn;
            float newAlpha = Mathf.Lerp(0, 1.0f, frac);
            exitText.faceColor = new Color(exitText.faceColor.r, exitText.faceColor.g, exitText.faceColor.b, newAlpha);
            
            yield return new WaitForSeconds(drawIter);
        }

        canExit = true;
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
                Debug.Log(markerRT.localScale.x);

                frac += 1.0f / (float)pulseSpeed;
                yield return new WaitForEndOfFrame();
        }

        // Pulse back in
        frac =1.0f / (float)pulseSpeed;
        for(int k = 0; k < pulseSpeed; k++) {
                float lerp = Mathf.Lerp(pulseAmount, 1.0f, frac);
                markerRT.localScale = new Vector3(lerp, lerp, 1);

                frac += 1.0f / (float)pulseSpeed;
                yield return new WaitForEndOfFrame();
        }
    }

    private IEnumerator PlayTimeline()
    {
        nextXPos = lineRenderer.GetComponent<RectTransform>().localPosition.x;
        yPos = lineRenderer.GetComponent<RectTransform>().localPosition.y;
        lineRT = lineRenderer.GetComponent<RectTransform>();
        p1SpriteRT = p1Sprite.GetComponent<RectTransform>();
        p2SpriteRT = p2Sprite.GetComponent<RectTransform>();

        // Fade background color in
        Color ogColor = background.color;
        float colorF = 1.0f / (float)fadeIn;
        for(int i = 0; i < fadeIn; i++) {
            background.color = Color.Lerp(ogColor, startBgColor, colorF);
            colorF += 1.0f / (float)fadeIn;

            yield return new WaitForSeconds(drawIter);
        }

        yield return new WaitForSeconds(pauseBetweenEvents);
        
        // TODO: drop in from top
        p1Sprite.SetActive(true);
        p2Sprite.SetActive(true);
        yield return new WaitForSeconds(pauseBetweenEvents);
        
        int lineIter = (int)(lineDrawTime / drawIter);
        for(int i = 0; i < events.Length; i++) {
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

            yield return new WaitForSeconds(renderEventTime);

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

                yield return new WaitForSeconds(drawIter);
            }

            // Wait
            yield return new WaitForSeconds(pauseBetweenEvents + Random.Range(0.0f, jitterTime));
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
                    p1SpriteRT.localPosition = new Vector3(p1SpriteRT.localPosition.x + spaceBetweenEvents / lineIter, p1SpriteRT.localPosition.y, 0);
                    p2SpriteRT.localPosition = new Vector3(p2SpriteRT.localPosition.x + spaceBetweenEvents / lineIter, p2SpriteRT.localPosition.y, 0);
                    yield return new WaitForSeconds(drawIter);
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
        p1Sprite.SetActive(false);
        p2Sprite.SetActive(false);
        exitText.faceColor = new Color(exitText.faceColor.r, exitText.faceColor.g, exitText.faceColor.b, 0);
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0) && canExit) {
            // TODO: add scene to go back to
            SceneManager.LoadScene(0);
        }
    }

    public void TimelineAnimation(string[] e, bool[] eOutcomes) {
        events = e;
        eventOutcomes = eOutcomes;
        StartCoroutine(PlayTimeline());
    }
}
