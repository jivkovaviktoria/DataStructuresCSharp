namespace _02.DOM.Models
{
    using System;
    using System.Collections.Generic;
    using _02.DOM.Interfaces;

    public class HtmlElement : IHtmlElement
    {
        public HtmlElement(ElementType type, params IHtmlElement[] children)
        {
            this.Type = type;
            this.Children = new List<IHtmlElement>();
            this.Attributes = new Dictionary<string, string>();

            foreach (var child in children)
            {
                this.Children.Add(child);
                child.Parent = this;
            }
        }

        public ElementType Type { get; set; }

        public IHtmlElement Parent { get; set; }

        public List<IHtmlElement> Children { get; }

        public Dictionary<string, string> Attributes { get; }

        public bool AddAttribute(string key, string value)
        {
            if (this.Attributes.ContainsKey(key))
            {
                return false;
            }

            this.Attributes.Add(key, value);

            return true;
        }

        public bool HasId(string id)
        {
            if (this.Attributes.ContainsKey("id"))
            {
                return this.Attributes["id"] == id;
            }

            return false;
        }

        public bool RemoveAttribute(string key)
        {
            if (!this.Attributes.ContainsKey(key))
            {
                return false;
            }

            this.Attributes.Remove(key);

            return true;
        }
    }
}
