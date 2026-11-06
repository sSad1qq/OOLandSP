# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

Основные правила курса - в AGENTS.md; они действуют полностью:

@AGENTS.md

Ниже - только то, что в AGENTS.md устарело или отсутствует.

## Платформы

Работа ведется и на Windows, и на macOS. Команды из AGENTS.md (`c++`,
`/tmp`, `-fsanitize=address,undefined`) - для macOS. На Windows `c++`,
`g++`, `cl` и `msbuild` отсутствуют в PATH; установлена Visual Studio 2026
Community (18.x) в `E:\Microsoft Visual Studio\18\Community`.

Проверочные бинарники на Windows класть в scratchpad-каталог сессии, а не в
каталоги лабораторных и не в `/tmp`. Для полного цикла сборки и запуска
использовать `/check-lab`.

### Консольные работы lb1_x на Windows (MSVC)

`cl` работает только после `vcvars64.bat`, поэтому запускать через
`.cmd`-файл:

```bat
call "E:\Microsoft Visual Studio\18\Community\VC\Auxiliary\Build\vcvars64.bat" >nul 2>&1
cl /nologo /std:c++17 /utf-8 /EHsc /W4 /permissive- /fsanitize=address /Zi <путь>\main.cpp /Fe:<scratch>\lb.exe /Fo:<scratch>\ /Fd:<scratch>\
```

- `/utf-8` обязателен: исходники - UTF-8 без BOM, иначе MSVC прочитает
  кириллицу как CP1251.
- Запускать `.exe`, собранный с ASan, тоже после `vcvars64.bat`, иначе не
  найдется DLL санитайзера. UBSan в MSVC нет.
- Программы ждут ввода и вызывают `system("pause")` - подавать ввод через
  stdin (`echo ... |`), иначе запуск зависнет.
- `pow()` без `#include <cmath>` (lb1_2, lb1_3, lb1_5) собирается в MSVC
  только за счет транзитивных заголовков; чистая сборка не доказывает
  соблюдение правила AGENTS.md о заголовках.

## Фактическая структура (дополняет AGENTS.md)

- `lb1_3`-`lb1_6/main.cpp` - отдельные консольные программы ЛБ 1.3-1.6.
- `lb2/` - проект ЛБ 2.1-2.7. Язык уже выбран: **C# Windows Forms, .NET
  Framework 4.7.2**, старый (не SDK) `lb2.csproj`, решение `lb2.slnx`
  (C++/CLI не используется, `.vcxproj` нет). Вся логика - в
  `lb2/Form1.cs`, разметка - `Form1.Designer.cs`. График -
  `System.Windows.Forms.DataVisualization`.
- Сборка lb2 (только Windows):
  `"E:/Microsoft Visual Studio/18/Community/MSBuild/Current/Bin/MSBuild.exe" lb2/lb2.slnx -nologo -v:m "-p:OutDir=<scratch>\lb2out\" "-p:IntermediateOutputPath=<scratch>\lb2obj\"`.
  Оба параметра обязательны: `lb2/obj` и `.vs/` закоммичены, и сборка без
  них меняет отслеживаемые файлы. Успешная сборка не означает визуальной
  проверки формы.
- `.cs`-файлы - UTF-8 с BOM и CRLF; сохранять кодировку при правке.
- `3 ООЯиСП/` - материалы курсового проекта (docx), кода нет.
- Все отслеживаемые бинарники в `lb1_*` (`lb1_N`, `prog`, `*.dSYM`) -
  сборки Mach-O arm64 для macOS; на Windows не запускаются. Не изменять и
  не коммитить новые.

## Git

Сообщения коммитов короткие, на английском, строчными:
`complete lb2_6`, `upd: lb1_3`.
