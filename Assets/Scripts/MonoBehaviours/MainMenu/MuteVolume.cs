using UnityEngine;
using UnityEngine.UI;

public class MuteVolume : MonoBehaviour
{
	public Text VolumeActiveLabel;
	public Text VolumeMutedLabel;
	private AudioSource _audioToMute;

	private void Start()
	{
		_audioToMute = FindObjectOfType<AudioSource>();
		VolumeActive = true;
	}

	public void SwitchState()
	{
		VolumeActive = !VolumeActive;
	}

	private bool VolumeActive
	{
		get { return _audioToMute.mute; }
		set
		{
			_audioToMute.mute = value;
			if (_audioToMute.mute)
			{
				VolumeActiveLabel.enabled = true;
				VolumeMutedLabel.enabled = false;
				_audioToMute.mute = false;
			}
			else
			{
				VolumeActiveLabel.enabled = false;
				VolumeMutedLabel.enabled = true;
				_audioToMute.mute = true;
			}
		}
	}
}
