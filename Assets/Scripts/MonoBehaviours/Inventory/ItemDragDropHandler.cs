using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemDragDropHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	public Item Item
	{
		get
		{
			return _item;
		}
		set
		{
			_item = value;

			if(_item == null)
			{
				_image.sprite = null;
				_image.enabled = false;
			}
			else
			{
				_image.sprite = _item.sprite;
				_image.preserveAspect = true;
				_image.enabled = true;
			}
		}
	}
	private Item _item;


	public Image Image
	{
		get
		{
			return _image;
		}
	}
	private Image _image;

	private Vector3 _beforeDragPosition;
	private Transform _beforeDragParent;

	public void OnBeginDrag(PointerEventData eventData)
	{
		if(!Item)
		{
			return;
		}
		_beforeDragPosition = transform.position;

		transform.position = eventData.position;
		GetComponent<CanvasGroup>().blocksRaycasts = false;

		_beforeDragParent = transform.parent;
		//TODO
		//transform.SetParent(transform.parent.parent);
	}

	public void OnDrag(PointerEventData eventData)
	{
		if (!Item)
		{
			return;
		}
		transform.position = eventData.position;
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		if (!Item)
		{
			return;
		}
		transform.SetParent(_beforeDragParent);
		transform.position = _beforeDragPosition;
		GetComponent<CanvasGroup>().blocksRaycasts = true;

		_beforeDragParent = null;
		_beforeDragPosition = default(Vector3);
	}

	void Start ()
	{
		_image = GetComponent<Image>();
	}
}
