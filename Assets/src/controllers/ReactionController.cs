using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReactionController : MonoBehaviour
{
    [SerializeField]
    private GameObject _reagentPrefab;
    [SerializeField]
    private Image _reagentContainer;
    [SerializeField]
    private Image _workTable;
    [SerializeField]
    private StatsDisplay _statsDisplay;

    private const int WORK_TABLE_PADDING = 4;

    private List<ReagentDisplay> _reagentsOnTable;
    private List<ReagentDisplay> _reagentsOnPanel;

	void Awake ()
    {
        if (Library.Instance == null)
        {
            Debug.Log("[ReactionController] Library unavailable"); // TODO: mock data?
            return;
        }

        var basicElements = Library.Instance.Reagents.GetBasicElements();
        foreach (ReagentLibItem libItem in basicElements)
        {
            makeReagentDisplay(libItem, _reagentContainer.transform);
        }

        _reagentsOnTable = new List<ReagentDisplay>();
        _reagentsOnPanel = new List<ReagentDisplay>(_reagentContainer.GetComponentsInChildren<ReagentDisplay>());

        _statsDisplay.ReagentCount = _reagentsOnPanel.Count;
        _statsDisplay.MaxReagentCount = Library.Instance.Reagents.Count;
	}

    public void OnReagentDropToTable(ReagentDisplay reagent)
    {
        bool wasReaction = false;
        foreach (var tableReagent in _reagentsOnTable)
        {
            if (reagent.RectTransform.IsIntersecting(tableReagent.RectTransform))
            {
                tryCombineReagents(reagent, tableReagent);
                wasReaction = true;
                break;
            }
        }
        
        if (!wasReaction)
            _reagentsOnTable.Add(reagent);

        // TODO: SHOW PARTICLES
    }

    private ReagentDisplay tryCombineReagents(ReagentDisplay reagent1, ReagentDisplay reagent2)
    {
        int reactionResultId = Library.Instance.Reactions.GetReactionResultId(reagent1.Data.id, reagent2.Data.id);
        cleanupReaction(reagent1, reagent2);

        if (reactionResultId != -1)
        {
            var libItem = Library.Instance.Reagents.GetItem(reactionResultId);
            var targetPosition = Vector3.Lerp(reagent1.LocalPosition, reagent2.LocalPosition, 0.5f);
            var reactionResult = makeReagentDisplay(libItem, _workTable.transform);
            reactionResult.LocalPosition = targetPosition;
            
            _reagentsOnTable.Add(reactionResult);

            _statsDisplay.TotalReactions++;
			
			// TODO: store player progress instead
			foreach (ReagentDisplay display in _reagentContainer.GetComponentsInChildren<ReagentDisplay>())
			{
				if (display.Data.id == reactionResultId)
                    return reactionResult;
			}
			
		    _reagentsOnPanel.Add(makeReagentDisplay(libItem, _reagentContainer.transform));
            _statsDisplay.ReagentCount++;

            return reactionResult;
        }
        else
        {
            return null;
        }
    }

    private void cleanupReaction(ReagentDisplay reagent1, ReagentDisplay reagent2)
    {
        _reagentsOnTable.Remove(reagent1);
        _reagentsOnTable.Remove(reagent2);

        Destroy(reagent1.gameObject);
        Destroy(reagent2.gameObject);
    }

    private ReagentDisplay makeReagentDisplay(ReagentLibItem libItem, Transform parent)
    {
        GameObject reagentView = Instantiate<GameObject>(_reagentPrefab);
        reagentView.transform.SetParent(parent);
        reagentView.name = "RD " + libItem.title;

        ReagentDisplay reagentDisplay = reagentView.GetComponent<ReagentDisplay>();
        reagentDisplay.SetData(libItem);
        reagentDisplay.SetController(this);
        return reagentDisplay;
    }

    public Image ReagentContainer
    {
        get { return _reagentContainer; }
    }

    public Image WorkTable
    {
        get { return _workTable; }
    }
}
