using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissingItemPickup : ItemPickup
{
    [SerializeField] private MeshRenderer _quadFront, _quadBack;

    private static Dictionary<int, Material> p_materialsCache = new Dictionary<int, Material>();
    private static Shader p_unlitTextureShader;

    public override void Setup(BasicItem item)
    {
        base.Setup(item);

        if(p_materialsCache.ContainsKey(item.ItemID))
        {
            _quadFront.material = p_materialsCache[item.ItemID];
            _quadBack.material = p_materialsCache[item.ItemID];
            return;
        }
        if(p_unlitTextureShader == null)
        {
            p_unlitTextureShader = Shader.Find("Unlit/Transparent");
        }
        Material material = new Material(p_unlitTextureShader)
        {
            mainTexture = item.Icon.texture
        };
        _quadFront.material = material;
        _quadBack.material = material;
        p_materialsCache.Add(item.ItemID, material);
    }
}
