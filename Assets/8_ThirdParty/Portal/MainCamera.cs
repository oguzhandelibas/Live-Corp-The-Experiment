using UnityEngine;
using UnityEngine.Rendering;

public class MainCamera : MonoBehaviour {

    Portal[] portals;

    void Awake () {
        portals = FindObjectsOfType<Portal> ();
        //RenderPipelineManager.beginCameraRendering += RenderPortal;
    }
    /*
    private void RenderPortal(ScriptableRenderContext context, Camera camera)
    {
        for (int i = 0; i < portals.Length; i++)
        {
            portals[i].PrePortalRender();
        }
        for (int i = 0; i < portals.Length; i++)
        {
            portals[i].Render();
        }
        for (int i = 0; i < portals.Length; i++)
        {
            portals[i].PostPortalRender();
        }
    }
    
    private void OnDestroy()
    {
        RenderPipelineManager.beginCameraRendering -= RenderPortal;
    }*/
    

}