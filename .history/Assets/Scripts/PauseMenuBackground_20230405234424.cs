using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuBackground : MonoBehaviour
{
    public GameObject player;
    [SerializeField] Canvas pauseUI;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player");

        GetComponent<RectTransform>().anchoredPosition = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
    }
    void Update()
    {
        if (player.GetComponent<Player>().isPaused && !pauseUI.enabled)
        {
            Debug.Log(1);
            animator.SetTrigger("fadeIn");
        }

        if (pauseUI.enabled == false)
        {
        Vector2 ViewportPos;
        ViewportPos = Camera.main.WorldToViewportPoint(player.transform.position);
        GetComponent<RectTransform>().anchoredPosition = new Vector2(Screen.width * ViewportPos.x, Screen.height * ViewportPos.y);
        }
    }
}
