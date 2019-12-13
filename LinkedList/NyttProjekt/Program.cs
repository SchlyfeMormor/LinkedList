using System;
using System.Collections;

class Program
{
    static void Main()
    {
        new Program().Run();
    }

    private void Run()
    {
        LinkedList<int> list = new LinkedList<int>();
        list.AddLast(0);
        list.AddLast(1);
        list.AddLast(2);
        list.AddLast(3);
        list.AddLast(5);

        Console.WriteLine();

        list.Print();

        list.Remove(2);

        Console.WriteLine();
        list.Print();
    }
}

public class LinkedList<T> : IEnumerable
{
    public IEnumerator GetEnumerator()
    {
        for (int i = 0; i < Count; i++)
            yield return this[i];
    }

    public int Count { get; set; }

    public T this[int index]
    {
        get
        {
            if (first.next == last || index >= Count)
                throw new IndexOutOfRangeException();
            Node<T> node = first.next;
            int counter = 0;
            while (counter < index)
            {
                node = node.next;
                counter++;
            }
            return node.data;
        }
    }

    public Node<T> first = new Node<T>();
    public Node<T> last = new Node<T>();

    public LinkedList()
    {
        first.next = last;
    }

    public void AddLast(T data)
    {
        Node<T> newNode = new Node<T>
        {
            data = data
        };

        Node<T> current;
        current = first;
        while (current.next != last)
        {
            current = current.next;
        }
        current.next = newNode;
        newNode.next = last;
        Count++;
    }

    public Node<T> AddFirst(T data)
    {
        Node<T> newNode = new Node<T>
        {
            data = data
        };
        newNode.next = first.next;
        first.next = newNode;
        Count++;
        return newNode;
    }

    public void Clear()
    {
        for (int i = Count - 1; i >= 0 ; i--)
        {
            Remove(this[i]);
        }
    }

    public bool Contains(T data)
    {
        for(int i = 0; i < Count; i++)
        {
            if (data.Equals(this[i]))
                return true;

        }
        return false;
    }

    public Node<T> Find(T data)
    {
        return Find(data, first.next);
    
    }

    public Node<T> Find(T data, Node<T> current)
    {
        if (current == last)
            return null;
        return current.data.Equals(data) ?
            current : Find(data, current.next);
    }

    public bool Remove(T data)
    {
        if (first == null)
            return false;

        if (first.Equals(data))
        {
            first = first.next;
            return true;
        }
        Node<T> current = first;
        while(current.next != last)
        {
            if (current.next.data.Equals(data))
            {
                current.next = current.next.next;
                Count--;
                return true;
            }
            current = current.next;

        }
        return false;
    }

    public void Print()
    {
        Node<T> current = first;
        while (current.next != last)
        {
            Console.WriteLine(current.next.data.ToString());
            current = current.next;
        }
    }
}

public class Node<T>
{
    public T data;
    public Node<T> next = null;

   
}