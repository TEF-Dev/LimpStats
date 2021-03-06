﻿using System.Collections.Generic;
using System.Linq;

namespace LimpStats.Model
{
    public class LimpUser
    {
        public LimpUser(string eolympLogin, string codeforcesHandle, string username)
        {
            Username = username;
            EOlympLogin = eolympLogin;
            CodeforcesHandle = codeforcesHandle;

            EOlimpProblemsResult = new Dictionary<int, int>();
            CodeforcesSubmissions = new List<string>();
        }

        public string Username { get; set; }
        public string EOlympLogin { get; set; }
        public string CodeforcesHandle { get; set; }

        public Dictionary<int, int> EOlimpProblemsResult { get; set; }
        public List<string> CodeforcesSubmissions { get; set; }

        public int CompletedTaskCount() => EOlimpProblemsResult.Count(t => t.Value == 100);
    }
}