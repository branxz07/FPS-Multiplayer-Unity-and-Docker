using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class SendAnimationEventToSFXManager : MonoBehaviour
{

    public PLayerPhotonSound playerPhotonSoundManager;
    // Start is called before the first frame update
    public void TriggerFootstepSFX(){
        playerPhotonSoundManager.PlayFootstaps();
    }
}
