using UnityEngine;
using VRStandardAssets.Utils;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using VRStandardAssets.Utils;

namespace VRStandardAssets.Examples
{
	// This script is a simple example of how an interactive item can
	// be used to change things on gameobjects by handling events.
	public class VRButtons : MonoBehaviour
	{
		Message eventMessage;
		[SerializeField] private Material m_NormalMaterial;                
		[SerializeField] private Material m_OverMaterial;                  
		[SerializeField] private Material m_ClickedMaterial;               
		[SerializeField] private Material m_DoubleClickedMaterial;         
		[SerializeField] private VRInteractiveItem m_InteractiveItem;
		[SerializeField] private Renderer m_Renderer;

		private Vector3 oldLocation;

		private void Awake ()
		{
			m_Renderer.material = m_NormalMaterial;
		}

		private void OnEnable()
		{
			m_InteractiveItem.OnOver += HandleOver;
			m_InteractiveItem.OnOut += HandleOut;
			m_InteractiveItem.OnClick += HandleClick;
			m_InteractiveItem.OnDoubleClick += HandleDoubleClick;
		}

		private void OnDisable()
		{
			m_InteractiveItem.OnOver -= HandleOver;
			m_InteractiveItem.OnOut -= HandleOut;
			m_InteractiveItem.OnClick -= HandleClick;
			m_InteractiveItem.OnDoubleClick -= HandleDoubleClick;
		}

		//Handle the Over event
		private void HandleOver()
		{
			Debug.Log("Show over state");
			m_Renderer.material = m_OverMaterial;
			Transform firstChild = transform.transform;

			foreach (Transform child in transform) {
				Vector3 v = child.position;
				v.y = v.y + 0.1f;
				child.position = v;
			}
		}

		//Handle the Out event
		private void HandleOut()
		{
			Debug.Log("Show out state");
			m_Renderer.material = m_NormalMaterial;
			foreach (Transform child in transform) {
				Vector3 v = child.position;
				v.y = v.y - 0.1f;
				child.position = v;
			}
		}

		//Handle the Click event
		private void HandleClick()
		{
			Debug.Log("Show click state");
			m_Renderer.material = m_ClickedMaterial;
			StartCoroutine (ActivateButton());
		}

		private IEnumerator ActivateButton()
		{
			eventMessage = new Message ();
			eventMessage.Type = MessageType.ClickVRButton;
			eventMessage.GameObjectValue = m_InteractiveItem.gameObject;
			MessageBus.Instance.SendMessage (eventMessage);
			yield break;
		}

		//Handle the DoubleClick event
		private void HandleDoubleClick()
		{
			Debug.Log("Show double click");
			m_Renderer.material = m_DoubleClickedMaterial;
		}
	}
}