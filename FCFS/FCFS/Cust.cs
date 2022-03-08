using System;

// Class to hold the customer data
public class Cust
{
    private long arrive_time;
    private int cust_num;
    private static int num;

    // Constructors
    public Cust()
    {
        arrive_time = 0;
        cust_num = num;
        num += 1;
    }

    public Cust(long time)
    {
        arrive_time = time;
        cust_num = num;
        num += 1;
    }

    // Return the customer number
    public int getnum()
    {
        return cust_num;
    }

    // Return the arrival time
    public long getarrive()
    {
        return arrive_time;
    }

    // set the arrival time
    public void setarrive(long time)
    {
        arrive_time = time;
    }
}
