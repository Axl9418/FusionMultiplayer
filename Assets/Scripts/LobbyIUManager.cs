using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyIUManager : MonoBehaviour
{
    [SerializeField] private LoadingCanvasController loadingCanvasController;
    [SerializeField] private LobbyPanelBase[] lobbyPanels;

    private void Start()
    {
        foreach (var lobby in lobbyPanels)
        {
            lobby.InitPanel(uiManger: this);
        }

        Instantiate(loadingCanvasController);
    }

    public void ShowPanel(LobbyPanelBase.LobbyPanelType type)
    {
        foreach (var lobby in lobbyPanels)
        {
            if (lobby.PanelType == type)
            {
                //show panel
                lobby.ShowPanel();

                break;
            }
        }
    }
}
