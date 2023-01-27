
using CodeWars.Tests;

//Console.WriteLine(KataTrainer.Rgb(255,255,1000));
var collection = new List<int> {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24};
var paginationHelper = new KataTrainer.PagnationHelper<int>(collection, 10);

Console.WriteLine(paginationHelper.PageCount);
Console.WriteLine(paginationHelper.ItemCount);

Console.WriteLine(paginationHelper.PageItemCount(0));
Console.WriteLine(paginationHelper.PageItemCount(1));
Console.WriteLine(paginationHelper.PageItemCount(2));

Console.WriteLine(paginationHelper.PageIndex(5));
Console.WriteLine(paginationHelper.PageIndex(2));
Console.WriteLine(paginationHelper.PageIndex(20));
Console.WriteLine(paginationHelper.PageIndex(24));