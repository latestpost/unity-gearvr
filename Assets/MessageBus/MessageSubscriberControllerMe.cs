using UnityEngine;
using System.Collections;

public class MessageSubscriberControllerMe : MonoBehaviour 
{
	public MessageType[] MessageTypes;
	private MessageHandler Handler;

	void Start()
	{
		Handler = new CollisionEvents(gameObject);
		MessageSubscriber subscriber = new MessageSubscriber ();
		subscriber.MessageTypes = MessageTypes;
		subscriber.Handler = Handler;

		MessageBus.Instance.AddSubscriber (subscriber);
	}
}
