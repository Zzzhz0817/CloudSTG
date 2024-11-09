using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResumeButtom : MonoBehaviour
{
    [SerializeField] Player player;

    void Update()
    {
        if (Input.touchCount == 0)
        {
        Vector2 ViewportPos;
        ViewportPos = Camera.main.WorldToViewportPoint(player.transform.position);
        GetComponent<RectTransform>().anchoredPosition = new Vector2(Screen.width * ViewportPos.x, Screen.height * ViewportPos.y);
        Debug.Log(new Vector2(Screen.width * ViewportPos.x, Screen.height * ViewportPos.y));
        }
    }

}
