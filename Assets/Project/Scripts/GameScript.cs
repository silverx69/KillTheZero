using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(UIScript))]
public class GameScript : MonoBehaviour
{
    [Header("Quit key")]
    // the key used to activate the push
    public KeyCode key = KeyCode.Escape;

    private UIScript uiscript;
    private bool keyPressed = false;

    void Start()
    {
        var t = Resources.Load<Texture2D>("eCash-Logo");

        uiscript = GetComponent<UIScript>();
        uiscript.AddResource(0, 0, Sprite.Create(t, new Rect(0, 0, t.width, t.height), new Vector2(.5f, .5f), 100f));
    }

    private void Update() {
        if (keyPressed = Input.GetKey(key))
#if RUNTIME_BUILD
            Application.Quit();
#else
            EditorApplication.ExitPlaymode();
#endif
    }
}
