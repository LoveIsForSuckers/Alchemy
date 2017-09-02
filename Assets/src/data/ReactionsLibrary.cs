using System;
using System.Collections;
using System.Collections.Generic;

public class ReactionLibrary
{
    private IEnumerable<ReactionLibItem> _list;
    private Dictionary<Pair<int, int>, int> _lookupDict;

    public ReactionLibrary(IEnumerable<ReactionLibItem> sourceList)
    {
        _list = sourceList;
        _lookupDict = new Dictionary<Pair<int, int>, int>();

        Pair<int, int> lookupKey;
        foreach (ReactionLibItem item in sourceList)
        {
            lookupKey = new Pair<int, int>(item.FirstSourceReagentId, item.SecondSourceReagentId);
            _lookupDict[lookupKey] = item.ResultReagentId;
        }
    }

    public IEnumerable<ReactionLibItem> List { get { return _list; } }

    public int GetReactionResultId(int reagentId1, int reagentId2)
    {
        if (reagentId1 > reagentId2)
        {
            int tmp = reagentId2;
            reagentId2 = reagentId1;
            reagentId1 = tmp;
        }

        Pair<int, int> key = new Pair<int, int>(reagentId1, reagentId2);

        int value;
        if (_lookupDict.TryGetValue(key, out value))
            return value;
        else
            return -1;
    }
}