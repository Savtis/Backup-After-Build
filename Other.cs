using Interface;
using System;
using System.Collections.Generic;
using System.IO;

public static class Other
{
    public static Dictionary<Parameter, string> parameters = new Dictionary<Parameter, string>
        {
            { Parameter.ext, null },
            { Parameter.path, null },
            { Parameter.backup_path, null },
            { Parameter.name, null }
        };
    public static string new_path;
    public static bool Check()
    {
        string error_text = null;
        foreach (var pair in parameters)
        {
            if (pair.Value is null && pair.Key != Parameter.ext)
            {
                switch (pair.Key)
                {
                    case Parameter.path:
                        error_text = "необходимо указать папку для копирования файлов";
                        break;
                    case Parameter.backup_path:
                        error_text = "необходимо указать папку куда будут копироваться файлы";
                        break;
                    case Parameter.name:
                        error_text = "необходимо указать название для проекта, файлы которого будут копироваться";
                        break;
                }
                Tester.Crash("ошибка параметров", error_text);
                return false;
            }
        }
        return true;
    }
    public static void Load(string[] args)
    {
        string token;
        string value;
        foreach (string arg in args)
        {
            token = arg.Split('$')[0];
            value = arg.Split('$')[1];
            switch (token)
            {
                case "E":
                    parameters[Parameter.ext] = value;
                    break;
                case "P":
                    parameters[Parameter.path] = value.Replace('^', ' ');
                    break;
                case "B":
                    parameters[Parameter.backup_path] = value.Replace('^', ' ');
                    break;
                case "N":
                    parameters[Parameter.name] = value.Replace('^', ' ');
                    break;
            }
        }
    }
    public static void Backup()
    {
        DateTime time = DateTime.Now;
        int num = (time.Year - 2021) * 100 + time.Month + time.Day + time.Hour + (int)Math.Ceiling((double)(time.Minute / 2));
        if (Directory.Exists(parameters[Parameter.path])) { }
        else
        {
            Tester.Crash("ошибка директории", $"директории {parameters[Parameter.path]} - не существует");
            return;
        }
        string[] file_list = Directory.GetFiles(parameters[Parameter.path]);
        List<string> new_list = new List<string> { };
        if (parameters[Parameter.ext] is null) { }
        else
        {
            Color.Print("отбор файлов по определённому расширению", ConsoleColor.Yellow, Colorize.Text);
            foreach (string file in file_list)
            {
                if (file.Split('.')[1] == parameters[Parameter.ext])
                    new_list.Add(file);
            }
            file_list = new_list.ToArray();
            Color.Print("отобрано", ConsoleColor.Green, Colorize.Text);
        }
        new_list.Clear();
        foreach (string file in file_list)
            new_list.Add(file.Split('\\')[file.Split('\\').Length - 1]);
        file_list = new_list.ToArray();
        if (!Directory.Exists(parameters[Parameter.backup_path]))
        {
            Directory.CreateDirectory(parameters[Parameter.backup_path]);
            Color.Print("несуществующая директория была создана", ConsoleColor.Yellow, Colorize.Text);
        }
        new_path = $"{parameters[Parameter.backup_path]}\\{parameters[Parameter.name]} {num}";
        if (Directory.Exists(new_path))
        {
            Directory.Delete(new_path, true);
            Color.Print($"существующий каталог был удалён", ConsoleColor.Yellow, Colorize.Text);
        }
        Directory.CreateDirectory(new_path);
        Color.Print("директория для бэкапа создана", ConsoleColor.Green, Colorize.Text);
        foreach (string file in file_list)
        {
            File.Copy(parameters[Parameter.path] + '\\' + file, new_path + '\\' + file);
            Color.Print($"файл {file} копирован", ConsoleColor.Cyan, Colorize.Text);
        }
        Color.Print("--бэкап создан--", ConsoleColor.Green, Colorize.Text);
        Color.Print($"номер = {num}", ConsoleColor.Cyan, Colorize.Text);
        Color.Print($"папка с бэкапа = {new_path}", ConsoleColor.Cyan, Colorize.Text);
    }
}