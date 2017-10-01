using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class StatsDisplay : MonoBehaviour
{
    [SerializeField]
    private Text _unlockedReagentsTF;
    [SerializeField]
    private Text _totalReactionsTF;

    private int _maxReagentCount;
    private int _reagentCount;
    private int _totalReactions;

    private void updateDisplay()
    {
        _unlockedReagentsTF.text = String.Format("{0}/{1}", _reagentCount, _maxReagentCount);
        _totalReactionsTF.text = _totalReactions.ToString();
    }

    public int MaxReagentCount
    {
        get { return _maxReagentCount; }
        set
        { 
            _maxReagentCount = value;
            updateDisplay();
        }
    }

    public int ReagentCount
    {
        get { return _reagentCount; }
        set
        { 
            _reagentCount = value;
            updateDisplay();
        }
    }

    public int TotalReactions
    {
        get { return _totalReactions; }
        set
        {
            _totalReactions = value;
            updateDisplay();
        }
    }
}
