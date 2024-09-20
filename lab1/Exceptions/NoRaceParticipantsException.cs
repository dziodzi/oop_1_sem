using System;

namespace lab1.Exceptions;

public class NoRaceParticipantsException : Exception
{
    public NoRaceParticipantsException() 
        : base("There are no participants in the race.") 
    { 
    }

    public NoRaceParticipantsException(string message) 
        : base(message) 
    { 
    }

    public NoRaceParticipantsException(string message, Exception innerException) 
        : base(message, innerException) 
    { 
    }
}