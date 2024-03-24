using System;
using _Game.Scripts.Tools;
using _Project.Scripts.Inventory.Interfaces;
using _Project.Scripts.Tools;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _Project.Scripts.Inventory.Views
{
    public class InventorySlot : MonoBehaviour, IBeginDragHandler ,  IDragHandler,  IPointerClickHandler, IEndDragHandler
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _amountText;

        private IInventorySlotPresenter _inventorySlotPresenter;
        private CompositeDisposable _disposable;

        private RectTransform _rectTransform;
        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
        }

        public void SetUp(IInventorySlotPresenter inventorySlotPresenter)
        {
            _inventorySlotPresenter = inventorySlotPresenter;
            _disposable = new CompositeDisposable();
            
            _inventorySlotPresenter.Amount
                .Subscribe(OnAmountChanged)
                .AddTo(_disposable);
            
            _inventorySlotPresenter.ItemId
                .Subscribe(OnItemIdChanged)
                .AddTo(_disposable);
            
            _inventorySlotPresenter.IsEmpty
                .Subscribe(OnItemIsEmptyChanged)
                .AddTo(_disposable);
        }
        
        private void SetAmount(string amount)
        {
            _amountText.text = amount;
        }

        private void SetSprite(Sprite sprite)
        {
            _icon.sprite = sprite;
        }

        private GameObject imageObject;
        public void OnBeginDrag(PointerEventData eventData)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                _rectTransform,
                eventData.position,
                eventData.pressEventCamera,
                out var pos);
            imageObject = new GameObject("Image");
            imageObject.transform.SetParent(transform);
            imageObject.transform.SetPositionAndRotation(_rectTransform.position, _rectTransform.rotation);
            Image image = imageObject.AddComponent<Image>();
            image.raycastTarget = false;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Log.ColorLog($"OnPointerClick");
        }

        public void OnDrag(PointerEventData eventData)
        {
            imageObject.transform.localPosition += new Vector3(eventData.delta.x, eventData.delta.y, 0);
        }
        
        private void OnAmountChanged(int amount)
        {
            SetAmount(_inventorySlotPresenter.GetConvertedItemAmount());
        }
        
        private void OnItemIdChanged(string itemId)
        {
            SetSprite(_inventorySlotPresenter.Icon);
        }
        
        private void OnItemIsEmptyChanged(bool isEmpty)
        {
            _amountText.SetActive(!isEmpty);
            _icon.SetActive(!isEmpty);
        }

        private void OnDestroy()
        {
            _disposable?.Dispose();
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            imageObject.Destroy();
            if (eventData.pointerCurrentRaycast.gameObject != null)
            {
                if (eventData.pointerCurrentRaycast.gameObject.TryGetComponent<InventorySlot>(out var slot))
                {
                    _inventorySlotPresenter.EndDragCommand.Execute(new SwitchItemSlots(
                        _inventorySlotPresenter.Position,
                        slot._inventorySlotPresenter.Position));
                }
            }
        }
    }
}