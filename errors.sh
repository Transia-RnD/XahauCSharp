#!/bin/bash

# Define the input C++ file
input_cpp_file="~/projects/xrpl-labs/xahaud-develop/src/ripple/protocol/impl/TER.cpp"

# Define the output file
output_file="output.txt"

# Start processing
awk '
# Match lines that contain MAKE_ERROR
/MAKE_ERROR/ {
    # Remove the MAKE_ERROR and surrounding spaces
    gsub(/^[ \t]*MAKE_ERROR\(/, "", $0);
    gsub(/\)[ \t]*,$/, "", $0);

    # Split the line into code and description
    split($0, parts, ",");

    # Trim leading and trailing spaces from code and description
    gsub(/^[ \t]+|[ \t]+$/, "", parts[1]);
    gsub(/^[ \t]+|[ \t]+$/, "", parts[2]);
    gsub(/^"|"$/, "", parts[2]); # Remove the quotes around the description

    # Print in the desired format
    printf "public static readonly EngineResult %s = Add(nameof(%s), -399, \"%s.\");\n", parts[1], parts[1], parts[2];
}
' "$input_cpp_file" > "$output_file"

echo "Output written to $output_file"