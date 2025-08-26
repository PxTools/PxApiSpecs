#!/bin/bash
awk '
$1==":::" && NF>=2 {
   system("'$0' " $2)
   next
}
{print}' "$@"
