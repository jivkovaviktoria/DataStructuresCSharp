﻿namespace _02.DOM.Interfaces
{
    using _02.DOM.Models;
    using System.Collections.Generic;

    public interface IHtmlElement
    {
        ElementType Type { get; set; }

        IHtmlElement Parent { get; set; }

        List<IHtmlElement> Children { get; }

        Dictionary<string, string> Attributes { get; }

        bool HasId(string id);

        bool AddAttribute(string key, string value);

        bool RemoveAttribute(string key);
    }
}
