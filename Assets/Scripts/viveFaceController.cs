using UnityEngine;
using System.Collections;

public class viveFaceController : MonoBehaviour {

	public GameObject faceObject;
	public GameObject leftEyeObject;
	public GameObject rightEyeObject;
	public GameObject viveHeadset;
	public Collider groundPlane;
	private Vector3 vivePosition;

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
		vivePosition = viveHeadset.transform.position;
		// Vector3 newPosition = new Vector3(viveHeadset.x, viveHeadset.y, -10f);
		Vector3 relativePosition = getRelativePosition(vivePosition, groundPlane);
		lookAtVive(vivePosition);
		blendExpression(1f - relativePosition.x);
		blendTexture(relativePosition.z);
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

	void blendExpression(float blend){
		float lustRage;
		if(blend > .5f){
			lustRage = (blend - .5f) * 200f;
			blendLust(lustRage);
		} else {
			lustRage = (.5f - blend) * 200f;
			blendRage(lustRage);
		}
	}

	void blendTexture(float alpha){
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

	Vector3 getRelativePosition(Vector3 vivePosition, Collider groundPlane){
		Bounds b = groundPlane.bounds;
		float xSize = b.max.x - b.min.x;
		float zSize = b.max.z - b.min.z;
		float relativeX = Mathf.Abs((b.max.x - vivePosition.x)/b.size.x);
		float relativeZ = Mathf.Abs((b.max.z - vivePosition.z)/zSize);
		Vector3 relativePosition = new Vector3(
			relativeX,
			0f,
			relativeZ
		);
		return relativePosition;
	}

	//mouse controls
	void lookAtVive(Vector3 newPosition){
		faceObject.transform.LookAt(newPosition);
		leftEyeObject.transform.LookAt(newPosition);
		rightEyeObject.transform.LookAt(newPosition);
	}

    Vector3 getMousePosition(){
		Vector3 screenPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector3 origin = new Vector3(screenPosition.x, screenPosition.y, 0);
		return origin;
    }
}
