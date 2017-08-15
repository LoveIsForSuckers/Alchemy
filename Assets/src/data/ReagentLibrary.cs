using UnityEngine;
using System.Collections.Generic;
using Newtonsoft.Json;
using System;

public class ReagentLibrary
{
    private List<ReagentLibItem> _reagents;

    public ReagentLibrary()
    {
        loadList();
    }

    private void loadList()
    {
        TextAsset source = Resources.Load<TextAsset>("ReagentLibrary");
        _reagents = JsonConvert.DeserializeObject<List<ReagentLibItem>>(source.text);
        _reagents.Sort(new ReagentComparer());
    }

    public List<ReagentLibItem> GetBasicElements()
    {
        List<ReagentLibItem> result = new List<ReagentLibItem>();
        result.AddRange(_reagents.GetRange(0, 4));
        return result;
    }

    public List<ReagentLibItem> Reagents
    {
        get { return _reagents; }
    }
}

public class ReagentComparer : IComparer<ReagentLibItem>
{
    public int Compare(ReagentLibItem x, ReagentLibItem y)
    {
        if (x.id < y.id)
            return -1;
        else if (x.id > y.id)
            return 1;
        else
            return 0;
    }
}
