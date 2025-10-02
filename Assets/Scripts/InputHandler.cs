using System;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(UnityEngine.UI.Outline))]
public class InputHandler : MonoBehaviour, IPointerClickHandler
{
    private UnityEngine.UI.Outline _outline;
    
    public event Action<string> InputEnded;
    public event Action InputCanceled;
    public event Action<UnityEngine.UI.Outline> Clicked;
    public event Action<FolderView> DoubleClicked;

    private void Awake()
    {
        _outline = GetComponent<UnityEngine.UI.Outline>();
    }

    private void OnDestroy()
    {
        InputEnded = null;
        InputCanceled = null;
        Clicked = null;
        DoubleClicked = null;
    }

    public void OnInputEnd(string input)
    {
        InputEnded?.Invoke(input);
    }
    
    public void OnInputCanceled()
    {
        InputCanceled?.Invoke();
    }

    public void OnFileClick()
    {
        Clicked?.Invoke(_outline);
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.clickCount == 2 && TryGetComponent(out FolderView folderView))
            DoubleClicked?.Invoke(folderView);
    }
}
