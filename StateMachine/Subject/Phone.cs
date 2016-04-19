using System;
using System.Diagnostics;
using System.Runtime.Serialization.Formatters;
using Stateless;

namespace StateMachine.Subject
{
    public class Phone
    {
        private readonly StateMachine<State, Trigger> _phone =
            new StateMachine<State, Trigger>(State.Offline);

        public Phone()
        {
            _phone.Configure(State.Offline) // turned off
                    .Permit(Trigger.GoneOnline, State.Online)
                    .Permit(Trigger.ThrowPhoneAway, State.TelefoneDestroyed);

            _phone.Configure(State.Online) // turned on
                    .Permit(Trigger.GoneOffline, State.Offline)
                    .Permit(Trigger.Ringing, State.Ringing)
                    .Permit(Trigger.CallDialed, State.Ringing)
                    .Permit(Trigger.ThrowPhoneAway, State.TelefoneDestroyed);

            _phone.Configure(State.Ringing) // turned on
                    .Permit(Trigger.GoneOffline, State.Offline)
                    .Permit(Trigger.CallRefused, State.Online)
                    .Permit(Trigger.CallConnected, State.Talking)
                    .Permit(Trigger.ThrowPhoneAway, State.TelefoneDestroyed);

            _phone.Configure(State.Talking)
                    .OnEntry(StartTalking)
                    .OnExit(StopTalking)
                    .Permit(Trigger.GoneOffline, State.Offline)
                    .Permit(Trigger.CallInterupted, State.Online)
                    .Permit(Trigger.CallEnded, State.Online)
                    .Permit(Trigger.ThrowPhoneAway, State.TelefoneDestroyed);
        }

        private void StartTalking()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\t\t\tConnection established...");
        }

        private void StopTalking()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\t\t\tDisconnected...");
        }

        public void GoOnline()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Turning the mobile phone on!");
            _phone.Fire(Trigger.GoneOnline);
        }

        public void GoOffline()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Turning the mobile phone off!");
            _phone.Fire(Trigger.GoneOffline);
        }

        public void Dial()
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("\tDial a number");
            _phone.Fire(Trigger.CallDialed);
        }

        public void Ring()
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("\tIt's ringing");
            _phone.Fire(Trigger.Ringing);
        }

        public void Interrupt()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\t\tCall interrupted...");
            _phone.Fire(Trigger.CallInterupted);
        }

        public void Refuse()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\t\tRefused!");
            _phone.Fire(Trigger.CallRefused);
        }

        public void End()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("\tCall ended");
            _phone.Fire(Trigger.CallEnded);
        }

        public void AcceptCall()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("\t\tHi, who is there?");
            _phone.Fire(Trigger.CallConnected);
        }
    }
}
