using System;
using System.Collections;
using System.Collections.Generic;

public class ReactionsData
{
    private Dictionary<Pair<int, int>, int> _dict;

    public ReactionsData()
    {
        // TODO: maybe use ReagentLibItems instead of their ids?
        _dict = new Dictionary<Pair<int, int>, int>();

        loadDictionary();
    }

    // TODO: HACK - load json?
    private void loadDictionary()
    {
        _dict.Add(new Pair<int, int>(0, 2), 4);
        _dict.Add(new Pair<int, int>(2, 3), 5);
        _dict.Add(new Pair<int, int>(0, 3), 6);
        _dict.Add(new Pair<int, int>(3, 4), 7);
    }

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
        if (_dict.TryGetValue(key, out value))
            return value;
        else
            return -1;
    }
}