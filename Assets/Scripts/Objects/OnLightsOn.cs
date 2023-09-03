using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnLightsOn : MonoBehaviour
{
    private void OnEnable()
    {
        GameManager.Shared().AddRangeToLocation(GameManager.Locations.SecondTentZoneDown, 
            GameManager.Shared().GetRangeOfLocation(GameManager.Locations.LastZoneDown));
        GameManager.Shared().AddRangeToLocation(GameManager.Locations.PreLastZoneDown, 
            GameManager.Shared().GetRangeOfLocation(GameManager.Locations.LastZoneDown));
    }
}
