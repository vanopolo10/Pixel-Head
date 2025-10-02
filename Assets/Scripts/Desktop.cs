using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Button))]
public class Desktop : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Computer _computer;
    [SerializeField] private Image _buttonsPanel;
    [SerializeField] private Image _createPanel;
    
    private void OnEnable()
    {
        _computer.ScreenOpened += OnScreenOpen;
        
        _buttonsPanel.gameObject.SetActive(false);
    }
    
    private void OnDisable()
    {
        _computer.ScreenOpened -= OnScreenOpen;
    }

    private void OnScreenOpen()
    {
        _buttonsPanel.gameObject.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _createPanel.gameObject.SetActive(false);
        
        switch (eventData.button)
        {
            case PointerEventData.InputButton.Right:
                _buttonsPanel.transform.position = eventData.position;
                _buttonsPanel.gameObject.SetActive(true);
                break;
            case PointerEventData.InputButton.Left:
                _buttonsPanel.gameObject.SetActive(false);
                break;
        }
    }
}
