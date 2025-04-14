using System;
using System.Collections;
using UnityEngine;
using TMPro;

public class PneumaticController : MonoBehaviour
{
	[SerializeField] private Transform[] pneumatics;
	[SerializeField] private Transform[] disposals;
	[SerializeField] private LayerMask boxLayer;
	[SerializeField] private TextMeshProUGUI points;
	private float pushSpeed;
	private Transform objectToMove, disposalTarget;
	public int Points { get; private set; }

	private void Awake()
	{
		points.text = "0";
	}

	private void Update()
	{
		if (objectToMove != null)
		{
			pushSpeed = 10 * Time.deltaTime;
			objectToMove.position = Vector3.Lerp(objectToMove.position, disposalTarget.position, pushSpeed);
		}

		if (Input.GetKeyDown(KeyCode.Alpha1))
			PneumaticFunction(1);
		if (Input.GetKeyDown(KeyCode.Alpha2))
			PneumaticFunction(2);
		if (Input.GetKeyDown(KeyCode.Alpha3))
			PneumaticFunction(3);
		if (Input.GetKeyDown(KeyCode.Alpha4))
			PneumaticFunction(4);
	}

	private void PneumaticFunction(int i)
	{
		Collider2D box = Physics2D.OverlapCircle(pneumatics[i-1].position + Vector3.up*1.25f, 0.5f, boxLayer);

		if (box == null)
			return;

		BoxBehaviour bb = box.GetComponent<BoxBehaviour>();
		StartCoroutine(PushingObject(bb, i));
	}

	private IEnumerator PushingObject(BoxBehaviour bb, int i)
	{
		ConveyorBeltItem item = ConveyorBelt.instances.GetItem(bb.index);
		item.canMove = false;
		objectToMove = item.item;
		disposalTarget = disposals[i-1];

		yield return new WaitForSeconds(0.25f);

		if (bb.colorIndex != i) 
			LivesSystem.instances.DecreaseLives(); 	
		else 
		{
			points.text = (Convert.ToInt32(points.text) + bb.points).ToString();
			Points = Convert.ToInt32(points.text);
		}

		ConveyorBelt.instances.DestroyItem(bb.index);
		objectToMove = null;
		disposalTarget = null;
	}
}
