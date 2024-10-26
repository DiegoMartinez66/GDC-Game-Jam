using UnityEngine;

public class UnlockMouse : MonoBehaviour
{
    void Awake()
    {
        // Show & unlock cursor
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
