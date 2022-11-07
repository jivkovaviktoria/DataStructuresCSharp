namespace Problem04.BalancedParentheses
{
    using System;
    using System.Collections.Generic;

    public class BalancedParenthesesSolve : ISolvable
    {
        public bool AreBalanced(string parentheses)
        {
            var openers = new List<char> { '[', '{', '(' };
            var closers = new List<char> { ']', '}', ')' };
            var matchingClosers = new Dictionary<char, char>()
            {
                ['['] = ']',
                ['{'] = '}',
                ['('] = ')',
            };
            var OpendedBrackets = new Stack<char>();

            foreach (char character in parentheses)
            {
                if (openers.Contains(character)) OpendedBrackets.Push(character);
                else 
                {
                    if (OpendedBrackets.Count == 0)
                        return false;

                    if (closers.Contains(character))
                    {
                        if (matchingClosers[OpendedBrackets.Peek()] == character) OpendedBrackets.Pop();
                        else return false;
                    }
                }
            }
            
            return true;
        }
    }
}

