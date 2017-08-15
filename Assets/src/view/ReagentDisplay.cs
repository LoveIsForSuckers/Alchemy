using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using System.Collections.Generic;

public class ReagentDisplay : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private ReagentLibItem _data;
    private Image _image;
    private ReactionController _controller;

    void Start()
    {
        _image = GetComponent<Image>();
        if (_image == null)
        {
            Debug.LogWarning("[ReagentDisplay] Image not found on Start!");
        }

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
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (transform.parent == _controller.reagentContainer.transform)
        {
            var copy = Instantiate<GameObject>(gameObject, transform.parent, true);
            var copyDisplay = copy.GetComponent<ReagentDisplay>();
            copyDisplay.SetData(_data);
            copyDisplay.SetController(_controller);
            copy.name = name;
            copy.transform.SetSiblingIndex(transform.GetSiblingIndex());
        }

        transform.SetParent(transform.parent.parent);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        foreach (RaycastResult result in results)
        {
            if (result.gameObject == _controller.workTable.gameObject)
            {
                transform.SetParent(_controller.workTable.transform);
                _controller.OnReagentDropToTable();
                break;
            }
            else if (result.gameObject == _controller.reagentContainer.gameObject)
            {
                Destroy(gameObject);
                break;
            }
        }
    }

    public ReagentLibItem Data
    {
        get
        {
            return _data;
        }
    }
}
