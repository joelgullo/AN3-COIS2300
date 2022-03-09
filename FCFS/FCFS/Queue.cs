using System;

// elements which make up the queue (hold customer data)
public class Qnode
{
    private	Cust custptr;
	private Qnode next;

    public Qnode()
	{
	    next = null;
	    custptr = null;
	}

    public Qnode Next
    {
        get {
            return next;
            }
        set {
            next = value;
            }
    }

    public Cust CustPtr
    {
        get
        {
            return custptr;
        }
        set
        {
            custptr = value;
        }
    }
        
    public Qnode(Cust cp, Qnode np)
	{
	    custptr = cp;
	    next = np;
	}

    // return customer pointer of node
    public Cust getcust() 
    { 
        return custptr; 
    }
}

// Class to manipulate the elements on the queue
public class Queue
{
    private Qnode head;
	private Qnode tail;

    //Constructor
    public Queue()
	{
	    head = null;
	    tail = null;
	}

    // returns true is queue is empty and false otherwise
    public bool isempty() 
    {
        return (head == null);
    }

    // add an element of type Cust to queue
    public void add_to_queue(Cust ptr)
    {
        Qnode newnode;
        // get an new node
        newnode = new Qnode(ptr, null);
        // check to see if the queue is initially empty
        if(tail == null)
	    {
	        head = newnode;
	        tail = newnode;
	        return;
	    }
        // otherwise add it to the end of the queue and relink
        tail.Next = newnode;
        tail = newnode;
        return;
    }

    //return the top element from the queue
    public Cust take_off_queue()
    {
        Qnode ptr;
        Cust val;
        // check if the queue is empty
        if(head == null)
	    {
	        Console.WriteLine(" ***Error - queue underflow***\n");
	        return null;
	    }
        // remove top element from queue
        ptr = head;
        // get customer index
        val = ptr.getcust();
        // check if queue now empty and relink
        if(head == tail)
	    {
	        tail = null;
	        head = null;
	        return val;
	    }
        // otherwise just relink
        head = ptr.Next;
        return val;
    }

    // print out the elements in the queue
    public void print()
	{
	    Qnode ptr;
	    ptr = head;
	    Console.WriteLine("\nQueue contents ... ");
	    while (ptr != null) 
        {
		    Console.Write("\nCust {0}", ptr.CustPtr.getnum());
		    Console.WriteLine(" => {0}", ptr.CustPtr.getarrive());
		    ptr = ptr.Next;    
        }
	}
	
	 public Cust[] foreachloop(int page)
    {
        Qnode ptr;
        Cust curCust;
        int curPage;
        int j = 0;
        Cust[] samePage = new Cust[30];

        ptr = head;
        while (ptr != null)
        {
            curCust = ptr.getcust();
            curPage = curCust.getPage();

            if (curPage == page)
            {
                samePage[j] = curCust;
                j++;

                ptr = head;
                take_off_queue();
            }

            ptr = ptr.Next;
        }
	return samePage;
    }
}
