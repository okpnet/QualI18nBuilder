// See https://aka.ms/new-console-template for more information

using Microsoft.Extensions.DependencyInjection;

Console.WriteLine("Hello, World!");
var servicecollection = new ServiceCollection();
servicecollection.AddI18nBuilderService();