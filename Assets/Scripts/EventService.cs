using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventService : Service
{
    public event Action<GameObjectClickData> GameObjectClick = delegate { };
    public event Action<EntityClickData> EntityClick = delegate { };


    protected override void Awake()
    {
        base.Awake();

        EventController eventController = Controller.Create<EventController>("[EVENT CONTROLLER]");
        SettingEventController(eventController);

        DontDestroyOnLoad(eventController.gameObject);
    }


    private void SettingEventController(EventController events)
    {
        events.MouseButtonUp += (MouseData) =>
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(MouseData.MousePosition); // todo: camera change
            Physics.Raycast(ray, out hit);
            
            if (hit.collider != null)
            {
                GameObject gameObject = hit.collider.gameObject;
                Entity entity = gameObject.GetComponent<Entity>();

                if (entity != null)
                {
                    EntityClick.Invoke(new EntityClickData()
                    {
                        MouseButton = MouseData.MouseButton,
                        Target = entity
                    });
                }
                else
                {
                    GameObjectClick.Invoke(new GameObjectClickData()
                    {
                        MouseButton = MouseData.MouseButton,
                        Target = gameObject,
                        ClickPoint = hit.point
                    });
                }
            }
        };
    }
}


public struct GameObjectClickData
{
    public MouseButton MouseButton;
    public GameObject Target;
    public Vector3 ClickPoint;
}

public struct EntityClickData
{
    public MouseButton MouseButton;
    public Entity Target;
}