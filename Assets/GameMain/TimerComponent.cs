using System;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Collections.Generic;
using UnityGameFramework.Runtime;

[DisallowMultipleComponent]
[AddComponentMenu("Game Framework/Timer")]
public class TimerComponent : GameFrameworkComponent
{

    private PriorityQueue<TimerData> timerQueueFrame = new PriorityQueue<TimerData>();
    private PriorityQueue<TimerData> timerQueuetime = new PriorityQueue<TimerData>();

    private void Awake()
    {
        base.Awake();
    }

    public void TimerUpdate(PriorityQueue<TimerData> timerQueue)
    {
        while (timerQueue.Count > 0 && timerQueue.Peek().elapsedTime >= timerQueue.Peek().duration)
        {
            TimerData timer = timerQueue.Dequeue();
            if (!timer.isCancelled)
            {
                timer.callback();
                if (timer.repeatCount == -1 || timer.repeatCount > 0)
                {
                    timer.elapsedTime = 0f;
                    if (timer.repeatCount > 0)
                    {
                        timer.repeatCount--;
                    }

                    timerQueue.Enqueue(timer);
                }
            }
        }

        foreach (TimerData timer in timerQueue)
        {
            timer.elapsedTime += timer.isFrameBased ? 1 : Time.deltaTime;
        }
    }
    
    private void Update()
    {
        TimerUpdate(timerQueueFrame);
        TimerUpdate(timerQueuetime);
    }

    public  void AddTimer(float duration, int repeatCount, int priority, Action callback,
        bool isFrameBased = false)
    {

        TimerData timer = new TimerData(duration, repeatCount, priority, callback, isFrameBased);
        if (isFrameBased)
        {
            timerQueueFrame.Enqueue(timer);
        }
        else
        {
            timerQueuetime.Enqueue(timer);
        }
    }

    public  void CancelTimer(Action callback)
    {


        foreach (TimerData timer in timerQueuetime)
        {
            if (timer.callback == callback)
            {
                timer.isCancelled = true;
            }
        }
        foreach (TimerData timer in timerQueueFrame)
        {
            if (timer.callback == callback)
            {
                timer.isCancelled = true;
            }
        }
    }

    public  void CancelAllTimers()
    {
        timerQueuetime.Clear();
        timerQueueFrame.Clear();
    }
}

public class TimerData : IComparable<TimerData>
{
    public float duration;
    public int repeatCount;
    public int priority;
    public Action callback;
    public bool isCancelled;
    public float elapsedTime;
    public bool isFrameBased;

    public TimerData(float duration, int repeatCount, int priority, Action callback, bool isFrameBased = false)
    {
        this.duration = duration;
        this.repeatCount = repeatCount;
        this.priority = priority;
        this.callback = callback;
        this.isCancelled = false;
        this.elapsedTime = 0f;
        this.isFrameBased = isFrameBased;
    }

    public int CompareTo(TimerData other)
    {
        //先按照剩余时间排序
        int result = (duration - elapsedTime).CompareTo(other.duration - other.elapsedTime);
        if (result == 0)
        {
            //再按照优先级排序
            result = priority.CompareTo(other.priority);
        }

        return result;
    }
}