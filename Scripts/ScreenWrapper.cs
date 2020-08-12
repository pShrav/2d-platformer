// Functionality to wrap screen (left-right and top-bottom)

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenWrapper : MonoBehaviour
{
    private void OnBecameInvisible()     // when object renderer is no longer active
    {
        if (this.gameObject.activeSelf)    // only when object is off-screen, not dead
        {
            // Update position based on screen location

            Vector2 thisPosition = transform.position;

            if (Camera.main.WorldToViewportPoint(transform.position).y > 1 || Camera.main.WorldToViewportPoint(transform.position).y  < 0) // off top of screen
            {
                thisPosition.y = -thisPosition.y;
            }

            if (Camera.main.WorldToViewportPoint(transform.position).x > 1 || Camera.main.WorldToViewportPoint(transform.position).x  < 0) // off side of screen
            {
                thisPosition.x = -thisPosition.x;
            }

            transform.position = thisPosition;
        }
    }
}
