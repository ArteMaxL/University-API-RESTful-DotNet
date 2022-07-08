﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LinqSnippets
{
    public class Snippets
    {
        static public void BasicLinq()
        {
            string[] cars =
            {
                "VW Golf",
                "VW California",
                "Audi A3",
                "Audi A5",
                "Fiat Punto",
                "Seat Ibiza",
                "Seat León"
            };

            // 1. SELECT * of cars (SELECT ALL CARS)
            var carList = from car in cars select car;

            foreach (var car in carList)
            {
                Console.WriteLine(car);
            }

            // 2. SELECT WHERE car is Audi (SELECT AUDIs)
            var audiList = from car in cars where car.Contains("Audi") select car;

            foreach (var audi in carList)
            {
                Console.WriteLine(audiList);
            }
        }

        //Number Examples

        static public void LinqNumbers()
        {
            List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9};

            // Each Number multiplied by 3
            // take all numbers, but 9
            // Order numbers by ascending value

            var processedNumberList =
                numbers
                .Select(num => num * 3) // { 3, 6, 9, etc. }
                .Where(num => num != 9) // { all but the 9 }
                .OrderBy(num => num); // { at the end, we order ascending }
        }

        static public void SearchExamples()
        {
            List<string> textList = new List<string>
            {
                "a",
                "bx",
                "c",
                "d",
                "e",
                "cj",
                "f",
                "c"
            };

            // 1. First of all elements

            var first = textList.First();

            // 2. First element that is "c"

            var cText = textList.First(text => text.Equals("c"));

            // 3. First element that contains "j"

            var jText = textList.First(text => text.Contains("j"));

            // 4. First element that contains "z" or default

            var firstOrDefaultText = textList
                .FirstOrDefault(text => text.Contains("z")); // "" or first element that contains "z" 

            // 5. Last element that contains "z" or default

            var lastOrDefaultText = textList
                .LastOrDefault(text => text.Contains("z")); // "" or last element that contains "z" 

            // 6. Single Values

            var uniqueTexts = textList.Single();
            var uniqueOrDefaultTexts = textList.SingleOrDefault();

            int[] evenNumbers = { 0, 2, 4, 6, 8 };
            int[] otherEvenNumbers = { 0, 2, 6 };

            // 7. Obtain { 4, 8 }

            var myEvenNumbers = evenNumbers.Except(otherEvenNumbers); // { 4, 8 }
        }

        static public void MultipleSelects()
        {
            // SELECT MANY

            string[] myOpinions =
            {
                "Opinion 1, text 1",
                "Opinion 2, text 2",
                "Opinion 3, text 3"
            };

            var myOpinionSelection = myOpinions.SelectMany(opinion => opinion.Split(","));

            var enterprises = new[]
            {
                new Enterprise()
                {
                    Id = 1,
                    Name = "Enterprise 1",
                    Employees = new[]
                    {
                        new Employee
                        {
                            Id=1,
                            Name="Ana",
                            Email="email@email.com",
                            Salary= 5000
                        },
                        new Employee
                        {
                            Id=2,
                            Name="Marcela",
                            Email="mar@email.com",
                            Salary= 4000
                        },new Employee
                        {
                            Id=3,
                            Name="Marcia",
                            Email="marcia@email.com",
                            Salary= 6000
                        }
                    }
                },
                new Enterprise()
                {
                    Id = 2,
                    Name = "Enterprise 2",
                    Employees = new[]
                    {
                        new Employee
                        {
                            Id=4,
                            Name="Artemio",
                            Email="email@email.com",
                            Salary= 2570
                        },
                        new Employee
                        {
                            Id=5,
                            Name="Maximiliano",
                            Email="max@email.com",
                            Salary= 4300
                        },new Employee
                        {
                            Id=6,
                            Name="Papau",
                            Email="papau@email.com",
                            Salary= 5600
                        }
                    }
                }
            };

            // Obtain all Employees of al Enterprises

            var employeeList = enterprises.SelectMany(enterprise => enterprise.Employees);

            // Known if any list is empty

            bool hasEnterprises = enterprises.Any();

            bool hasEmployees = enterprises.Any(enterprise => enterprise.Employees.Any());

            // All enterprises at least employees with at least 1000€ of salary

            bool hasEmployeeWithSalaryMoreThanOrEqual1000 = enterprises
                .Any(enterprise => enterprise.Employees.Any(employee => employee.Salary >= 1000)); 
        }

        static public void LinqCollections()
        {
            var firstList = new List<string> { "a", "b", "c" };
            var secondList = new List<string> { "a", "c", "d" };

            // INNER JOIN

            var commonResult = from element in firstList
                               join secondElement in secondList
                               on element equals secondElement
                               select new { element, secondElement };

            var commonResult2 = firstList.Join(
                    secondList,
                    element => element,
                    secondElement => secondElement,
                    (element, secondElement) => new { element, secondElement }
                );

            // OUTER JOIN - LEFT

            var leftOuterJoin = from element in firstList
                                join secondElement in secondList
                                on element equals secondElement
                                into temporalList
                                from temporalElement in temporalList.DefaultIfEmpty()
                                where element != temporalElement
                                select new { Element = element };

            var leftOuterJoin2 = from element in firstList
                                 from secondElement in secondList.Where(s => s == element).DefaultIfEmpty()
                                 select new { Element = element, SecondElement = secondElement };
            
            // OUTER JOIN - RIGHT

            var rightOuterJoin = from secondElement in secondList
                                join element in firstList
                                on secondElement equals element
                                into temporalList
                                from temporalElement in temporalList.DefaultIfEmpty()
                                where secondElement != temporalElement
                                select new { Element = secondElement };

            // UNION

            var unionList = leftOuterJoin.Union(rightOuterJoin);
        }

        static public void SkipTakeLinq()
        {
            var myList = new[]
            {
                1,2,3,4,5,6,7,8,9,10
            };

            // SKIP

            var skipTwoFirstValues = myList.Skip(2); // { 3,4,5,6,7,8,9,10 }
            var skipLastTwoFirstValues = myList.SkipLast(2); // { 1,2,3,4,5,6,7,8 }
            var skipWhileSmallerThan4 = myList.SkipWhile(num => num < 4); // { 4,5,6,7,8,9,10 }
          
            // TAKE

            var takeFirstTwoValues = myList.Take(2); // { 1,2 }
            var takeLastTwoValues = myList.TakeLast(2); // { 9,10 }
            var takeWhileSmallerThan4 = myList.TakeWhile(num => num < 4); // { 1,2,3 }
        } 

        // TODO:

        // Variables

        // ZIP

        // Repeat

        // ALL

        // Agreagate

        // Distinct

        // GroupBy
    }
}