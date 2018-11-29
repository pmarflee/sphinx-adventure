using System;

namespace SphinxAdventure.Core.Entities.Exceptions
{
    public class InvalidActionException : Exception
    {
        public InvalidActionException(string message) : base(message) { }
    }
}
