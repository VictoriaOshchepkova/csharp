using System;
using System.Collections.Generic;

namespace ConsoleApp6
{
    /// <summary>
    /// Предоставляет вспомогательный метод для работы с мяукающими объектами.
    /// </summary>
    public static class Funs
    {
        /// <summary>
        /// Вызывает мяуканье у каждого объекта, способного мяукать, и подсчитывает количество мяуканий каждого объекта.
        /// </summary>
        /// <param name="meowables">Коллекция мяукающих объектов.</param>
        /// <exception cref="ArgumentNullException">
        /// Выбрасывается, если параметр <paramref name="meowables"/> равен null/>.
        /// </exception>
        public static Dictionary<IMeowable, MeowCounter> MeowsCare(params IMeowable[] meowables)
        {
            if (meowables == null)
                throw new ArgumentNullException(nameof(meowables));

            Dictionary<IMeowable, MeowCounter> counters = new Dictionary<IMeowable, MeowCounter>();

            foreach (var meowable in meowables)
            {
                if (meowable == null)
                    continue;

                if (!counters.ContainsKey(meowable))
                {
                    counters[meowable] = new MeowCounter(meowable);
                }

                counters[meowable].Meow();
            }

            return counters;
            //foreach (var meowable in counters)
            //{
            //    Console.WriteLine($"{meowable.Key} мяукал {meowable.Value.MeowCount} раз(а)");
            //}
        }
    }
}
