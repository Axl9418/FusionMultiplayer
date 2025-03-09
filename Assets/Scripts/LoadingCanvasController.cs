using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingCanvasController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Button cancelBtn;

    private NetworkRunnerController networkRunnerController;

    private void Start()
    {
        networkRunnerController = GlobalManagers.Instance.networkRunnerController;
        networkRunnerController.OnStartedRunnerConnection += OnStartedRunnerConnection;
        networkRunnerController.onPlayerJoinedSucessfully += onPlayerJoinedSucessfully;

        cancelBtn.onClick.AddListener(networkRunnerController.ShutDownRunner);
        this.gameObject.SetActive(false);
    }

    private void onPlayerJoinedSucessfully()
    {
        const string CLIP_NAME = "Out";
        StartCoroutine(Utils.PlayAnimationSetStateWhenFinished(gameObject, animator, CLIP_NAME, false));
    }

    private void OnStartedRunnerConnection()
    {
        this.gameObject.SetActive(true);
        const string CLIP_NAME = "In";
        StartCoroutine(Utils.PlayAnimationSetStateWhenFinished(gameObject, animator, CLIP_NAME));
    }

    private void OnDestroy()
    {
        networkRunnerController.OnStartedRunnerConnection -= OnStartedRunnerConnection;
        networkRunnerController.onPlayerJoinedSucessfully += onPlayerJoinedSucessfully;
    }
}
