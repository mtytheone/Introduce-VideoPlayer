#region License
/*------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/
/* MIT License                                                                                                                                                                                                                                                                                                                                                                                                                                                                  */
/*                                                                                                                                                                                                                                                                                                                                                                                                                                                                              */
/* Copyright (c) 2000 hatuxes                                                                                                                                                                                                                                                                                                                                                                                                                                                   */
/*                                                                                                                                                                                                                                                                                                                                                                                                                                                                              */
/* Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:                             */
/*                                                                                                                                                                                                                                                                                                                                                                                                                                                                              */
/* The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.                                                                                                                                                                                                                                                                                                                                               */
/*                                                                                                                                                                                                                                                                                                                                                                                                                                                                              */
/* THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE. */
/*------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/
#endregion

using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDKBase;
using VRC.Udon;

public class LocalVideoController_PlayList : UdonSharpBehaviour
{
    [SerializeField]
    private VRC.SDK3.Video.Components.Base.BaseVRCVideoPlayer _videoPlayer;

    [SerializeField]
    private VRCUrl[] _playlistURL;

    [SerializeField]
    private Text _playTimeDisplayText;

    [SerializeField]
    private Text _indexDisplayText;

    [SerializeField]
    private Toggle _loopToggle;

    private float _videoDuration;
    private int _urlIndex;
    private bool _isPausing;

    private void Update()
    {
        _indexDisplayText.text = string.Format("Index : {0}", _urlIndex);

        if (_videoPlayer.IsPlaying)
        {
            _playTimeDisplayText.text = string.Format("{0:f} / {1:f}", _videoPlayer.GetTime(), _videoDuration);
        }
    }

    public override void OnVideoStart()
    {
        _videoDuration = _videoPlayer.GetDuration();
    }

    public override void OnVideoEnd()
    {
        if (_loopToggle.isOn)
        {
            _urlIndex = (_urlIndex + 1) >= _playlistURL.Length ? 0 : _urlIndex + 1;
        }
        else
        {
            if ((_urlIndex + 1) >= _playlistURL.Length)
            {
                return;
            }

            _urlIndex++;
        }

        PlayVideo();
    }

    public void PlayVideo()
    {
        _videoPlayer.Stop();
        _videoPlayer.PlayURL(_playlistURL[_urlIndex]);
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

    public void NextVideo()
    {
        _urlIndex = (_urlIndex + 1) >= _playlistURL.Length ? 0 : _urlIndex + 1;

        PlayVideo();
    }

    public void PreviousVideo()
    {
        _urlIndex = (_urlIndex - 1) < 0 ? _playlistURL.Length - 1 : _urlIndex - 1;

        PlayVideo();
    }
}