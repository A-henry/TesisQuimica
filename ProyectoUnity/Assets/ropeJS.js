//ropeJS by youtube.com/vulgerstal
//How to use:
//1) Drag and Drop RopeJS Prefab

#pragma strict

var parent : boolean;
var connectTo : Transform;

function Start () {

gameObject.AddComponent.<CharacterJoint>();
if(!parent) { GetComponent.<CharacterJoint>().connectedBody=transform.parent.GetComponent.<Rigidbody>();
transform.parent = null; }

}

function Update()
{
if(parent && connectTo != null)transform.position=connectTo.position;
}
