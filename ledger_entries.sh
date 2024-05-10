#!/bin/bash

json=$(cat "Base/Xahau.BinaryCodec/Enums/definitions.json")

for key in $(echo $json | jq -r '.LEDGER_ENTRY_TYPES | keys | .[]'); do
    value=$(echo $json | jq -r ".LEDGER_ENTRY_TYPES.$key")
    if (( value < 0 )); then
        echo "public static readonly LedgerEntryType $key = Add(nameof($key), $value);" >> output.txt
    else
        hex_value=$(printf "'\\x$(printf %x $value)'")
        echo "public static readonly LedgerEntryType $key = Add(nameof($key), $hex_value);" >> output.txt
    fi
done