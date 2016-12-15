using UnityEngine;
using System.Collections;

public class faceMouseController : MonoBehaviour {

	public GameObject faceObject;
	private Vector3 mousePosition;

	private SkinnedMeshRenderer faceRend;
	private SkinnedMeshRenderer noFaceRend;
	private Mesh faceMesh;
	private Mesh noFaceMesh;
	private Material faceMaterial;

	// Use this for initialization
	void Start () {
		setupMeshes();
		setupMaterial();
	}

	// Update is called once per frame
	void Update () {
		mousePosition = getMousePosition();
		Vector3 newPosition = new Vector3(mousePosition.x, mousePosition.y, -10f);
		Vector3 relativePosition = getRelativePosition(newPosition);
		lookAtMouse(newPosition);
		blendExpression(relativePosition);
		blendTexture(relativePosition);
	}

	void setupMeshes(){
		GameObject face = faceObject.transform.Find("face").gameObject;
		GameObject noFace = faceObject.transform.Find("face_no_texture").gameObject;
		faceRend = face.GetComponent<SkinnedMeshRenderer>();
		noFaceRend = noFace.GetComponent<SkinnedMeshRenderer>();
	}

	void setupMaterial(){
		faceMaterial = faceRend.material;
	}

	void blendExpression(Vector3 pos){
		float blendY;
		if(pos.y > .5f){
			blendY = (pos.y - .5f) * 200f;
			blendLust(blendY);
		} else {
			blendY = (.5f - pos.y) * 200f;
			blendRage(blendY);
		}
	}

	void blendTexture(Vector3 pos){
		float alpha = pos.x;
		Color color = faceMaterial.color;
		faceMaterial.color = new Color(color.r, color.g, color.b, alpha);
	}

	void blendLust(float blend){
		faceRend.SetBlendShapeWeight(0, blend);
		noFaceRend.SetBlendShapeWeight(0, blend);
	}

	void blendRage(float blend){
		faceRend.SetBlendShapeWeight(1, blend);
		noFaceRend.SetBlendShapeWeight(1, blend);
	}

	Vector3 getRelativePosition(Vector3 mousePosition){
		float newX = Input.mousePosition.x / Screen.width;
		float newY = Input.mousePosition.y / Screen.height;
		Vector3 relativePosition = new Vector3(
			newX,
			newY,
			mousePosition.z
		);
		return relativePosition;
	}



	//mouse controls
	void lookAtMouse(Vector3 newPosition){
		faceObject.transform.LookAt(newPosition);
	}

    Vector3 getMousePosition(){
		Vector3 screenPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector3 origin = new Vector3(screenPosition.x, screenPosition.y, 0);
		return origin;
    }
}
