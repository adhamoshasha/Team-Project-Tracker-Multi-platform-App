using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Team_Logger_API.Models
{
    public class Announcment
    {
        public string  Title { get; set; }
        public string  Content { get; set; }
        public DateTime Date { get; set; }
        public string Sender_Name { get; set; }
        public Announcment()
        {
            Title = "";
            Content = "";
            Date = DateTime.Now;
            Sender_Name = "";
        }
        public Announcment(string tite, string s_n, string txt, DateTime d )
        {
            Title = tite;
            Content = txt;
            Sender_Name = s_n;
            Date = d;
        }
    }
}