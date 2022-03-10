using System;

public static class Utility
{
    /*********************************************************************
    // Name: Gen_arrival
    // Description
    //    This function will generate a new arrival.  It has one parameter,
    // stream, which is the random number generator stream to be used.  It
    // performs the following steps:
    //    1 - gets a new customer.
    //    2 - generates an exponential arrival time.
    //    3 - inserts arrival event into the event list.
    //*********************************************************************/
    public static void Gen_arrival(Cust index, long time)
    {
        if (Globals.DEBUG)
        {
            Console.WriteLine(" Interarrival time for customer is {0}", time);
            Console.WriteLine(" Arrival time for customer is {0}", Globals.clock + time);
            Console.WriteLine(" Page number requested is {0}", thisPage);
        }
        // add the event to the list
        Globals.ev_list.insert_event(Globals.ARRIVAL, Globals.clock + time, index);
        return;
    }


    //*********************************************************************
    // Name: Gen_departure
    // Description
    //    This function generates a departure event from the server.  It
    // has two parameters: 1) stream - random number generator stream, and
    // 2) index - index of customer departing.  The following steps are
    // performed:
    //    1 - generate the service time.
    //    2 - insert the departure event into the event list.
    //*********************************************************************
    public static void Gen_departure(Cust index, long time)
    {
        // generate exponential service time
        if (Globals.DEBUG)
        {
            Console.WriteLine(" Service time for customer is {0}", time);
            Console.WriteLine(" Departure time for customer is {0}", Globals.clock + time);
        }
        // add departure event to the event list
        Globals.ev_list.insert_event(Globals.COMPLETE, time + Globals.clock, index);
        return;
    }



    //*********************************************************************
    // Name: Read_parms
    // Description
    //    This function inputs the required simulation parameters.
    //*********************************************************************
    public static void Read_parms()
    {
        Console.WriteLine("   SIMULATION -- CPU Scheduling Algorithm: FCFS");
        Console.WriteLine("      Input the following parameters:");
        Console.Write("      mean interarrival time => ");
        Globals.iarrive_time = Convert.ToDouble(Console.ReadLine());
        Console.Write("      mean service time => ");
        Globals.service_time = Convert.ToDouble(Console.ReadLine());
        Console.Write("      length of simulation => ");
        Globals.sim_length = Convert.ToInt64(Console.ReadLine());
        Console.Write("      seed for the random number generator => ");
        Globals.seed = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine(" Simulation time = {0} units", Globals.sim_length);
        Console.WriteLine(" Simulation begins...\n");
    }

    
    //*********************************************************************
    // Name: Process_statistics
    // Description
    //    This function computes and prints the mean response time for the
    // customers in an M/M/1 system.
    //*********************************************************************
    public static void Process_statistics()
    {
        double mean_resp_time, util;
        // compute mean response time
        mean_resp_time = Globals.accum_resp_time / (double)(100.0 * Globals.num_resp_time);
        // compute utiliation
        util = Globals.busytime / (double)Globals.clock;
        // print out results
        Console.WriteLine("\n...Simulation ends");
        Console.WriteLine(" Simulation results");
        Console.WriteLine(" mean response time ---------> {0:F4}", mean_resp_time);
        Console.WriteLine(" utilization ---------> {0:F4}", util);
        Console.ReadLine();  // hold the screen
    }




    //*********************************************************************
    // Name: Initialize
    // Description
    //    This function initializes the event list, queue, customer list,
    // and global variables.
    //*********************************************************************
    public static void Initialize()
    {
        // initialize the event list
        Globals.ev_list = new EVlist();
        Globals.fcfs = new Queue();
        // initialize the global variables
        Globals.clock = 0;
        Globals.busy = false;
        Globals.accum_resp_time = 0;
        Globals.num_resp_time = 0;
        Globals.busytime = 0;
        // Create and initialize the random number generator
        Globals.random_num = new Random(Globals.seed);
    }



    //*********************************************************************
    // Name: expon
    // Description
    //    This function is used to generate an exponential variate given
    // the mean time.
    //*********************************************************************
    public static int Expon(double time)
    {
        int val;
        double temp;
        time = time * 100;
        temp = 1.0 - (Globals.random_num.Next() / (double)(int.MaxValue - 1));
        val = (int) Math.Ceiling(-time * Math.Log(temp));
        return (val);
    }

}
