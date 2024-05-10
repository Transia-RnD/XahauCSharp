﻿using Newtonsoft.Json;

namespace Xahau.Models.Subscriptions
{
    public class BaseStream
    {
        /// <summary>
        /// consensusPhase indicates this is from the consensus stream<br/>
        /// consensusPhase - type
        /// </summary>
        [JsonProperty("type")]
        public ResponseStreamType Type { get; set; }
    }
}