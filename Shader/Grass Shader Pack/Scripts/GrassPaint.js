#pragma strict

@Range(0,1)
public var height : float = 1;
public var heightMapSize : Vector2 = Vector2 (64, 64);
public var filterMode : FilterMode = FilterMode.Trilinear;

public var useReticle : boolean;
public var reticle : Texture;

private var cam: Camera;
private var tex : Texture2D;

function Start () {
    cam = GetComponent.<Camera>();
    tex = new Texture2D (heightMapSize.x, heightMapSize.y);
    tex.filterMode = filterMode;
    for (var x : int; x < tex.width; x++) {
        for (var y : int; y < tex.height; y++) {
            tex.SetPixel (x, y, Color.clear);
        }
    }
    tex.Apply();
}


function Update () {
    // Only when we press the mouse
    if (!Input.GetMouseButton(0))
        return;

    // Only if we hit something, do we continue
    var hit : RaycastHit;
    if (!Physics.Raycast (cam.ScreenPointToRay(Input.mousePosition), hit))
        return;

    // Just in case, also make sure the collider also has a renderer
    // material and texture. Also we should ignore primitive colliders.
    var rend : Renderer = hit.transform.GetComponent.<Renderer>();
   	var meshCollider = hit.collider as MeshCollider;
   	
    if (rend == null || rend.sharedMaterial == null || !rend.sharedMaterial.GetTexture ("_Height") || meshCollider == null)
        return;

    rend.sharedMaterial.SetTextureScale ("_Height", Vector2 (1,1));
 
    // Now draw a pixel where we hit the object
    var pixelUV : Vector2 = hit.textureCoord;
    pixelUV.x *= tex.width;
    pixelUV.y *= tex.height;
    tex.SetPixel (pixelUV.x, pixelUV.y, Color (1,1,1,height));
    tex.Apply();

    rend.material.SetTexture ("_Height", tex);
}

function OnGUI () {
    if (useReticle)
        GUI.DrawTexture (Rect (Screen.width / 2 - 2, Screen.height / 2 - 2, 5, 5), reticle);
}