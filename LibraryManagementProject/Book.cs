using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagementProject
{
    //Book – представлява книга с ISBN, заглавие, автор и година на издаване.
    public class Book
    {
        public int ISBN { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int Year { get; set; }

        public override string ToString()
        {
            return $"ISBN: {ISBN,-6} | Title: {Title,-20} | Author: {Author,-15} | Year: {Year}";
        }
    }
}
