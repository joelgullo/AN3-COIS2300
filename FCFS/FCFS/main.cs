using System;

public class MainProgram
{
    //**********************************************************************
    // Name: Main
    // Description
    //    This function performs the main control loop of the simulation.
    // It performs the following steps:
    //    1 - call routines to initialize global variables.
    //    2 - schedules an end of simulation.
    //    3 - generates the first arrival.
    //    4 - processes the events on the event list until the end of
    //        simulation event is reached.
    //    5 - frees event node after it has been processed.
    //    6 - prints out the statistics when the simulation is finished.
    //*********************************************************************
    public static void Main()
    {
        bool not_done;
        Cust new_index;
        int arrive_time;
        EVnode eventid;
        // initialization
        Utility.Read_parms();
        Utility.Initialize();
        // schedule an end of simulation
        Globals.ev_list.insert_event(Globals.EOS, Globals.sim_length, null);
        // generate first arrival
        // get new customer
        new_index = new Cust();
        // generate exponential interarrival time of new customer
        arrive_time = Utility.Expon(Globals.iarrive_time);
        // add the next arrival to the event list for future 
        Utility.Gen_arrival(new_index, arrive_time);
        // main loop to process the event list
        not_done = true;
        while (not_done)
        {
            // get next event
            eventid = Globals.ev_list.remove_event();
            // update clock
            Globals.clock = eventid.get_evtime();
            // process event type
            switch (eventid.get_evtype())
            {
                case Globals.ARRIVAL:
                    Arrive(eventid);
                    break;
                case Globals.COMPLETE:
                    Depart(eventid);
                    break;
                case Globals.EOS:
                    Utility.Process_statistics();
                    not_done = false;
                    break;
                default:
                    Console.WriteLine("***Error - invalid event type\n");
                    break;
            }
        }
    }
    public static int page = 0;
    static int[] pageArray = new int[101];                 // keep track of total # of page requests

    //*********************************************************************
    // Name: arrive                                                        
    // Description                                                         
    //    This function processes an arrival to the system.  It performs   
    // the followiong steps:                                               
    //    1 - generates the next arrival.                                  
    //    2 - sets the system statistics for current job                                  
    //    3 - puts the customer into the queue.                   
    //    4 - if the server is not busy then calls start_service.          
    //*********************************************************************
    public static void Arrive(EVnode ev_num)
    {
        Cust cur_index, new_index;
        long arrive_time;
        // get new customer
        new_index = new Cust();
        // generate exponential interarrival time of new customer
        arrive_time = Utility.Expon(Globals.iarrive_time);
        // add the next arrival to the event list for future 
        Utility.Gen_arrival(new_index, arrive_time);
        // set statistics gathering variable
        cur_index = ev_num.get_cust();
        cur_index.setarrive(Globals.clock);

        page = cur_index.getPage();
        pageArray[page]++;

        // put the customer in the queue
        Globals.fcfs.add_to_queue(cur_index);
        // if server is not busy then start service
        if (!Globals.busy)
            StartService();
        return;
    }



    //*********************************************************************
    // Name: start_service
    // Description
    //    This function performs the following steps:
    //    1 - removes the first customer from the queue.
    //    2 - sets the server to busy.
    //    3 - schedules a departure event.
    //*********************************************************************
    public static void StartService()
    {
        long servicetime;
        ///Cust index;
        int p = 0;
        //int largestTotal = 0;
        //int curPage;
        Cust curIndex;
        Cust longestCust;
        Cust[] samePage = new Cust[100];

        longestCust = Globals.fcfs.firstForEach();
        p = longestCust.getPage();

        //initiallizing object for non-static member
        //Queue q = new Queue();
        //call method that loops through queue to find all customers requesting the most requested page
        samePage = Globals.fcfs.secondForEachLoop(p);


        servicetime = Utility.Expon(Globals.service_time);
        //For each cust that has the same page, generate a departure event at the current time
        for (int x = 0; x < samePage.Length; x++)
        {
            curIndex = samePage[x];
            if (curIndex != null)
            {
                Utility.Gen_departure(curIndex, Globals.clock + servicetime);
            }
        }

        // remove the first customer from the queue
        // ***index = Globals.fcfs.take_off_queue();
        // set server to busy
        Globals.busy = true;
        // generate a CPU bust time for the next arrival and associate it with customer
        // ***servicetime = Utility.Expon(Globals.service_time);
        // schedule a departure event based on CPU burst time
        // ***Utility.Gen_departure(index, servicetime);
        //accumulate busy time to compute utilization
        Globals.busytime += servicetime;
        return;
    }



    //********************************************************************* 
    // Name: depart
    // Description
    //    This function processes a departure from the server event.  It
    // performs the following steps:
    //    1 - sets the server to idle.
    //    2 - accumulate response time statistics.
    //    3 - remove the customer from the system.
    //    4 - if the queue is not empty, then start service.
    //*********************************************************************
    public static void Depart(EVnode ev_num)
    {
        Cust dep_index;
        long temp;
        // set server to idle
        Globals.busy = false;
        // accumulate response time
        dep_index = ev_num.get_cust();

        page = dep_index.getPage();
        pageArray[page] = 0;

        temp = Globals.clock - dep_index.getarrive();
        //if (Globals.DEBUG)
            //Console.WriteLine(" Response time for customer {0} is {1}", index.getnum(), temp);
        Globals.accum_resp_time += temp;
        Globals.num_resp_time++;
        // if queue is non-empty, start service
        if (!Globals.fcfs.isempty())
            StartService();
        return;
    }
}
