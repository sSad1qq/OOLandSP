#!/usr/bin/env bash
# PostToolUse Write|Edit: clang-tidy (+ -Wall -Wextra -Wpedantic) для lb1_*/*.cpp.
# При предупреждениях - exit 2, вывод уходит Claude. Иначе молча exit 0.

input=$(cat)
f=$(printf '%s' "$input" | sed -n 's/.*"tool_input":{[^}]*"file_path":"\([^"]*\)".*/\1/p' | sed 's/\\\\/\\/g')
[ -n "$f" ] || exit 0
command -v cygpath >/dev/null 2>&1 && f=$(cygpath -u "$f")

case "$f" in
    */lb1_*/*.cpp) ;;
    *) exit 0 ;;
esac

tidy="/e/Microsoft Visual Studio/18/Community/VC/Tools/Llvm/x64/bin/clang-tidy.exe"
[ -x "$tidy" ] || tidy=$(command -v clang-tidy) || exit 0

config="$(dirname "$0")/../../.clang-tidy"
out=$("$tidy" --quiet --config-file="$config" "$f" -- -std=c++17 -Wall -Wextra -Wpedantic 2>&1 | grep -E '(warning|error):' -A3)
[ -z "$out" ] && exit 0
printf 'clang-tidy %s:\n%s\n' "$f" "$out" >&2
exit 2
