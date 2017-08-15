using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReactionController : MonoBehaviour
{
    public GameObject reagentPrefab;
    public Image reagentContainer;
    public Image workTable;

    private ReactionsData _reactionsData;
    private ReagentLibrary _reagentLibrary;

	void Awake ()
    {
        _reagentLibrary = new ReagentLibrary();
        _reactionsData = new ReactionsData();

        var basicElements = _reagentLibrary.GetBasicElements();
        foreach (ReagentLibItem libItem in basicElements)
        {
            makeReagentView(libItem, reagentContainer.transform);
        }
	}

    public void OnReagentDropToTable()
    {
        if (workTable.transform.childCount >= 2)
        {
            List<ReagentDisplay> displaysOnTable = new List<ReagentDisplay>();
            workTable.GetComponentsInChildren<ReagentDisplay>(displaysOnTable);
            if (displaysOnTable.Count < 2)
                return;

            // TODO: HACK-ish
            ReagentDisplay reagent1 = displaysOnTable[0];
            ReagentDisplay reagent2 = displaysOnTable[1];
            tryCombineReagents(reagent1, reagent2);
        }
    }

    private void tryCombineReagents(ReagentDisplay reagent1, ReagentDisplay reagent2)
    {
        int reactionResultId = _reactionsData.GetReactionResultId(reagent1.Data.id, reagent2.Data.id);
        cleanupReaction(reagent1, reagent2);

        if (reactionResultId != -1)
        {
            var libItem = _reagentLibrary.Reagents[reactionResultId];
            makeReagentView(libItem, reagentContainer.transform);
            makeReagentView(libItem, workTable.transform);
        }
    }

    private void cleanupReaction(ReagentDisplay reagent1, ReagentDisplay reagent2)
    {
        Destroy(reagent1.gameObject);
        Destroy(reagent2.gameObject);
    }

    private void makeReagentView(ReagentLibItem libItem, Transform parent)
    {
        GameObject reagentView = Instantiate<GameObject>(reagentPrefab);
        reagentView.transform.SetParent(parent);
        reagentView.name = "RD " + libItem.title;

        ReagentDisplay reagentDisplay = reagentView.GetComponent<ReagentDisplay>();
        reagentDisplay.SetData(libItem);
        reagentDisplay.SetController(this);
    }
}
