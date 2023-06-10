using List;

var list = new CustomList<int>();

list.Add(1);

Console.WriteLine(list[0]); // 1

list.Remove(1);

Console.WriteLine(list.Count); // 0

for(int i = 1; i <= 10; i++)
    list.Add(i);

Console.WriteLine(string.Join(", ", list)); // 1, 2, 3, 4, 5, 6, 7, 8, 9, 10

list.RemoveAt(0);

Console.WriteLine(list[0]); // 2

Console.WriteLine(list.IndexOf(3)); // 1

