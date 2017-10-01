using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

static public class RectTransformExtensions
{
    static public bool IsFullyInside(this RectTransform item, RectTransform target)
    {
        Vector3[] corners = new Vector3[4];
        item.GetWorldCorners(corners);

        foreach (var corner in corners)
        {
            if (!RectTransformUtility.RectangleContainsScreenPoint(target, corner))
                return false;
        }

        return true;
    }

    static public bool IsIntersecting(this RectTransform item, RectTransform target)
    {
        Vector3[] corners = new Vector3[4];
        item.GetWorldCorners(corners);

        foreach (var corner in corners)
        {
            if (RectTransformUtility.RectangleContainsScreenPoint(target, corner))
                return true;
        }

        return false;
    }
}
