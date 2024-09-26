using System;

namespace lab1.Exceptions;

public class InvalidParticipantTypeException : Exception
{
    public InvalidParticipantTypeException() 
        : base("The participant is of an invalid type.") 
    { 
    }

    public InvalidParticipantTypeException(string message) 
        : base(message) 
    { 
    }

    public InvalidParticipantTypeException(string message, Exception innerException) 
        : base(message, innerException) 
    { 
    }
}