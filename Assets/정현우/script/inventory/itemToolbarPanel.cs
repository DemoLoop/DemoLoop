// 2024-09-19 작성자 : 정현우


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemToolbarPanel : ItemPanel
{
    [SerializeField] ToolbarController toolbarController;

    private void Start()
    {
        Init();
        toolbarController.onChange += Highlight;
        Highlight(0);
    }
    public override void OnClick(int id)
    {
        toolbarController.Set(id);
        Highlight(id);
    }

    int currentSelectedTool;
    public void Highlight(int id)
    {
        buttons[currentSelectedTool].HighLight(false);
        currentSelectedTool = id;
        buttons[currentSelectedTool].HighLight(true);
    }
}
