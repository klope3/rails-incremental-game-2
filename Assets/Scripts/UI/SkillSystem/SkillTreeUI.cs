using System.Collections.Generic;
using UnityEngine;

public class SkillTreeUI : MonoBehaviour
{
    [SerializeField] private PlayerSkills playerSkills;
    [SerializeField] private SkillTreeHelper helper;
    [SerializeField] private SkillHandler skillHandler;
    [SerializeField] private SkillTreeNodeUI nodePf;
    [SerializeField] private Transform nodesParent;
    [SerializeField] private Transform linesParent;
    [SerializeField] private UILine uiLine;
    [SerializeField] private float gridSize;
    private List<SkillTreeNodeUI> nodes;

    public void Initialize()
    {
        nodes = new List<SkillTreeNodeUI>();
        DrawTree();
        playerSkills.OnSkillsChange += PlayerSkills_OnSkillAdded;
    }

    private void PlayerSkills_OnSkillAdded()
    {
        UpdateNodeVisuals();
    }

    [Sirenix.OdinInspector.Button]
    private void DrawTree()
    {
        DrawNode(helper.RootNode);
        UpdateNodeVisuals();
    }

    private void UpdateNodeVisuals()
    {
        foreach (SkillTreeNodeUI node in nodes)
        {
            node.UpdateVisuals(skillHandler.GetSkillStatus(node.nodeSO));
        }
    }

    private void DrawNode(SkillTreeNodeSO node)
    {
        SkillTreeNodeUI nodeObj = Instantiate(nodePf, nodesParent);
        nodeObj.nodeSO = node;
        nodeObj.Initialize();
        nodeObj.GetComponent<RectTransform>().anchoredPosition = ScaleGridPosition(node.GridPosition);
        nodeObj.OnClick += NodeObj_OnClick;
        if (nodes != null)
        {
            nodes.Add(nodeObj);
        }

        foreach (SkillTreeNodeSO child in node.Children)
        {
            uiLine.Draw(ScaleGridPosition(node.GridPosition), ScaleGridPosition(child.GridPosition), Color.white, linesParent);
            DrawNode(child);
        }
    }

    private void NodeObj_OnClick(SkillTreeNodeUI clickedNode)
    {
        skillHandler.TryPurchase(clickedNode.nodeSO);
    }

    private Vector2 ScaleGridPosition(Vector2Int gridPosition)
    {
        return new Vector2(gridPosition.x, gridPosition.y) * gridSize;
    }
}
