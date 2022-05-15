using Interface;
using System;
using System.IO;
using System.Text;

public enum Parameter
{
    ext,
    path,
    backup_path,
    name
}

public static class Code
{
    public static void Main(string[] args)
    {
        try
        {
            //args = new string[] { "E$cs", "B$E:\\T^EST001", @"P$C:\Users\1\Desktop\Line^Engine^FW\BuildCtrl\Controller", "N$KEK" };
            Console.InputEncoding = Encoding.ASCII;
            Console.CursorSize = 1;
            Color.Reset();
            int choice;
            Other.Load(args);
            if (!Other.Check())
                return;
            while (true)
            {
                Console.WriteLine("Создать бэкап?");
                choice = Menu.Num_menu("создать", "пропустить", "проверить данные");
                if (choice == 3)
                {
                    Color.Print("--данные--\n" +
                        $"название проекта = {Other.parameters[Parameter.name]}\n" +
                        $"папка с исходными файлами = {Other.parameters[Parameter.path]}\n" +
                        $"папка для бэкапа = {Other.parameters[Parameter.backup_path]}\n" +
                        $"{(!(Other.parameters[Parameter.ext] is null) ? $"фильтр расширения файлов = .{Other.parameters[Parameter.ext]}\n" : "")}",
                        ConsoleColor.Cyan, Colorize.Text);
                    Tester.Pause("нажмите любую клавишу, чтобы продолжить");
                    Console.Clear();
                }
                else if (choice == 2)
                    break;
                else
                {
                    Other.Backup();
                    Console.WriteLine("\nДобавить README файл?");
                    if (Menu.Num_menu("да", "нет") == 1)
                    {
                        string text = "";
                        Menu.Input(ref text);
                        if (text.Replace(" ", "").Replace("\n", "") != "")
                        {
                            File.WriteAllText(Other.new_path + "\\README.txt", text.Replace("\n", "\r\n"), Encoding.Unicode);
                            Color.Print("README файл добавлен", ConsoleColor.Green, Colorize.Text);
                            Console.WriteLine();
                            Tester.Pause();
                        }
                    }
                    break;
                }
            }
        }
        catch (Exception E)
        {
            Tester.Crash("непредвиденная ошибка", E.Message);
            Tester.Pause();
        }
    }
}
