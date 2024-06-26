﻿using sqliteThreads.Model;

const int INSERT_COUNT = 100;
int insert_count = 0;
if (args.Length > 0){
    try{
      insert_count = Int32.Parse(args[0]);
    }
    catch{
        insert_count = INSERT_COUNT;
    }
}
else{
    insert_count = INSERT_COUNT;
}

Console.WriteLine($"#### Inserting {insert_count} records for each thread. ####");
Thread t = new Thread(() => WriteData("T1"));
Thread t2 = new Thread(() => WriteData("T2"));
Thread t3 = new Thread(()=>WriteData("T3"));
Thread t4 = new Thread(()=>WriteData("T4"));
Thread t5 = new Thread(()=>WriteData("T5"));
Thread t6 = new Thread(()=>WriteData("T6"));
Thread t7 = new Thread(()=>WriteData("T7"));
Thread t8 = new Thread(()=>WriteData("T8"));
Thread t9 = new Thread(()=>WriteData("T9"));
Thread t10 = new Thread(()=>WriteData("T10"));
Thread t11 = new Thread(()=>WriteData("T11"));
Thread t12 = new Thread(()=>WriteData("T12"));

t.Start();
t2.Start();
t3.Start();
t4.Start();
t5.Start();
t6.Start();

t7.Start();
t8.Start();
t9.Start();
t10.Start();
t11.Start();
t12.Start();

WriteData("Main");

void WriteData(string threadId){
    ThreadDataContext db = new ThreadDataContext();
    var beginTime = DateTime.Now;
    for (int i = 0; i < insert_count;i++){
        try{
            ThreadData td = new ThreadData{ThreadId=threadId, Created=DateTime.Now};
            db.Add(td);
            db.SaveChanges();
        }
        catch(Exception ex){
            Console.WriteLine($"Error: {threadId} => {ex.InnerException.Message}");
            continue;
        }
    }
    Console.WriteLine($"{threadId}: Completed - {DateTime.Now - beginTime}");
}