using System.Collections.Generic;

public class Stack<T>
{
    private List<T> items;

    public Stack()
    {
        items = new List<T>();
    }

    public bool IsEmpty()
    {
        return items.Count == 0;
    }

    public void Push(T item)
    {
        items.Add(item);
    }

    public T Pop()
    {
        if (!IsEmpty())
        {
            T item = items[items.Count - 1];
            items.RemoveAt(items.Count - 1);
            return item;
        }
        throw new System.InvalidOperationException("Pop from empty stack");
    }

    public int Size()
    {
        return items.Count;
    }
}
