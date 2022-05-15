# Backup-After-Build
Этот кусок кода нужен для visual studio. В опции "события после сборки" нужно прописать команду для запуска этого кода(предварительно его скомпилировав), а также добавить необходимые аргументы. После компиляции вашего проекта, запустится этот код и там вы сможете в пару кликов сделать бэкап, который сохраниться под номером, который генерирутеся по простому алгоритму.
### аргументы для программы:
* N$[текст] - название, под которым будет сохранятся бэкап
* P$[путь] - путь, откуда будут бэкапнуты все файлы
* E$[расширние(без точки)] - расширение для файлов, которые будут копироваться(файлы с другими расширениями будут отсеиваться). Этот параметр необязателен.
* B$[путь] - путь, куда будет сохранятся бэкап. Если не существует, то будет создан.

**все пробелы заменяйте символом ^**

**пример аргументации:**
N$My^Project E$py B$C:\Documents\ProjectBackups P$C:\Desktop\Stuff\Python^Project

В этом примере файлы с расширением .py будут копированы из папки "C:\Desktop\Stuff\Python Project" в папку "C:\Documents\ProjectBackups\My Project"

в программе много уязвимостей и багов, например, можно ввести пустое название проекта или невозможность использовать пути, в названии которых есть символ ^. Но мне всё равно.
