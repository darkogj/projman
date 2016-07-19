using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Entities;
using ProjectManagement.Models;

namespace ProjectManagement.Services
{
    public static class Extensions
    {

        public static List<TaskStatusGroup> GroupTasksByStatus(this List<Task> tasksForProject)
        {
            var toDo = tasksForProject.Where(ab => ab.Status == TaskStatus.ToDo).ToList();
            var inProgress = tasksForProject.Where(ab => ab.Status == TaskStatus.InProgress).ToList();
            var inTesting = tasksForProject.Where(ab => ab.Status == TaskStatus.InTesting).ToList();
            var done = tasksForProject.Where(ab => ab.Status == TaskStatus.Done).ToList();
            var maxTaskStatusLength = new List<List<Task>>() { toDo, inProgress, inTesting, done }.Max(l => l.Count());
            toDo.FillWithNullsUntilLength(maxTaskStatusLength);
            inProgress.FillWithNullsUntilLength(maxTaskStatusLength);
            inTesting.FillWithNullsUntilLength(maxTaskStatusLength);
            done.FillWithNullsUntilLength(maxTaskStatusLength);
            return toDo.Zip4(inProgress, inTesting, done, (td, ip, it, dn) => new TaskStatusGroup(td, ip, it, dn)).ToList();

        }


        public static void FillWithNullsUntilLength(this List<Task> list, int requiredLength)
        {
            var listLength = list.Count();
            if (listLength >= requiredLength) return;

            var numberOfNullsToAdd = requiredLength - listLength;
            var listOfNulls = new List<Task>();

            for (int i = 0; i < numberOfNullsToAdd; i++)
            {
                listOfNulls.Add(null);
            }

            list.AddRange(listOfNulls);

        }

        public static IEnumerable<TResult> Zip4<TFirst, TSecond, TThird, TForth, TResult>(
            this IEnumerable<TFirst> first,
            IEnumerable<TSecond> second,
            IEnumerable<TThird> third,
            IEnumerable<TForth> forth,
            Func<TFirst, TSecond, TThird, TForth, TResult> resultSelector)
        {
            using (var enum1 = first.GetEnumerator())
            using (var enum2 = second.GetEnumerator())
            using (var enum3 = third.GetEnumerator())
            using (var enum4 = forth.GetEnumerator())
            {
                while (enum1.MoveNext() && enum2.MoveNext() && enum3.MoveNext() && enum4.MoveNext())
                {
                    yield return resultSelector(
                        enum1.Current,
                        enum2.Current,
                        enum3.Current,
                        enum4.Current);
                }
            }
        }
    }
}