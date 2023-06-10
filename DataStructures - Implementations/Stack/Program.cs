using Stack;

var stack = new CustomStack<int>();

stack.Push(1);

Console.WriteLine(stack.Peek()); // 1

stack.Push(3);

Console.WriteLine(stack.Pop()); // 3
Console.WriteLine(stack.Count); // 1

stack.Clear();

Console.WriteLine(stack.Count); // 0