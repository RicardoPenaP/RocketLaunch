using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class Menu<T> : MonoBehaviour where T : Menu<T>
{
    private readonly int OPEN_MENU_ANIMATION_HASH = Animator.StringToHash("OpenMenu");
    private readonly int CLOSE_MENU_ANIMATION_HASH = Animator.StringToHash("CloseMenu");

    public static event EventHandler OnAnyMenuOpened;
    public static event EventHandler OnAnyMenuClosed;
    public static T Instance { get; private set; }

    public event Action OnMenuOpened;
    public event Action OnMenuClosed;

    protected Action onOpenAnimationEndedActions;
    protected Action onCloseAnimationEndedActions;

    private Animator animator;
    protected bool menuOpened = false;

    protected virtual void Awake()
    {
        if (!Instance)
        {
            Instance = (T)this;
        }
        else
        {
            Destroy(this);
        }
        animator = GetComponent<Animator>();
    }    

    protected virtual void OpenMenu(Action onOpenAnimationEndedActions = null)
    {
        menuOpened = true;
        gameObject.SetActive(true);
        animator.SetTrigger(OPEN_MENU_ANIMATION_HASH);
        OnMenuOpened?.Invoke();
        OnAnyMenuOpened?.Invoke(this, EventArgs.Empty);
        this.onOpenAnimationEndedActions = onOpenAnimationEndedActions;
    }

    protected virtual void CloseMenu(Action onCloseAnimationEndedActions = null)
    {
        menuOpened = false;
        animator.SetTrigger(CLOSE_MENU_ANIMATION_HASH);
        this.onCloseAnimationEndedActions = onCloseAnimationEndedActions;
        this.onCloseAnimationEndedActions += () => { gameObject.SetActive(false); };
        this.onCloseAnimationEndedActions += () => { OnMenuClosed?.Invoke(); };
        this.onCloseAnimationEndedActions += () => { OnAnyMenuClosed?.Invoke(this, EventArgs.Empty); };
    }

    private void Animator_OpenMenuAnimationFinished()
    {
        onOpenAnimationEndedActions?.Invoke();
    }

    private void Animator_CloseMenuAnimationFinished()
    {
        onCloseAnimationEndedActions?.Invoke();
    }

   
}
