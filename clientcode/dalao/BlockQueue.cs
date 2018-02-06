using System;
using System.Collections.Generic;
using System.Threading;

public class BlockQueue<T>
{
    public readonly int SizeLimit = 0;
    private Queue<T> _inner_queue = null;
    public int Count
    {
        get { return _inner_queue.Count; }
    }
    private ManualResetEvent _enqueue_wait = null;
    private ManualResetEvent _dequeue_wait = null;
    public BlockQueue(int sizeLimit)
    {
        this.SizeLimit = sizeLimit;
        this._inner_queue = new Queue<T>(this.SizeLimit);
        this._enqueue_wait = new ManualResetEvent(false);
        this._dequeue_wait = new ManualResetEvent(false);
    }
    public void EnQueue(T item)
    {
        if (this._IsShutdown == true) throw new InvalidCastException("Queue was shutdown. Enqueue was not allowed.");
        while (true)
        {
            lock (this._inner_queue)
            {
                if (this._inner_queue.Count < this.SizeLimit)
                {
                    this._inner_queue.Enqueue(item);
                    this._enqueue_wait.Reset();
                    this._dequeue_wait.Set();
                    break;
                }
            }
            this._enqueue_wait.WaitOne();
        }
    }
    public T DeQueue()
    {
        while (true)
        {
            if (this._IsShutdown == true)
            {
                lock (this._inner_queue) return this._inner_queue.Dequeue();
            }
            lock (this._inner_queue)
            {
                if (this._inner_queue.Count > 0)
                {
                    T item = this._inner_queue.Dequeue();
                    this._dequeue_wait.Reset();
                    this._enqueue_wait.Set();
                    return item;
                }
            }
            this._dequeue_wait.WaitOne();
        }
    }
    private bool _IsShutdown = false;
    public void Shutdown()
    {
        this._IsShutdown = true;
        this._dequeue_wait.Set();
    }
}