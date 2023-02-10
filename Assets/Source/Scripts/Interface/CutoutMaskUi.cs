using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;

public class CutoutMaskUi : Image {
    private static readonly int StencilComp = Shader.PropertyToID("_StencilComp");

    public override Material materialForRendering {
        get {
            Material forRendering = new Material(base.materialForRendering);
            forRendering.SetInt(StencilComp, (int)CompareFunction.NotEqual);
            return forRendering;
        }
    }

}