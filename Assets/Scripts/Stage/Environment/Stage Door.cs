using System;
using System.Collections;
using UnityEngine;

public class StageDoor : MonoBehaviour
{
    public DoorDirection Direction;
    public int tiles;

    private Vector2 closePosition;

    private Vector2 openPosition;
    private IEnumerator routine;
    public Coroutine coroutine;

    public bool setActiveInNextFrame = true;

    void Start()
    {
        
    }

    public void Init()
    {
        setActiveInNextFrame = true;
        closePosition = transform.position;

        int dir = 1;
        Vector2 offset = Vector2.zero;
        switch (Direction)
        {
            case DoorDirection.Left:
                offset = Vector2.left * tiles * dir;
                break;
            case DoorDirection.Right:
                offset = Vector2.right * tiles * dir;
                break;
            case DoorDirection.Up:
                offset = Vector2.up * tiles * dir;
                break;
            case DoorDirection.Down:
                offset = Vector2.down * tiles * dir;
                break;

        }

        openPosition = (Vector2)transform.position + offset;
    }

    private void Update()
    {
        if(!setActiveInNextFrame)
        {
            setActiveInNextFrame = true;
            gameObject.SetActive(false);
        }
    }

    public void CloseDoor(Action OnDoorFinish)
    {
        BeginCoroutineDoor(true, OnDoorFinish);
    }


    public void OpenDoor(Action OnDoorFinish)
    {
        BeginCoroutineDoor(false, OnDoorFinish);
    }

    public void BeginCoroutineDoor(bool close, Action OnDoorFinish)
    {
        if (coroutine != null)
        {
            CoroutineManager.Instance.StopTrackedCoroutine(routine, ref coroutine);
        }
        routine = BeginDoor(close, OnDoorFinish);
        coroutine = CoroutineManager.Instance.StartNewCoroutine(routine);
    }

    private IEnumerator BeginDoor(bool close, Action OnDoorFinish)
    {


        Vector2 newPos = close ? closePosition : openPosition;

        if (close) yield return new WaitForSeconds(2);

        while(Vector2.Distance(transform.position, newPos) > 0.1f)
        {
            transform.position = Vector2.MoveTowards(transform.position, newPos, 0.1f);
            yield return null;

        }

        OnDoorFinish?.Invoke();
    }
}

public enum DoorDirection
{
    Left, Right, Up, Down
}
