using UnityEngine;
using System.Collections.Generic;
using Newtonsoft.Json;
using System;

public class ReagentLibrary
{
    // TODO: HACK
    static private readonly int[] BASIC_ELEMENT_IDS = { 1, 2, 3, 8 };
    
    private Dictionary<int, ReagentLibItem> _dict;

    public ReagentLibrary(IEnumerable<ReagentLibItem> sourceList)
    {
        _dict = new Dictionary<int, ReagentLibItem>();

        foreach(ReagentLibItem item in sourceList)
        {
            _dict[item.id] = item;
        }
    }

    public List<ReagentLibItem> GetBasicElements()
    {
        List<ReagentLibItem> result = new List<ReagentLibItem>();
        foreach (var id in BASIC_ELEMENT_IDS)
        {
            result.Add(_dict[id]);
        }
        return result;
    }

    public ReagentLibItem GetItem(int id)
    {
        ReagentLibItem result;
        if (_dict.TryGetValue(id, out result))
            return result;
        else
            return null;
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
