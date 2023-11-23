using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class Menu : MonoBehaviour
{
    private readonly int OPEN_MENU_ANIMATION_HASH = Animator.StringToHash("OpenMenu");
    private readonly int CLOSE_MENU_ANIMATION_HASH = Animator.StringToHash("CloseMenu");
    public event Action OnMenuOpened;
    public event Action OnMenuClosed;

    private Animator animator;

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
    }

    protected virtual void OpenMenu()
    {
        OnMenuOpened?.Invoke();
        animator.SetTrigger(OPEN_MENU_ANIMATION_HASH);
    
    }

    protected virtual void CloseMenu()
    {
        OnMenuClosed?.Invoke();
        animator.SetTrigger(CLOSE_MENU_ANIMATION_HASH);
    }
}
