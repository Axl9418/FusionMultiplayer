using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;

public class LobbyPanelBase : MonoBehaviour
{
    [field: SerializeField, Header("LobbyPanelBase Vars")]
    public LobbyPanelType PanelType { get; private set; }
    [SerializeField] private Animator panelAnimator;

    protected LobbyIUManager lobbyIUManager;

    public enum LobbyPanelType
    {
        None,
        CreateNicknamePanel,
        MiddleSectionPanel
    }

    public virtual void InitPanel(LobbyIUManager uiManger)
    {
        lobbyIUManager = uiManger;
    }

    public void ShowPanel()
    {
        this.gameObject.SetActive(true);
        const string Pop_In_Clip_Name = "In";
        panelAnimator.Play(Pop_In_Clip_Name);
    }

    protected void ClosePanel()
    {
        const string Pop_In_Clip_Name = "Out";
        
        //animation delay
        StartCoroutine(routine: Utils.PlayAnimationSetStateWhenFinished(gameObject, panelAnimator, Pop_In_Clip_Name, activeStateAtTheEnd:false));
    }
}
