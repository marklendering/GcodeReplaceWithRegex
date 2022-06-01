# GcodeReplaceWithRegex

Convert button opens file explorer. Open a gcode file (.nc).
Program will read original file on left side and parsed data on right.

Will filter out Yxxx.xxx data from gcode file, parse this to a floating point number, add 13.37 to value and prints to output as Axxx.xxx
