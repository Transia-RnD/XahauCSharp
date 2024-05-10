using System;

namespace Xahau.Client.Exceptions
{
    public class RippleException : Exception
    {
        public RippleException() { }

        public RippleException(string message) : base(message) { }
        public RippleException(string message, Exception? InnerException) : base(message, InnerException) { }
    }

    public class XahauException : Exception
    {
        //public readonly string message;
        //public readonly byte[]? data;

        //public XahauException(byte[]? data, string message = "")
        //{
        //    //this.name = this.constructor.name; 
        //    this.message = message;
        //    this.data = data;
        //}

        public XahauException() { }

        public XahauException(string message) : base(message) { }
        public XahauException(string message, Exception? InnerException) : base(message, InnerException) { }
        //public XahauException(string message, dynamic data) : base(message, data) { }
    }

    /// <summary>
    /// Exception thrown when rippled responds with an Exception.
    /// </summary>
    public class RippledException : XahauException {
        public RippledException(string message, dynamic data = null) : base(message)
        {
        }
    }
    /// <summary>
    /// Exception thrown when xrpl.js cannot specify Exception type.
    /// </summary>
    public class UnexpectedException : XahauException { }
    /// <summary>
    /// Exception thrown when xrpl.js has an Exception with connection to rippled.
    /// </summary>
    public class ConnectionException : XahauException
    {
        public ConnectionException(string message) : base(message)
        {
        }
    }
    /// <summary>
    /// Exception thrown when xrpl.js is not connected to rippled server.
    /// </summary>
    public class NotConnectedException : XahauException
    {
        public NotConnectedException(string message = null) : base(message)
        {
        }
    }
    /// <summary>
    /// Exception thrown when xrpl.js has disconnected from rippled server.
    /// </summary>
    public class DisconnectedException : XahauException
    {
        public DisconnectedException(string message) : base(message)
        {
        }
    }
    /// <summary>
    /// Exception thrown when rippled is not initialized.
    /// </summary>
    public class RippledNotInitializedException : XahauException { }
    /// <summary>
    /// Exception thrown when xrpl.js times out.
    /// </summary>
    public class TimeoutException : XahauException { }
    /// <summary>
    /// Exception thrown when xrpl.js sees a response in the wrong format.
    /// </summary>
    public class ResponseFormatException : XahauException
    {
        public ResponseFormatException(string message, dynamic data = null) : base(message)
        {
        }
    }
    /// <summary>
    /// Exception thrown when xrpl.js sees a malformed transaction.
    /// </summary>
    public class ValidationException : XahauException
    {
        public ValidationException(string message = null) : base(message)
        {
        }
    }
    /// <summary>
    /// Exception thrown when a client cannot generate a wallet from the testnet/devnet
    /// faucets, or when the client cannot infer the faucet URL(i.e.when the Client
    /// is connected to mainnet).
    /// </summary>
    public class XRPLFaucetException : XahauException
    {
        public XRPLFaucetException(string message = null) : base(message)
        {
        }
    }
    /// <summary>
    /// Exception thrown when xrpl.js cannot retrieve a transaction, ledger, account, etc.
    /// From rippled.
    /// </summary>
    public class NotFoundException : XahauException
    {
        public NotFoundException(string message = "Not Found") : base(message) { }
    }
}