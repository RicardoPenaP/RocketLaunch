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
        if (Instance && Instance != this)
        {
            Debug.Log(this + "not instance");
            Destroy(gameObject);
        }
        else
        {
            Instance = (T)this;
        }
        
        animator = GetComponent<Animator>();
    }   
    
    protected virtual void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    protected virtual void OpenMenu(Action onOpenAnimationEndedActions = null)
    {
        if (menuOpened)
        {
            return;
        }
        menuOpened = true;        
        if (gameObject)
        {
            gameObject.SetActive(true);
        }       
        animator.SetTrigger(OPEN_MENU_ANIMATION_HASH);
        OnMenuOpened?.Invoke();
        OnAnyMenuOpened?.Invoke(this, EventArgs.Empty);        
        this.onOpenAnimationEndedActions = onOpenAnimationEndedActions;
    }

    protected virtual void CloseMenu(Action onCloseAnimationEndedActions = null)
    {
        if (!menuOpened)
        {
            return;
        }
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
