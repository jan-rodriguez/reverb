#pragma strict

@RPC
function PlayNetworkedSound( soundBite : float[] ){

	var audioClip : AudioClip = AudioClip.Create("testSound", soundBite.Length, 1, 44100, true, false);
	audioClip.SetData (soundBite, 0);

	AudioSource.PlayClipAtPoint (audioClip, this.transform.position);

//		UpdateOtherPlayerLight (soundBite);

}