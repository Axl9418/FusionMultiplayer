using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Fusion;

public class MiddleSectionPanel : LobbyPanelBase
{
    [Header("MiddleSectionPanel Vars")]
    [SerializeField] private Button joinRandomRoomBtn;
    [SerializeField] private Button joinRandomByArgBtn;
    [SerializeField] private Button createRoomBtn;

    [SerializeField] private TMP_InputField joinRoomByArgInputField;
    [SerializeField] private TMP_InputField createRoomInputField;

    private NetworkRunnerController networkRunnerController;

    public override void InitPanel(LobbyIUManager uiManger)
    {
        base.InitPanel(uiManger);

        networkRunnerController = GlobalManagers.Instance.networkRunnerController;

        joinRandomRoomBtn.onClick.AddListener(JoinRandommRoom);
        joinRandomByArgBtn.onClick.AddListener(() => CreateRoom(GameMode.Client, joinRoomByArgInputField.text));
        createRoomBtn.onClick.AddListener(()=> CreateRoom(GameMode.Host, createRoomInputField.text));
    }


    private void CreateRoom(GameMode mode, string field)
    {
        if (field.Length >= 2)
        {
            Debug.Log($"---------{mode}---------");
            //create a room
            networkRunnerController.StartGame(mode, field);
        }

    }

    private void JoinRandommRoom()
    {
        Debug.Log($"---------JoinRandommRoom!---------");
        networkRunnerController.StartGame(GameMode.AutoHostOrClient, string.Empty);
    }



}
