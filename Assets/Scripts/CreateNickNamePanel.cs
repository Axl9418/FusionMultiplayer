using Fusion;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreateNickNamePanel : LobbyPanelBase
{
    [Header("CreateNicknamePanel Vars")]
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private Button createNickNameBtn;

    private const int Max_Char_NickName = 2;

    public override void InitPanel(LobbyIUManager lobbyIUManager)
    {
        base.InitPanel(lobbyIUManager);

        createNickNameBtn.interactable = false;
        createNickNameBtn.onClick.AddListener(onClickCreateNickName);
        inputField.onValueChanged.AddListener(OnInputValueChanged);
    }

    private void OnInputValueChanged(string arg0)
    {
        createNickNameBtn.interactable = arg0.Length >= Max_Char_NickName;
    }

    private void onClickCreateNickName()
    {
        var nickName = inputField.text;

        if (nickName.Length >= Max_Char_NickName)
        {
            base.ClosePanel();
            lobbyIUManager.ShowPanel(LobbyPanelType.MiddleSectionPanel);
        }
    }
}
