using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuBackground : MonoBehaviour
{
    public GameObject player;
    [SerializeField] GameObject pauseUI;
    private CanvasGroup pauseImageCanvasGroup;

    /// <summary>
    /// Set the pause image's initial opacity to 0.
    /// This is here because when I put it in Start() it gave me a nullpointer.
    /// </summary>
    void Awake()
    {
        pauseImageCanvasGroup = pauseUI.GetComponent<CanvasGroup>();
        pauseImageCanvasGroup.alpha = 0f; // Start invisible
    }

    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    /// <summary>
    /// Called when the game is paused. Activate the pause canvas and calls ShowPauseImage()
    /// </summary>
    public void PauseGame()
    {
        pauseUI.SetActive(true);
        StartCoroutine(ShowPauseImage());
    }

    public void ResumeGame()
    {
        StartCoroutine(HidePauseImage());
    }

    /// <summary>
    /// Display the normal pause image (white clouds).
    /// Fade in: increases the opacity of the image linearly
    /// Zoom in: decreases the scale of the image on the smoothstep curve.
    /// </summary>
    /// <returns>An IEnumerator that allows the function to be run as co routine.</returns>
    IEnumerator ShowPauseImage()
    {
        // Get the Canvas Scaler's scale factor
        float scaleFactor = pauseUI.GetComponentInParent<CanvasScaler>().scaleFactor;

        // Debug.Log("Coroutine started");
        Vector2 viewportPos = Camera.main.WorldToViewportPoint(player.transform.position);
        // Debug.Log("Viewport Position: " + viewportPos);

        // Adjust screenPos by the scale factor
        Vector2 screenPos = new Vector2(Screen.width * viewportPos.x / scaleFactor, Screen.height * viewportPos.y / scaleFactor);
        Debug.Log("Screen Position: " + screenPos);

        GetComponent<RectTransform>().anchoredPosition = screenPos;

        float duration = 0.75f;
        float elapsedTime = 0f;

        pauseImageCanvasGroup.alpha = 0f; // Reset alpha

        // Get the Canvas Scaler component
        CanvasScaler canvasScaler = pauseUI.GetComponentInParent<CanvasScaler>(); 

        // Set the initial scale factor to 5
        canvasScaler.scaleFactor = 5f; 

        while (elapsedTime < duration)
        {
            
            elapsedTime += Time.unscaledDeltaTime;
            float t = Mathf.SmoothStep(0f, 1f, elapsedTime / duration); // smooooooth curves
            t = Mathf.SmoothStep(0f, 1f, t);

            // Fade in the alpha
            pauseImageCanvasGroup.alpha = elapsedTime / duration; 

            // Smoothly scale down the Canvas Scaler's scale factor
            canvasScaler.scaleFactor = Mathf.Lerp(5f, 1f, t);

            // Recalculate and update the position in each iteration
            viewportPos = Camera.main.WorldToViewportPoint(player.transform.position); // Update viewportPos
            Vector2 updatedScreenPos = new Vector2(Screen.width * viewportPos.x / canvasScaler.scaleFactor, 
                Screen.height * viewportPos.y / canvasScaler.scaleFactor);
            GetComponent<RectTransform>().anchoredPosition = updatedScreenPos; 

            yield return null;
        }
        

        // Ensure alpha is set to 1 at the end
        pauseImageCanvasGroup.alpha = 1f;
    }
    
    /// <summary>
    /// Hides the pause image when the game is resumed. Faster than show to ensure good pacing.
    /// </summary>
    /// <returns>An IEnumerator that allows the function to be run as co routine.</returns>
    IEnumerator HidePauseImage()
    {
        Debug.Log("HidePauseImage() started"); // Log when the coroutine starts

        float duration = 0.35f;
        float elapsedTime = 0f;

        // Get the Canvas Scaler component
        CanvasScaler canvasScaler = pauseUI.GetComponentInParent<CanvasScaler>();

        while (elapsedTime < duration)
        {
            Debug.Log("Hiding: elapsedTime = " + elapsedTime + ", alpha = " + pauseImageCanvasGroup.alpha + ", scaleFactor = " + canvasScaler.scaleFactor);

            elapsedTime += Time.unscaledDeltaTime; // Use unscaledDeltaTime for the timer

            // Apply easing to the interpolation factor 't'
            float t = Mathf.SmoothStep(0f, 1f, elapsedTime / duration);
            t = Mathf.SmoothStep(0f, 1f, t);
            t = Mathf.SmoothStep(0f, 1f, t);
            t = Mathf.SmoothStep(0f, 1f, t);

            // Fade out the alpha
            pauseImageCanvasGroup.alpha = Mathf.Lerp(1f, 0f, t); // Lerp from 1 to 0

            // Smoothly scale up the Canvas Scaler's scale factor
            canvasScaler.scaleFactor = Mathf.Lerp(1f, 2.5f, t); // Lerp from 1 to 2.5

            // Recalculate and update the position in each iteration
            Vector2 viewportPos = Camera.main.WorldToViewportPoint(player.transform.position);
            Vector2 screenPos = new Vector2(Screen.width * viewportPos.x / canvasScaler.scaleFactor, 
                Screen.height * viewportPos.y / canvasScaler.scaleFactor);
            GetComponent<RectTransform>().anchoredPosition = screenPos; 

            yield return null;
        }

        Debug.Log("HidePauseImage() finished"); // Log when the coroutine finishes

        // Ensure alpha is set to 0 at the end
        pauseImageCanvasGroup.alpha = 0f;

        // Deactivate the pause UI after the animation is complete
        pauseUI.SetActive(false); 
    }
}