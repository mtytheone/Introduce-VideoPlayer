using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDKBase;
using VRC.Udon;

public class LocalVideoController_InputURL : UdonSharpBehaviour
{
    [SerializeField]
    private VRC.SDK3.Video.Components.Base.BaseVRCVideoPlayer _videoPlayer;
    
    [SerializeField]
    private VRC.SDK3.Components.VRCUrlInputField _inputfield;

    [SerializeField]
    private Text _playTimeDisplayText;

    private float _videoDuration;
    private bool _isPausing;

    private void Update()
    {
        if (_videoPlayer.IsPlaying)
        {
            _playTimeDisplayText.text = string.Format("{0:f} / {1:f}", _videoPlayer.GetTime(), _videoDuration);
        }
    }

    public override void OnVideoStart()
    {
        _videoDuration = _videoPlayer.GetDuration();
    }

    public void PlayVideo()
    {
        _videoPlayer.Stop();
        _videoPlayer.PlayURL(_inputfield.GetUrl());
    }

    public void PauseVideo()
    {
        _isPausing = !_isPausing;

        if (_isPausing)
        {
            _videoPlayer.Pause();
        }
        else
        {
            _videoPlayer.Play();
        }
    }

    public void StopVideo()
    {
        _videoPlayer.Stop();
    }

    public void SkipVideo()
    {
        _videoPlayer.SetTime(_videoPlayer.GetTime() + 10.0f);
    }

    public void BackVideo()
    {
        _videoPlayer.SetTime(_videoPlayer.GetTime() - 5.0f);
    }
}