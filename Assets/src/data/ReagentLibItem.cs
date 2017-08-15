using UnityEngine;

public class ReagentLibItem
{
    public int id;
    public string title;
    public Color32 color;

    public ReagentLibItem()
    {
        color.a = 255;
    }

    public ReagentLibItem(int id, string title, Color32 color)
    {
        this.id = id;
        this.title = title;
        this.color = color;
    }

    public ReagentLibItem(int id, string title, byte red = 255, byte green = 255, byte blue = 255, byte alpha = 255)
    {
        this.id = id;
        this.title = title;
        this.color = new Color32(red, green, blue, alpha);
    }

    public byte Red
    {
        get { return color.r; }
        set { color.r = value; }
    }

    public byte Green
    {
        get { return color.g; }
        set { color.g = value; }
    }

    public byte Blue
    {
        get { return color.b; }
        set { color.b = value; }
    }
}
