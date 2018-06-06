using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ict_lab_website.Models.Schedule.Views
{
    public interface IView
    {
        string Name { get; }
        DateTime IncreaseDate(DateTime dateTime);
        DateTime DecreaseDate(DateTime dateTime);
    }
}
