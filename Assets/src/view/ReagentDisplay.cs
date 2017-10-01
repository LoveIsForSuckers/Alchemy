using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using System.Collections.Generic;

public class ReagentDisplay : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private ReagentLibItem _data;
    private Image _image;
    private LayoutElement _layout;
    private ReactionController _controller;

    private Transform _currentParent;
    private Vector2 _dragOffset;
    private Vector3? _tempLocalPosition;

    void Start()
    {
        _layout = GetComponent<LayoutElement>();
        _image = GetComponent<Image>();

        tryUpdateDisplay();
    }

    public void SetController(ReactionController controller)
    {
        _controller = controller;
    }

    public void SetData(ReagentLibItem data)
    {
        _data = data;
        tryUpdateDisplay();
    }

    private void tryUpdateDisplay()
    {
        if (_image != null)
        {
            _image.color = _data.color;
            _image.GetComponentInChildren<Text>().text = _data.title;

            if (_tempLocalPosition != null)
            {
                _image.rectTransform.localPosition = _tempLocalPosition ?? default(Vector3);
                _tempLocalPosition = null;
            }
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (transform.parent == _controller.ReagentContainer.transform)
        {
            var copy = Instantiate<GameObject>(gameObject, transform.parent, true);
            var copyDisplay = copy.GetComponent<ReagentDisplay>();
            copyDisplay.SetData(_data);
            copyDisplay.SetController(_controller);
            copy.name = name;
            copy.transform.SetSiblingIndex(transform.GetSiblingIndex());
        }

        var thisTransform = transform;
        _layout.ignoreLayout = true;
        _currentParent = _controller.transform;
        thisTransform.SetParent(_currentParent);
        _dragOffset = new Vector2(thisTransform.position.x - eventData.pressPosition.x, thisTransform.position.y - eventData.pressPosition.y);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position + _dragOffset;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _layout.ignoreLayout = false;

        if (RectTransform.IsFullyInside(_controller.WorkTable.rectTransform))
        {
            _currentParent = _controller.WorkTable.transform;
            transform.SetParent(_currentParent);
            _controller.OnReagentDropToTable(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public ReagentLibItem Data
    {
        get
        {
            return _data;
        }
    }

    public RectTransform RectTransform
    {
        get { return _image ? _image.rectTransform : null; }
    }

    public Vector3 LocalPosition
    {
        get { return _image != null ? _image.rectTransform.localPosition : new Vector3(); }
        set
        {
            if (_image != null)
                _image.rectTransform.localPosition = value;
            else
                _tempLocalPosition = value;
        }
    }

    
}
