using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class ProfileTracker : MonoBehaviour
{
    public static ProfileTracker S;

    public HashSet<ProfilePictureData> RemainingProfiles { get; set; }

    private void Awake()
    {
        S = this;
        DontDestroyOnLoad(this.gameObject);

        RemainingProfiles = new HashSet<ProfilePictureData>();
    }
}
