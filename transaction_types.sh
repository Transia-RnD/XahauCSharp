#!/bin/bash

json=$(cat "Base/Xahau.BinaryCodec/Enums/definitions.json")

for key in $(echo $json | jq -r '.TRANSACTION_TYPES | keys | .[]'); do
    value=$(echo $json | jq -r ".TRANSACTION_TYPES.$key")
    echo "public static readonly TransactionType $key = Add(nameof($key), $value);" >> output.txt
done