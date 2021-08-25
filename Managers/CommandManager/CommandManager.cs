using ConsoleIO;
using System;

namespace Managers.CommandManagers
{
    public abstract class CommandManager
    {
        protected bool _doThingAfterCommand = true;

        protected CommandInfo[] commandInfoArray;
        protected CommandManager() => IniCommandInfoArray();
        protected abstract void IniCommandInfoArray();
        protected abstract void PrepareScreen();

        public bool Run()
        {
            while (true)
            {
                PrepareScreen();
                Console.WriteLine();
                ShowCommandsMenu();

                Command command = EnterCommand();
                if (command == null) return false;
                command();
                if (_doThingAfterCommand)
                {
                    ThingAfterCommand();
                    return true;
                }
            }
        }     

        protected void ShowCommandsMenu()
        {
            for (int i = 0; i < commandInfoArray.Length; i++) Console.WriteLine("\t{0,2} - {1}", i, commandInfoArray[i].Name);
        }
        protected Command EnterCommand()
        {
            Console.WriteLine();
            return commandInfoArray[Entering.EnterInt("Введіть номер команди меню: ", 0, commandInfoArray.Length - 1, "\nНЕВІРНИЙ ПАРАМЕТР\tВиберіть одну з поданих операцій")].Command;
        }

        protected virtual bool ReaquestForContinuation(string message = "\nДля продовження роботи натисність довільну клавішу, (S) щоб зберегти текст в файл, (Esc) щоб завершити роботу")
        {
            Console.WriteLine(message);
            ConsoleKeyInfo cki = Console.ReadKey(true);

            if (cki.Key == ConsoleKey.S) return true;
            if (cki.Key == ConsoleKey.Escape) Environment.Exit(0);

            return false;
        }
        protected virtual void ThingAfterCommand() { }
    }
}
