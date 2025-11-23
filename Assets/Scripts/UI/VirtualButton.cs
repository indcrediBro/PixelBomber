using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VirtualButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private UnityEvent onPointerDown;
    [SerializeField] private UnityEvent onPointerUp;

    [SerializeField] private Sprite pressedSprite;
    [SerializeField] private Vector2 iconPressedPos;
    
    private Image imageComponent;
    private Sprite defaultSprite;
    private Transform childIcon;
    private Vector2 iconOriginalPos;
    
    private void Awake()
    {
        imageComponent = GetComponent<Image>();
        childIcon = transform.GetChild(0);
        defaultSprite = imageComponent.sprite;
        iconOriginalPos = childIcon.position;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        imageComponent.sprite = pressedSprite;
        childIcon.position = new Vector3(childIcon.position.x + iconPressedPos.x, childIcon.position.y+iconPressedPos.y, childIcon.position.z);
        onPointerDown?.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        imageComponent.sprite = defaultSprite;
        childIcon.position = iconOriginalPos;
        onPointerUp?.Invoke();
    }
}
