using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameLayer
{
    public const string LAYER_PLAYER = "Player";
    public const string LAYER_GROUND = "Ground";

    public static readonly int PlayerLayerIndex = LayerMask.NameToLayer(LAYER_PLAYER);
    public static readonly int GroundLayerIndex = LayerMask.NameToLayer(LAYER_GROUND);

    public static readonly LayerMask PlayerLayerMask = 1 << PlayerLayerIndex;
    public static readonly LayerMask GroundLayerMask = 1 << GroundLayerIndex;
}
