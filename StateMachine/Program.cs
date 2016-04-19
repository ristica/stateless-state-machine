using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using StateMachine.Subject;

namespace StateMachine
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("");

            var phone = new Phone();

            phone.GoOnline();
            Thread.Sleep(1000);

            phone.Dial();
            Thread.Sleep(1000);

            phone.Refuse();
            Console.WriteLine("\t\twaiting for some time...");
            Thread.Sleep(5000);

            phone.Ring();
            Thread.Sleep(500);

            phone.AcceptCall();
            Thread.Sleep(3000);

            phone.Interrupt();
            Thread.Sleep(2000);

            phone.Ring();
            Thread.Sleep(500);

            phone.AcceptCall();
            Thread.Sleep(5000);

            phone.End();
            Thread.Sleep(2000);

            phone.GoOffline();

            Console.WriteLine();
            Console.ReadKey();
        }
    }
}
